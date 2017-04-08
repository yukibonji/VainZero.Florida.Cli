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
  let createCommand args =
    if args |> Array.isEmpty then
      Arguments.parser.PrintUsage() |> Command.printUsage
      printfn "コマンドライン引数を入力してください:"
      match Console.ReadLine() with
      | null ->
        Command.Empty
      | line ->
        Arguments.parse (line |> String.splitBySpaces)
    else
      Arguments.parse args

  let runAsync args =
    async {
      let command = createCommand args
      let notifier = ConsoleNotifier() :> INotifier
      let config = Config.load notifier
      let database = FileSystemDatabase(DirectoryInfo(config.RootDirectory)) :> IDatabase
      use dataContext = database.Connect()
      return! Command.executeAsync config notifier dataContext command
    }

  [<EntryPoint>]
  let main args =
    match runAsync args |> Async.RunSynchronously with
    | Ok ((), _) ->
      0 // success
    | Bad errors ->
      eprintfn "ERROR! %s" (errors |> String.concatWithLineBreak)
      1 // error
