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
    let templateFile = System.IO.FileInfo(@"data/time-sheet-excels/template.xlsx")
    let file = templateFile.CopyTo("data/time-sheet-excels/x.xlsx", overwrite = true)
    let date = DateTime.Now

    let convertToExcel () =
      use package = new ExcelPackage(file)
      let sheet = package.Workbook.Worksheets.[1]

      // Headers
      sheet.Cells.[2, 1].Value <- date
      sheet.Cells.[2, 7].Value <- "田中太郎" //config.Name

      // Records
      (*
      let rec loop rowIndex =
        sheet.Cells.[rowIndex, 3].Value <- TimeSpan(9, 30, 0)
        sheet.Cells.[rowIndex, 4].Value <- TimeSpan(18, 30, 0)
        sheet.Cells.[rowIndex, 5].Value <- TimeSpan(1, 0, 0)
        sheet.Cells.[rowIndex, 7].Value <- "Note"
      loop 5
      //*)

      package.Save()

  let convertToExcel = ConvertToExcel.convertToExcel
