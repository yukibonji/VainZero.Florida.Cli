namespace VainZero.Florida

open System
open System.Diagnostics
open System.IO
open Argu
open Chessie.ErrorHandling
open FsYaml
open VainZero.Misc
open VainZero.Florida.Configurations
open VainZero.Florida.Reports

module Program =
  let parser = ArgumentParser.Create<Arguments>()

  let createWeeklyReport date =
    let path = WeeklyReports.path date
    if not (File.Exists(path))
      || Console.readYesNo "Weekly report file already exists. Overwrite?"
    then
      DailyReports.generateWeeklyReports date
      Process.Start(path) |> ignore

  let convertWeeklyReportToExcel date =
    WeeklyReports.generateExcel date
    Process.Start("excel", WeeklyReports.excelPath date) |> ignore

  let printUsage (parser: ArgumentParser<_>) =
    printfn "USAGE: %s" (parser.PrintUsage())

  let procCommandLine args =
    let now = DateTime.Now
    let parseResults = parser.ParseCommandLine(args, raiseOnUsage = false)
    if parseResults.IsUsageRequested then
      parseResults.Parser |> printUsage
    else
      match parseResults.GetSubCommand() with
      | Daily_Report parseResults ->
        if parseResults.Contains(<@ DailyReportArguments.New @>) then
          () // TODO: implement
        else if parseResults.Contains(<@ DailyReportArguments.Mail @>) then
          DailyReports.sendMail now
        else
          parseResults.Parser |> printUsage
      | Weekly_Report parseResults ->
        if parseResults.Contains(<@ WeeklyReportArguments.New @>) then
          createWeeklyReport now
        else if parseResults.Contains(<@ WeeklyReportArguments.Excel @>) then
          convertWeeklyReportToExcel now
        else
          parseResults.Parser |> printUsage

  let run args =
    if args |> Array.isEmpty then
      parser |> printUsage
      printfn "コマンドライン引数を入力してください:"
      match Console.ReadLine() with
      | null -> ()
      | line -> procCommandLine (line |> String.splitBySpaces)
    else
      procCommandLine args

  [<EntryPoint>]
  let main args =
    Console.runApp (fun () -> run args)
