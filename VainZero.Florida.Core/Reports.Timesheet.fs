namespace VainZero.Florida.Reports

open System
open System.IO
open System.Collections.Generic
open FSharpKit.ErrorHandling
open OfficeOpenXml
open VainZero.Florida
open VainZero.Florida.Configurations
open VainZero.Florida.Data
open VainZero.Misc

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module TimeSheetItem =
  let empty =
    {
      開始時刻 =
        None
      終了時刻 =
        None
      休憩時間 =
        None
      備考 =
        None
    }

  let create firstTime duration recess =
    {
      開始時刻 =
        Some firstTime
      終了時刻 =
        Some (firstTime + duration + recess)
      休憩時間 =
        Some recess
      備考 =
        None
    }

  let fromDailyReport (config: TimeSheetConfig) (report: DailyReport) =
    let duration =
      report.作業実績 |> Seq.sumBy (fun work -> work.工数) |> TimeSpan.FromHours
    create config.DefaultFirstTime duration config.DefaultRecess

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module TimeSheet =
  let create (month: DateTime) =
    DateTime.monthDates month
    |> Seq.map (fun date -> KeyValuePair(date.Day, TimeSheetItem.empty))
    |> Seq.toArray

  /// Update an element.
  let update day item (timeSheet: TimeSheet): TimeSheet =
    timeSheet |> Array.map
      (fun (KeyValue (day', _) as kv) ->
        if day' = day
        then KeyValuePair(day, item)
        else kv
      )

  let createOrUpdateAsync (dataContext: IDataContext) (config: TimeSheetConfig) (date: DateTime) =
    async {
      let! report = dataContext.DailyReports.FindAsync(date)
      match report with
      | Ok (_, report) ->
        let item = TimeSheetItem.fromDailyReport config report
        let! timeSheet = dataContext.TimeSheets.FindAsync(date)
        let timeSheet =
          timeSheet
          |> Option.getOrElse (fun () -> create date)
          |> update date.Day item
        do! dataContext.TimeSheets.AddOrUpdateAsync(date, timeSheet)
        return Ok ()
      | Error e ->
        let error =
          match e with
          | :? FsYaml.FsYamlException as e ->
            sprintf "日報を解析できません: %s" e.Message
          | :? FileNotFoundException ->
            "勤務表の更新には日報が必要です。"
          | e ->
            e |> string
        return Error error
    }

  [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
  module ConvertToExcel =
    let (-->) x y = (x, y)

    let month =
      let now = DateTime.Now
      now.AddDays(float (1 - now.Day))

    type DateRow =
      {
        日付:
          DateTime
        勤務時間:
          option<TimeSpan * TimeSpan>
        休憩時間:
          TimeSpan
        備考:
          string
      }
    with
      static member Create(date, workTime, restTime, note) =
        {
          日付 =
            date
          勤務時間 =
            workTime
          休憩時間 =
            restTime
          備考 =
            note
        }

    let dateRows (timeSheet: TimeSheet) (month: DateTime) =
      let map = timeSheet |> Seq.map (|KeyValue|) |> Map.ofSeq
      [|
        for day in 1..31 do
          let date = month.AddDays(float (day - 1))
          match map |> Map.tryFind day with
          | Some item ->
            let workTime =
              match item with
              | { 開始時刻 = Some firstTime; 終了時刻 = Some endTime } ->
                Some (firstTime, endTime)
              | _ ->
                None
            let restTime =
              item.休憩時間 |> Option.getOr TimeSpan.Zero
            let note =
              item.備考 |> Option.getOr ""
            yield DateRow.Create(date, workTime, restTime, note)
          | None ->
            yield DateRow.Create(date, None, TimeSpan.Zero, "")
      |]

    let excelXmlFromDateRow (dateRow: DateRow) =
      match dateRow.勤務時間 with
      | Some (firstTime, endTime) ->
        XmlTemplate.timeSheetWorkingRow |> String.replaceEach
          [|
            "{{日付}}"
              --> dateRow.日付.ToString("yyyy-MM-dd")
            "{{日}}"
              --> string dateRow.日付.Day
            "{{開始時刻}}"
              --> firstTime.ToString("c")
            "{{終了時刻}}"
              --> endTime.ToString("c")
            "{{休憩時間}}"
              --> dateRow.休憩時間.ToString("c")
            "{{勤務時間}}"
              --> (endTime - firstTime).ToString("c")
            "{{備考}}"
              --> dateRow.備考
          |]
      | None ->
        XmlTemplate.timeSheetEmptyRow |> String.replaceEach
          [|
            "{{日付}}"
              --> dateRow.日付.ToString("yyyy-MM-dd")
            "{{日}}"
              --> string dateRow.日付.Day
            "{{備考}}"
              --> dateRow.備考
          |]

    let excelXml (config: Config) timeSheet month =
      let dateRowXmls =
        dateRows timeSheet month |> Array.map excelXmlFromDateRow |> String.concatWithLineBreak
      XmlTemplate.timeSheet
      |> String.replaceEach
        [|
          "{{日付}}"
            --> month.ToString("yyyy-MM-dd")
          "{{名前}}"
            --> (config.UserName |> Option.getOr "")
          "{{行}}"
            --> dateRowXmls
        |]

    let convertToExcelXml (database: IDatabase) config month =
      

  let convertToExcel = ConvertToExcel.convertToExcel
