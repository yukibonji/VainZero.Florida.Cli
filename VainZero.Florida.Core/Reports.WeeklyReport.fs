namespace VainZero.Florida.Reports

open System
open System.IO
open System.Collections.Generic
open FSharpKit.ErrorHandling
open FsYaml
open VainZero.Collections
open VainZero.Misc
open VainZero.Text
open VainZero.Florida.Misc
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

  /// 指定された日付に生成すべき週報の対象となる日付の区間 (閉区間) を取得する。
  let dateRangeFromDateAsync (context: IDataContext) date =
    async {
      let! latest = context.WeeklyReports.LatestDateRangeAsync(date)
      match latest with
      | Some (firstDate, lastDate) ->
        let! existsNewerDailyReport =
          async {
            let dates = DateTime.dates (lastDate.AddDays(1.0)) (date.AddDays(1.0))
            let! reports = DailyReport.findByRangeAsync context dates
            return reports |> Array.isEmpty |> not
          }
        return
          if existsNewerDailyReport
          then (lastDate.AddDays(1.0), date)
          else (firstDate, lastDate)
      | None ->
        let! firstDate = context.DailyReports.FirstDateAsync
        match firstDate with
        | Some firstDate ->
          return (firstDate, date)
        | None ->
          return (date, date)
    }

  module internal GenerateFromDailyReportsFunction =
    /// 指定された日付に生成すべき週報の対象となる日報をすべて検索する。
    let dailyReportsAsync dataContext (firstDate, lastDate: DateTime) =
      DailyReport.findByRangeAsync dataContext (DateTime.dates firstDate (lastDate.AddDays(1.0)))

    /// 週報の日付の区間を、実際に日報があった区間に縮める。
    let minimalDateRange (dailyReports: array<DateTime * _>) =
      if dailyReports |> Array.isEmpty then
        "週報にまとめる対象の日報がありません。" |> failwith
      else
        let dates = dailyReports |> Seq.map fst |> Seq.sort
        (dates |> Seq.min, dates |> Seq.max)

    /// 日報に含まれる作業の名前のリストと、作業の名前から備考への辞書を生成する。
    /// 同内容の作業が複数の日報に渡っている場合は、1つにまとめる。
    let activities reports =
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
          , activityNoteList
            |> Seq.choose
              (fun (title, note) -> note |> Option.map (fun note -> (title, note)))
            |> Seq.bundle id (fun l r -> l + Environment.NewLine + r)
            |> Map.ofSeq
          )

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
            日付 = date |> Date.ofDateTime
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
          |> Option.map (fun note -> KeyValuePair(activityName, note))
        )
      |> Yaml.myDump<array<KeyValuePair<_, _>>>

    let staff (config: Config) =
      {
        所属部署 =
          config.Department |> Option.getOr ""
        名前 =
          config.UserName |> Option.getOr ""
      }

    let weeklyReport staff (reports: array<DateTime * DailyReport>) =
      let (activityNames, activityNotes) = reports |> activities
      { empty staff with
          担当者 = staff
          今週の主な活動 =
            activityNames |> String.concatWithLineBreak
          進捗 =
            progressList activityNames
          日別の内容 =
            simplifiedDailyReports reports
          今週実績 =
            weeklyActivity activityNames activityNotes
      }

    let generateAsync config (dataContext: IDataContext) date =
      async {
        let! dateRange = dateRangeFromDateAsync dataContext date
        let! dailyReports = dailyReportsAsync dataContext dateRange
        let dateRange = minimalDateRange dailyReports
        let staff = staff config
        let weeklyReport = weeklyReport staff dailyReports
        do! dataContext.WeeklyReports.AddOrUpdateAsync(dateRange, weeklyReport)
        dataContext.WeeklyReports.Open(dateRange)
      }

  /// 週報を生成して開く。
  let generateAsync = GenerateFromDailyReportsFunction.generateAsync

  module internal ConvertToExcelXmlFunction =
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

    let dayRowsFromDailyReport (dailyReport: SimplifiedDailyReport) =
      [|
        let date = dailyReport.日付 |> Date.toDateTime
        for (i, work) in dailyReport.作業実績 |> Array.indexed do
          let dateString =
            if i = 0 then date.ToString("MM/dd") else ""
          let dow =
            if i = 0 then date.DayOfWeek |> DayOfWeek.toKanji else ""
          let duration =
            work.工数 |> sprintf "%.2f"
          yield DayRow.Create(dateString, dow, work.案件, work.内容, duration)
      |]

    let dayRowsFromWeeklyReport (wr: WeeklyReport) =
      wr.日別の内容
      |> Array.collect dayRowsFromDailyReport
      |> Array.tailpad 10 DayRow.Empty

    let toExcelXml (wr: WeeklyReport) =
      let dailyWorkXml =
        wr
        |> dayRowsFromWeeklyReport
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
              "{{所属部署}}" -->
                wr.担当者.所属部署
              "{{担当者名}}" -->
                wr.担当者.名前
              "{{今週活動}}" -->
                (wr.今週の主な活動 |> Xml.escape)
              "{{進捗}}" -->
                (wr.進捗 |> Xml.escape)
              "{{日別の内容}}" -->
                dailyWorkXml
              "{{今週実績}}" -->
                (wr.今週実績 |> Xml.escape)
              "{{来週予定}}" -->
                (wr.来週予定 |> Xml.escape)
              "{{その他}}" -->
                (wr.その他 |> Xml.escape)
            |]
      xml

    /// 指定された日付に対応する週報をエクセルファイルに変換して開く。
    let convertToExcelAsync (dataContext: IDataContext) date =
      async {
        let! dateRange = dateRangeFromDateAsync dataContext date
        let! weeklyReport = dataContext.WeeklyReports.FindAsync(dateRange)
        match weeklyReport with
        | ParsableEntry (_, weeklyReport) ->
          let excelXml = weeklyReport |> toExcelXml
          do! dataContext.WeeklyReportExcels.AddOrUpdateAsync(dateRange, excelXml)
          dataContext.WeeklyReportExcels.Open(dateRange)
        | UnparsableEntry (_, e) ->
          return exn("週報の解析に失敗しました。", e) |> raise
        | UnexistingParsableEntry ->
          return "エクセルファイルに変換する対象の週報がありません。" |> failwith
      }

  let convertToExcelAsync = ConvertToExcelXmlFunction.convertToExcelAsync
