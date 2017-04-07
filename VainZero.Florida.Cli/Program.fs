namespace Reports

open System
open System.Diagnostics
open System.IO
open Chessie.ErrorHandling
open FsYaml

module Program =
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

  let rec procCommandLine args =
    match args with
    | [] ->
        printfn "Input command line:"
        match Console.ReadLine() with
        | null -> ()
        | line -> procCommandLine (line |> String.splitBySpaces |> Array.toList)
    | "wr" :: dateStr :: _ ->
        createWeeklyReport (dateStr |> DateTime.Parse)
    | "wr" :: _ ->
        createWeeklyReport DateTime.Now
    | "excel" :: dateStr :: _ ->
        convertWeeklyReportToExcel (dateStr |> DateTime.Parse)
    | "excel" :: _ ->
        convertWeeklyReportToExcel DateTime.Now
    | "mail" :: dateStr :: _ ->
        DailyReports.sendMail (dateStr |> DateTime.Parse)
    | "mail" :: _ ->
        DailyReports.sendMail DateTime.Now
    | _ ->
        failwithf "Unknown commands: %s" (args |> String.concat " ")

  [<EntryPoint>]
  let main argv =
    let exitCode =
      Console.runApp (fun () -> procCommandLine (argv |> Array.toList))
    in exitCode
