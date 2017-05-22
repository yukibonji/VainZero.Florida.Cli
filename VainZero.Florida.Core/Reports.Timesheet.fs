namespace VainZero.Florida.Reports

open System
open System.IO
open System.Collections.Generic
open FSharpKit.ErrorHandling
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
  module ConvertToExcelXml =
    type WorkTime =
      {
        FirstTime:
          TimeSpan
        EndTime:
          TimeSpan
        Recess:
          TimeSpan
      }
    with
      static member Create(firstTime, endTime, recess) =
        {
          FirstTime =
            firstTime
          EndTime =
            endTime
          Recess =
            recess
        }

      member this.Duration =
        this.EndTime - this.FirstTime - this.Recess

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module WorkTime =
      let ofTimeSheetItem (item: TimeSheetItem) =
        match (item.開始時刻, item.終了時刻, item.休憩時間) with
        | (Some firstTime, Some endTime, Some recess) ->
          WorkTime.Create(firstTime, endTime, recess) |> Some
        | _ ->
          None

    type DateRow =
      int * option<WorkTime> * string

    let dateRows (month: DateTime) (timeSheet: TimeSheet): array<DateRow> =
      let map = timeSheet |> Seq.map (|KeyValue|) |> Map.ofSeq
      [|
        for day in 1..31 do
          match map |> Map.tryFind day with
          | Some item ->
            let note = item.備考 |> Option.getOr ""
            match item |> WorkTime.ofTimeSheetItem with
            | Some workTime ->
              yield (day, Some workTime, note)
            | None ->
              yield (day, None, note)
          | None ->
            yield (day, None, "")
      |]

    let excelXmlFromDateRow (month: DateTime) (day, workTime, note) =
      let date = month.AddDays(float (day - 1))
      match workTime with
      | Some workTime ->
        XmlTemplate.timeSheetWorkingRow |> String.replaceEach
          [|
            "{{日付}}" -->
              date.ToString("yyyy-MM-dd")
            "{{日}}" -->
              string day
            "{{開始時刻}}" -->
              workTime.FirstTime.ToString("c")
            "{{終了時刻}}" -->
              workTime.EndTime.ToString("c")
            "{{休憩時間}}" -->
              workTime.Recess.ToString("c")
            "{{勤務時間}}" -->
              workTime.Duration.ToString("c")
            "{{備考}}" -->
              note
          |]
      | None ->
        XmlTemplate.timeSheetEmptyRow |> String.replaceEach
          [|
            "{{日付}}" -->
              date.ToString("yyyy-MM-dd")
            "{{日}}" -->
              string day
            "{{備考}}" -->
              note
          |]

    let excelXml (config: Config) (month: DateTime) timeSheet =
      XmlTemplate.timeSheet |> String.replaceEach
        [|
          "{{日付}}" -->
            month.ToString("yyyy-MM-dd")
          "{{名前}}" -->
            (config.UserName |> Option.getOr "")
          "{{行}}" -->
            (
              dateRows month timeSheet
              |> Array.map (excelXmlFromDateRow month)
              |> String.concatWithLineBreak
            )
        |]

    let convertToExcelXmlAsync (dataContext: IDataContext) config month =
      async {
        let! timeSheet = dataContext.TimeSheets.FindAsync(month)
        match timeSheet with
        | Some timeSheet ->
          let xml = excelXml config month timeSheet
          do! dataContext.TimeSheetExcels.AddOrUpdateAsync(month, xml)
          return Result.Ok ()
        | None ->
          return Result.Error (sprintf "%d月分の勤務表がありません。" month.Month)
      }

  let convertToExcelXmlAsync = ConvertToExcelXml.convertToExcelXmlAsync

  let convertToExcelXmlAndOpenAsync (dataContext: IDataContext) config month =
    AsyncResult.build {
      do! convertToExcelXmlAsync dataContext config month
      dataContext.TimeSheetExcels.Open(month)
    }
