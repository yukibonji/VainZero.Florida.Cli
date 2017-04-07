namespace VainZero.Florida.Reports

open System
open System.IO
open System.Net.Mail
open Chessie.ErrorHandling
open VainZero.Collections
open VainZero.ErrorHandling
open VainZero.Misc
open VainZero.Net
open VainZero.Text
open FsYaml
open VainZero.Florida.Configurations

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module DailyReports =
  let ofYaml yaml =
    Yaml.load<``日報``> yaml

  let path (date: DateTime) =
    Path.Combine
      ( App.config.ReportsDir
      , string date.Year
      , sprintf "%02d" date.Month
      , sprintf "%02d.yaml" date.Day
      )

  let loadAsync date: Async<option<DayOfWeek * ``日報`` * string>> =
    async {
      let file = FileInfo(date |> path)
      if file.Exists then
        let! yaml = file.OpenText().ReadToEndAsync() |> Async.AwaitTask
        let drepo = yaml |> ofYaml
        return Some (date.DayOfWeek, drepo, yaml)
      else
        return None
    }

  let loadAll dates =
    dates
    |> Array.map (fun date -> async {
        try
          let! repo = loadAsync date
          return repo |> pass
        with
        | e -> return fail (date, e)
        })
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Trial.collectToArray
    |> (function
        | Pass rs -> rs |> Array.choose id
        | Warn (_, msgs)
        | Fail msgs ->
            ( "Failed to load some of daily reports. Error messages are following:"
            :: (msgs |> List.map (fun (date, e) -> sprintf "%d/%d: %s" date.Month date.Day e.Message)))
            |> String.concatWithLineBreak
            |> failwith
        )

  let private sumUpActivities dailyReports =
    let (activityNames, activityNoteList) =
      dailyReports
      |> Array.collect (fun (_, drepo, _) -> drepo.``作業実績``)
      |> Array.map (fun activity ->
          let title = sprintf "%s/%s" (activity.``案件``) (activity.``内容``)
          let noteOpt = activity.``備考``
          in (title, (title, noteOpt))
          )
      |> Array.unzip
    let activityNames =
      activityNames |> Array.unique
    let activityNotes =
      Map.empty |> fold activityNoteList (fun (title, noteOpt) m ->
          match noteOpt with
          | None -> m
          | Some note ->
              Map.appendWith
                (fun l r -> l + Environment.NewLine + r)
                m (Map.singleton title note)
          )
    in (activityNames, activityNotes)

  let private eliminateNotes item =
    { item with ``備考`` = None }

  let toWeeklyReport date =
    let dailyReports =
      date
      |> DateTime.weekDays
      |> loadAll
    let (activityNames, activityNotes) =
      dailyReports |> sumUpActivities
    in
      {
        ``担当者`` =
          {
            ``所属部署`` =
              App.config.Department |> Option.getOr ""
            ``名前`` =
              App.config.UserName |> Option.getOr ""
          }
        ``今週の主な活動`` =
          activityNames |> String.concatWithLineBreak
        ``進捗`` =
          activityNames |> Array.map (fun activityName ->
            sprintf "%s: 0%c" activityName '%'
            )
          |> String.concatWithLineBreak
        ``日別の内容`` =
          dailyReports |> Array.map (fun (dow, drepo, _) ->
            let ``曜日`` = dow |> DayOfWeek.toKanji
            let ``実績`` = drepo.``作業実績`` |> Array.map eliminateNotes
            in (``曜日``, ``実績``)
            )
          |> Map.ofArray
          |> ``日別の内容``.ofMap
        ``今週実績`` =
          activityNames |> Array.choose (fun activityName ->
            activityNotes |> Map.tryFind activityName |> Option.map (fun note ->
              (activityName, note)
              ))
          |> Map.ofArray
          |> Yaml.dump
        ``来週予定`` =
          ""
        ``その他`` =
          ""
      }

  let generateWeeklyReports date =
    let wr = date|> toWeeklyReport
    let data =
      wr |> Yaml.dump
      |> String.replace "\r\n\r\n" "\r\n"   // Workaround the bug YamlDotNet can't handle linebreaks
      |> Regex.replace @"\b(\w+): >-?" "$1: |"
      |> Regex.replace @"\b(\w+): ''" ("$1: |" + Environment.NewLine)
    let path = WeeklyReports.path date
    File.WriteAllText(path, data)

  let sendMail date =
    let (_, dailyReport, yaml) =
      match loadAsync date |> Async.RunSynchronously with
      | Some x -> x
      | None -> failwith "Daily report not found."
    let mail =
      App.config.Mail |> Option.getOrElse (fun () ->
        failwith "Please add Mail value in the settings file."
        )
    let body =
      [
        mail.Header
        Some yaml
        mail.Footer
      ]
      |> List.choose id
      |> String.concatWithLineBreak
    let tos =
      if mail.TOs |> Array.isEmpty
      then failwith "Mail.TOs mustn't be empty."
      else mail.TOs |> Array.map MailAddress.ofString
    let ccs =
      Array.append
        mail.CCs
        (dailyReport.CCs |> Option.getOr Array.empty)
      |> Array.map MailAddress.ofString
    let bccs = 
      mail.BCCs |> Array.map MailAddress.ofString
    let stringizeAddrs ss =
      "[" + (ss |> Seq.map MailAddress.nameAddr |> String.concat "; ") + "]"
    do
      printfn """
You're sending the mail:
  TOs: %s
  CCs: %s
  BCCs: %s
  Body: |
%s"""
        (stringizeAddrs tos)
        (stringizeAddrs ccs)
        (stringizeAddrs bccs)
        body
    if Console.readYesNo "OK?" then
      let password =
        mail.Password |> Option.getOrElse (fun () ->
          printf "Password: "
          Console.ReadLine()
          )
      use smtpClient =
        SmtpClient.create
          mail.Host mail.Addr password
      smtpClient |> SmtpClient.send
        (MailAddress(mail.Addr, mail.Name))
        tos ccs bccs
        (sprintf "日報 %d/%d" date.Month date.Day)
        body
