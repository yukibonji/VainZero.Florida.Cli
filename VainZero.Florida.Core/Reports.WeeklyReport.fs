namespace VainZero.Florida.Reports

open System
open System.IO
open Chessie.ErrorHandling
open FsYaml
open VainZero.Collections
open VainZero.Misc
open VainZero.Text
open VainZero.Florida.Configurations
open VainZero.Florida.Data

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module WeeklyReport =
  let empty staff =
    {
      担当者 =
        staff
      今週の主な活動 =
        ""
      進捗 =
        ""
      日別の内容 =
        [||]
      今週実績 =
        ""
      来週予定 =
        ""
      その他 =
        ""
    }

  /// 指定された日付に生成すべき週報の対象となる日付の区間を取得する。
  let dateRangeFromDateAsync (context: IDataContext) date =
    async {
      let! latest = context.WeeklyReports.LatestDateRangeAsync(date)
      match latest with
      | Some (firstDate, lastDate) ->
        return (lastDate.AddDays(1.0), date)
      | None ->
        let! firstDate = context.DailyReports.FirstDateAsync
        match firstDate with
        | Some firstDate ->
          return (firstDate, date)
        | None ->
          return (date, date)
    }

  module internal GenerateFromDailyReports =
    /// 指定された日付に生成すべき週報の対象となる日報をすべて検索する。
    let dailyReportsAsync (dataContext: IDataContext) (firstDate, lastDate) =
      async {
        let! reports =
          DateTime.dates firstDate lastDate
          |> Seq.map
            (fun date ->
              async {
                let! report = dataContext.DailyReports.FindAsync(date)
                return report |> Option.map (fun (_, report) -> (date, report))
              }
            )
          |> Async.Parallel
        return reports |> Array.choose id
      }

    /// 日報に含まれる作業の名前のリストと、作業の名前から備考への辞書を生成する。
    /// 同内容の作業が複数の日報に渡っている場合は、1つにまとめる。
    let activities reports =
      let (activityNames, activityNoteList) =
        reports
        |> Array.collect (fun (_, report: DailyReport) -> report.作業実績)
        |> Array.map
          (fun work ->
            let title = sprintf "%s/%s" work.案件 work.内容
            (title, (title, work.備考))
          )
        |> Array.unzip
        |> fun (activityNames, activityNoteList) ->
            ( activityNames |> Array.unique
            , activityNoteList |> Seq.choose
                (fun (title, note) -> note |> Option.map (fun note -> (title, note)))
            )
      let activityNotes =
        Map.empty
        |> fold activityNoteList
          (fun (title, note) m ->
            Map.appendWith
              (fun l r -> l + Environment.NewLine + r)
              m (Map.singleton title note)
          )
      (activityNames, activityNotes)

    /// 作業項目のリストから進捗リストの雛形を生成する。
    let progressList activityNames =
      activityNames
      |> Array.map
        (fun activityName ->
          sprintf "%s: 0%c" activityName '%'
        )
      |> String.concatWithLineBreak

    let simplifiedDailyReports dailyReports =
      dailyReports
      |> Array.map
        (fun (date, report) ->
          {
            日付 = date
            作業実績 =
              (report: DailyReport).作業実績
              |> Array.map (fun dr -> { dr with 備考 = None })
          }
        )

    /// 日報の備考欄から、1週間の活動実績を書くための参考になるテキストを生成する。
    let weeklyActivity activityNames activityNotes =
      activityNames
      |> Array.choose
        (fun activityName ->
          activityNotes
          |> Map.tryFind activityName
          |> Option.map (fun note -> (activityName, note))
        )
      |> Map.ofArray // TODO: 順序を保つ。
      |> Yaml.myDump

    let staff (config: Config) =
      {
        所属部署 =
          config.Department |> Option.getOr ""
        名前 =
          config.UserName |> Option.getOr ""
      }

    let weeklyReportAsync config dataContext dateRange =
      async {
        let! dailyReports = dailyReportsAsync dataContext dateRange
        let staff = staff config
        let (activityNames, activityNotes) = dailyReports |> activities
        return
          { empty staff with
              担当者 = staff
              今週の主な活動 =
                activityNames |> String.concatWithLineBreak
              進捗 =
                progressList activityNames
              日別の内容 =
                simplifiedDailyReports dailyReports
              今週実績 =
                weeklyActivity activityNames activityNotes
          }
      }

    let generateAsync config (dataContext: IDataContext) date =
      async {
        let! dateRange = dateRangeFromDateAsync dataContext date
        let! weeklyReport = weeklyReportAsync config dataContext dateRange
        do! dataContext.WeeklyReports.AddOrUpdateAsync(dateRange, weeklyReport)
        dataContext.WeeklyReports.Open(dateRange)
      }

  /// 週報を生成して開く。
  let generateAsync = GenerateFromDailyReports.generateAsync

  module private ConvertToExcelXml =
    let (-->) x y = (x, y)

    type DayRow =
      {
        Date:
          string
        DayOfWeek:
          string
        Project:
          string
        Content:
          string
        Duration:
          string
      }
    with
      static member Create(date, dow, project, content, duration) =
        {
          Date = date
          DayOfWeek = dow
          Project = project
          Content = content
          Duration = duration
        }

      static member Empty =
        DayRow.Create("", "", "", "", "")

    let dayRows (wr: WeeklyReport) =
      [|
        for dailyReport in wr.日別の内容 do
          let date = dailyReport.日付
          for (i, work) in dailyReport.作業実績 |> Array.indexed do
            let dateString =
              if i = 0 then date.ToString("yyyy/MM/dd") else ""
            let dow =
              if i = 0 then date.DayOfWeek |> DayOfWeek.toKanji else ""
            let duration =
              work.工数 |> sprintf "%.2f"
            yield DayRow.Create(dateString, dow, work.案件, work.内容, duration)
      |]
      |> Array.tailpad 10 DayRow.Empty

    let toExcelXml (wr: WeeklyReport) =
      let dailyWorkXml =
        wr
        |> dayRows
        |> Array.map
          (fun dayRow ->
            dayByDay
            |> String.replaceEach
                [|
                  "{{日付}}" --> dayRow.Date
                  "{{曜日}}" --> dayRow.DayOfWeek
                  "{{案件}}" --> dayRow.Project
                  "{{内容}}" --> dayRow.Content
                  "{{工数}}" --> dayRow.Duration
                |]
          )
        |> String.concat (Environment.NewLine)
      let xml =
        weeklyReport
        |> String.replaceEach
            [|
              "{{所属部署}}"
                --> wr.担当者.所属部署
              "{{担当者名}}"
                --> wr.担当者.名前
              "{{今週活動}}"
                --> (wr.今週の主な活動 |> Xml.escape)
              "{{進捗}}"
                --> (wr.進捗 |> Xml.escape)
              "{{日別の内容}}"
                --> dailyWorkXml
              "{{今週実績}}"
                --> (wr.今週実績 |> Xml.escape)
              "{{来週予定}}"
                --> (wr.来週予定 |> Xml.escape)
              "{{その他}}"
                --> (wr.その他 |> Xml.escape)
            |]
      xml

  let toExcelXml = ConvertToExcelXml.toExcelXml

  /// 指定された日付に対応する週報をエクセルファイルに変換して開く。
  let convertToExcelAsync (dataContext: IDataContext) date =
    async {
      let! dateRange = dateRangeFromDateAsync dataContext date
      let! weeklyReport = dataContext.WeeklyReports.FindAsync(dateRange)
      match weeklyReport with
      | Some weeklyReport ->
        let excelXml = weeklyReport |> toExcelXml
        do! dataContext.WeeklyReportExcels.AddOrUpdateAsync(dateRange, excelXml)
        dataContext.WeeklyReportExcels.Open(dateRange)
        return ok ()
      | None ->
        return "週報がありません。" |> fail
    }
