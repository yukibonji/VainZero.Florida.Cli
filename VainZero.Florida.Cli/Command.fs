namespace VainZero.Florida

open System
open System.IO
open FSharpKit.ErrorHandling
open VainZero.Misc
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
    | Command.TimeSheetUpdate _ ->
      "勤務表を更新します。"
    | Command.TimeSheetExcel _ ->
      "前月の勤務表をエクセル形式に変換します。"
    | Command.DailyReportFinalize _ ->
      "日報を送信し、勤務表を更新します。"

  let printUsage usage =
    printfn "%s" usage

  let executeAsync config notifier dataContext smtpService command =
    async {
      match command with
      | Command.Empty ->
        ()
      | Command.Usage usage ->
        printUsage usage
        Console.ReadKey(intercept = true) |> ignore
      | Command.DailyReportCreate date ->
        return! DailyReport.scaffoldAsync dataContext date
      | Command.DailyReportSendMail date ->
        return! DailyReport.submitAsync config notifier dataContext smtpService date
      | Command.WeeklyReportCreate date ->
        return! WeeklyReport.generateAsync config dataContext date
      | Command.WeeklyReportConvertToExcel date ->
        return! WeeklyReport.convertToExcelAsync dataContext date
      | Command.TimeSheetUpdate date ->
        return! TimeSheet.createOrUpdateAsync dataContext config.TimeSheetConfig date
      | Command.TimeSheetExcel date ->
        let previousMonth = (date |> DateTime.toMonth).AddMonths(-1)
        return! TimeSheet.convertToExcelXmlAndOpenAsync dataContext config previousMonth
      | Command.DailyReportFinalize date ->
        do! DailyReport.submitAsync config notifier dataContext smtpService date
        return! TimeSheet.createOrUpdateAsync dataContext config.TimeSheetConfig date
    }

  let executeManyAsync config notifier dataContext smtpService (commands: array<_>) =
    async {
      for command in commands do
        do! executeAsync config notifier dataContext smtpService command
    }

  let recommendAsync (config: Config) (dataContext: IDataContext) date =
    async {
      let commands = ResizeArray<_>()
      let! dailyReport = dataContext.DailyReports.FindAsync(date)

      // 日報がまだなければ、日報の生成をおすすめする。
      if dailyReport = UnexistingParsableEntry then
        commands.Add(Command.DailyReportCreate date)

      // 終業が近ければ、日報の送信と勤務表の更新をおすすめする。
      else if date.TimeOfDay > TimeSpan(16, 30, 0) then
        commands.Add(Command.DailyReportFinalize date)

      // 週例会議の日なら、週報関連の作業をおすすめする。
      else if date.DayOfWeek = config.WeeklyReportConfig.MeetingDay then
        let! dateRange = WeeklyReport.dateRangeFromDateAsync dataContext date
        let! report = dataContext.WeeklyReports.FindAsync dateRange
        match report with
        | UnexistingParsableEntry ->
          commands.Add(Command.WeeklyReportCreate date)
        | ParsableEntry _
        | UnparsableEntry _ ->
          commands.Add(Command.WeeklyReportConvertToExcel date)

      return commands.ToArray()
    }
