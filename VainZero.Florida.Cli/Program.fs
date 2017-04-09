namespace VainZero.Florida

open System
open System.Diagnostics
open System.IO
open Argu
open Chessie.ErrorHandling
open FsYaml
open VainZero.Misc
open VainZero.Florida.Configurations
open VainZero.Florida.Data
open VainZero.Florida.Reports
open VainZero.Florida.UI.Notifications

module Program =
  let readCommandFromConsole () =
    Arguments.parser.PrintUsage() |> Command.printUsage
    printfn "コマンドライン引数を入力してください:"
    match Console.ReadLine() with
    | null ->
      Command.Empty
    | line ->
      Arguments.parse (line |> String.splitBySpaces)

  let createCommandAsync config dataContext args =
    async {
      if args |> Array.isEmpty then
        let! command = Command.tryRecommendAsync config dataContext DateTime.Now
        match command with
        | Some command ->
          printfn "次のコマンドがおすすめです:"
          printfn "  - %s" (command |> Command.toHelp)
          if Console.readYesNo "これを実行しますか？" then
            return command
          else
            return readCommandFromConsole ()
        | None ->
          return readCommandFromConsole ()
      else
        return Arguments.parse args
    }

  let runAsync args =
    async {
      try
        let notifier = ConsoleNotifier() :> INotifier
        let config = Config.load notifier
        let database = FileSystemDatabase(DirectoryInfo(config.RootDirectory)) :> IDatabase
        use dataContext = database.Connect()
        let! command = createCommandAsync config dataContext args
        return! Command.executeAsync config notifier dataContext command
      with
      | e ->
        return e |> string |> fail
    }

  [<EntryPoint>]
  let main args =
    match runAsync args |> Async.RunSynchronously with
    | Ok ((), _) ->
      0 // success
    | Bad errors ->
      eprintfn "ERROR! %s" (errors |> String.concatWithLineBreak)
      1 // error
