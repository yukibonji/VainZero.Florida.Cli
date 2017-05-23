namespace VainZero.Florida.Reports

open System
open System.IO
open System.Net
open FSharpKit.ErrorHandling
open VainZero.Collections
open VainZero.Misc
open VainZero.Text
open FsYaml
open VainZero.Florida.Configurations
open VainZero.Florida.Data
open VainZero.Florida.Net.Mail
open VainZero.Florida.UI.Notifications

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module DailyReport =
  let create works plan note =
    {
      作業実績 =
        works
      翌営業日の予定 =
        plan
      その他 =
        note
      CC =
        None
    }

  let empty =
    create [||] "" None

  /// 指定された日付の日報の雛形を生成して、開く。
  let scaffoldAsync (dataContext: IDataContext) date =
    async {
      do! dataContext.DailyReports.ScaffoldAsync(date)
      dataContext.DailyReports.Open(date)
    }

  module internal Submit =
    let destination (submitConfig: DailyReportSubmitConfig) (report: DailyReport) =
      let tos =
        submitConfig.To |> Array.map MailAddress
      let ccs =
        Array.append
          submitConfig.CC
          (report.CC |> Option.getOr Array.empty)
        |> Array.map MailAddress
      let bccs = 
        submitConfig.Bcc |> Array.map MailAddress
      MailDestination(tos, ccs, bccs)

    let subject (submitConfig: DailyReportSubmitConfig) (date: DateTime) =
      sprintf "[日報] %d/%d %s" date.Month date.Day submitConfig.SenderName

    /// メールの文面を作成する。
    let content (submitConfig: DailyReportSubmitConfig) yaml =
      [|
        submitConfig.Header
        Some yaml
        submitConfig.Footer
      |]
      |> Array.choose id
      |> String.concatWithLineBreak

    /// 送信前の確認メッセージを構築する。
    let confirmationMessage (message: MailMessage) =
      let stringifyAddresses ss =
        "[" + (ss |> Seq.map string |> String.concat "; ") + "]"
      sprintf """
以下の設定で日報を送信します:
  TO: %s
  CC: %s
  BCC: %s
  本文: |
%s
"""
        (stringifyAddresses message.Destination.Tos)
        (stringifyAddresses message.Destination.CCs)
        (stringifyAddresses message.Destination.Bccs)
        message.Body

    let password (submitConfig: DailyReportSubmitConfig) =
      submitConfig.Password |> Option.getOrElse
        (fun () ->
          printf "Password: "
          Console.ReadLine() // TODO: INotifier 経由で起動する。
        )

    let submitAsync config notifier dataContext smtpService date =
      async {
        let! report = (dataContext: IDataContext).DailyReports.FindAsync(date)
        let submitConfig = (config: Config).DailyReportSubmitConfig
        match (report, submitConfig) with
        | (ParsableEntry (yaml, report), Some submitConfig) ->
          let server = submitConfig.SmtpServer
          let sender = MailAddress(submitConfig.SenderAddress, submitConfig.SenderName)
          let subject = subject submitConfig date
          let content = content submitConfig yaml
          let destination = destination submitConfig report
          let message = MailMessage(sender, subject, content, destination)
          if (notifier: INotifier).Confirm(confirmationMessage message) then
            let password = password submitConfig
            let credential = NetworkCredential(submitConfig.SenderAddress, password)
            do! (smtpService: ISmtpService).SendAsync(server, credential, message)
        | (UnparsableEntry (_, e), _) ->
          return! exn("日報の解析に失敗しました。", e) |> raise
        | (UnexistingParsableEntry, _) ->
          return! "日報がありません。" |> failwith
        | (_, None) ->
          return! "日報のメール送信の設定がありません。" |> failwith
      }

  let submitAsync = Submit.submitAsync
