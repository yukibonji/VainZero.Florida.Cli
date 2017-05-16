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
    let convertToExcel () =
      let (-->) x y = (x, y)

      let month =
        let now = DateTime.Now
        now.AddDays(float (1 - now.Day))

      let rows =
        [|
          for i in 1..31 do
            let date = month.AddDays(float (i - 1))
            if i < 4 then
              yield
                XmlTemplate.timeSheetWorkingRow |> String.replaceEach
                  [|
                    "{{日付}}" --> date.ToString("yyyy-MM-dd")
                    "{{日}}" --> string date.Day
                    "{{開始時刻}}" --> "09:30:00"
                    "{{終了時刻}}" --> "18:30:00"
                    "{{休憩時間}}" --> "01:00:00"
                    "{{勤務時間}}" --> "08:00:00"
                  |]
            else
              yield
                XmlTemplate.timeSheetEmptyRow |> String.replaceEach
                  [|
                    "{{日付}}" --> date.ToString("yyyy-MM-dd")
                    "{{日}}" --> string date.Day
                  |]
        |]
      let xml =
        XmlTemplate.timeSheet
        |> String.replace "{{行}}" (rows |> String.concatWithLineBreak)

      File.WriteAllText("x.xml", xml)

  let convertToExcel = ConvertToExcel.convertToExcel
