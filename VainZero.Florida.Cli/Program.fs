namespace VainZero.Florida

open System
open System.Diagnostics
open System.IO
open Argu
open FSharpKit.ErrorHandling
open FsYaml
open VainZero.Misc
open VainZero.Florida.Configurations
open VainZero.Florida.Data
open VainZero.Florida.Net.Mail
open VainZero.Florida.Reports
open VainZero.Florida.UI.Notifications

module Program =
  let readCommandsFromConsole () =
    Arguments.parser.PrintUsage() |> Command.printUsage
    printfn "コマンドライン引数を入力してください:"
    match Console.ReadLine() with
    | null ->
      Array.empty
    | line ->
      Arguments.parse (line |> String.splitBySpaces) |> Array.singleton

  let createCommandsAsync config dataContext args =
    async {
      if args |> Array.isEmpty then
        let! commands = Command.recommendAsync config dataContext DateTime.Now
        if commands |> Array.isEmpty then
          return readCommandsFromConsole ()
        else
          printfn "次のコマンドがおすすめです:"
          for command in commands do
            printfn "  - %s" (command |> Command.toHelp)
          if Console.readYesNo "これを実行しますか？" then
            return commands
          else
            return readCommandsFromConsole ()
      else
        return Arguments.parse args |> Array.singleton
    }

  let runAsync args =
    async {
      try
        let smtpService = SmtpService.create ()
        let notifier = ConsoleNotifier() :> INotifier
        let! config = Config.loadAsync ()
        let database = FileSystemDatabase(DirectoryInfo(config.RootDirectory)) :> IDatabase
        use dataContext = database.Connect()
        let! commands = createCommandsAsync config dataContext args
        do! Command.executeManyAsync config notifier dataContext smtpService commands
        return Ok ()
      with
      | e ->
        eprintfn "ERROR:"
        eprintfn "%s" (e |> string)
        if args |> Array.isEmpty then
          Console.ReadKey() |> ignore
        return e |> string |> Error
    }

  [<EntryPoint>]
  let main args =
    match runAsync args |> Async.RunSynchronously with
    | Ok () ->
      0 // success
    | Error error ->
      1 // error
