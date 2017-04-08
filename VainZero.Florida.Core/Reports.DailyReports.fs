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
open VainZero.Florida.Data
open VainZero.Florida.UI.Notifications

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module DailyReports =
  module internal Submit =
    let destinations (submitConfig: DailyReportSubmitConfig) (report: DailyReport) =
      let tos =
        submitConfig.To |> Array.map MailAddress.ofString
      let ccs =
        Array.append
          submitConfig.CC
          (report.CC |> Option.getOr Array.empty)
        |> Array.map MailAddress.ofString
      let bccs = 
        submitConfig.Bcc |> Array.map MailAddress.ofString
      (tos, ccs, bccs)

    let subject (submitConfig: DailyReportSubmitConfig) (date: DateTime) =
      sprintf "[日報] %d/%d %s" date.Month date.Day submitConfig.SenderName

    /// メールの文面を作成する。
    let content (submitConfig: DailyReportSubmitConfig) report =
      let yaml = report |> Yaml.dump
      [|
        submitConfig.Header
        Some yaml
        submitConfig.Footer
      |]
      |> Array.choose id
      |> String.concatWithLineBreak

    /// 送信前の確認メッセージを構築する。
    let confirmationMessage (tos, ccs, bccs) content =
      let stringifyAddresses ss =
        "[" + (ss |> Seq.map MailAddress.nameAddress |> String.concat "; ") + "]"
      sprintf """
以下の設定で日報を送信します:
  TO: %s
  CC: %s
  BCC: %s
  本文: |
%s
"""
        (stringifyAddresses tos)
        (stringifyAddresses ccs)
        (stringifyAddresses bccs)
        content

    let password (submitConfig: DailyReportSubmitConfig) =
      submitConfig.Password |> Option.getOrElse
        (fun () ->
          printf "Password: "
          Console.ReadLine() // TODO: INotifier 経由で起動する。
        )

    let submit submitConfig destinations subject content password =
      let host = (submitConfig: DailyReportSubmitConfig).Host
      use smtpClient = SmtpClient.create host submitConfig.SenderAddress password
      let sender = MailAddress(submitConfig.SenderAddress, submitConfig.SenderName)
      smtpClient |> SmtpClient.send sender destinations subject content

    let submitAsync (config: Config) (notifier: INotifier) dataContext date =
      async {
        let! report = (dataContext: IDataContext).DailyReports.FindAsync(date)
        let submitConfig = config.DailyReportSubmitConfig
        match (report, submitConfig) with
        | (Some report, Some submitConfig) ->
          let destinations = destinations submitConfig report
          let subject = subject submitConfig date
          let content = content submitConfig report
          let confirmationMessage = confirmationMessage destinations content
          if (notifier: INotifier).Confirm(confirmationMessage) then
            let password = password submitConfig
            submit submitConfig destinations subject content password
          return ok ()
        | (None, _) ->
          return "日報がありません。" |> fail
        | (_, None) ->
          return "日報のメール送信の設定がありません。" |> fail
      }

  let submitAsync = Submit.submitAsync
