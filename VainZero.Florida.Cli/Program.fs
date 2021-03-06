﻿namespace VainZero.Florida

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
        let smtpService = SmtpService.create ()
        let notifier = ConsoleNotifier() :> INotifier
        let! config = Config.loadAsync ()
        let database = FileSystemDatabase(DirectoryInfo(config.RootDirectory)) :> IDatabase
        use dataContext = database.Connect()
        let! command = createCommandAsync config dataContext args
        do! Command.executeAsync config notifier dataContext smtpService command
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
