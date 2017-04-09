namespace VainZero.Florida

open System
open Chessie.ErrorHandling
open VainZero.Florida
open VainZero.Florida.Configurations
open VainZero.Florida.Data
open VainZero.Florida.Reports

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Command =
  let toHelp =
    function
    | Command.Empty ->
      "何もしません。"
    | Command.Usage _ ->
      "使い方を表示します。"
    | Command.DailyReportCreate _ ->
      "日報の雛形を生成します。"
    | Command.DailyReportSendMail _ ->
      "日報を送信します。"
    | Command.WeeklyReportCreate _ ->
      "週報の雛形を生成します。"
    | Command.WeeklyReportConvertToExcel _ ->
      "週報をエクセル形式に変換します。"

  let printUsage usage =
    printfn "%s" usage

  let executeAsync config notifier dataContext command =
    async {
      match command with
      | Command.Empty ->
        return ok ()
      | Command.Usage usage ->
        printUsage usage
        return ok ()
      | Command.DailyReportCreate date ->
        do! DailyReport.scaffoldAsync dataContext date
        return ok ()
      | Command.DailyReportSendMail date ->
        return! DailyReport.submitAsync config notifier dataContext date
      | Command.WeeklyReportCreate date ->
        do! WeeklyReport.generateAsync config dataContext date
        return ok ()
      | Command.WeeklyReportConvertToExcel date ->
        return! WeeklyReport.convertToExcelAsync dataContext date
    }

  let tryRecommendAsync (config: Config) (dataContext: IDataContext) date =
    async {
      let! dailyReport = dataContext.DailyReports.FindAsync(date)

      // 日報がまだなければ、日報の生成をおすすめする。
      if dailyReport |> Option.isNone then
        return Command.DailyReportCreate date |> Some

      // 終業が近ければ、日報の送信をおすすめする。
      else if date.TimeOfDay > TimeSpan(16, 30, 0) then
        return Command.DailyReportSendMail date |> Some

      // 週例会議の日なら、週報関連の作業をおすすめする。
      else if date.DayOfWeek = config.WeeklyReportConfig.MeetingDay then
        let! dateRange = WeeklyReport.dateRangeFromDateAsync dataContext date
        let! report = dataContext.WeeklyReports.FindAsync dateRange
        match report with
        | None ->
          return Command.WeeklyReportCreate date |> Some
        | Some _ ->
          return Command.WeeklyReportConvertToExcel date |> Some
      else
        return None
    }
