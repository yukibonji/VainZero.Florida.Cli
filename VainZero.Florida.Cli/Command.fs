namespace VainZero.Florida

open System
open Chessie.ErrorHandling
open VainZero.Florida.Reports

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Command =
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
