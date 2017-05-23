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
      use stringWriter = new StringWriter()
      let writer = StructuralTextWriter(stringWriter)
      writer.WriteLine("以下の設定で日報を送信します:")
      let () =
        use indentation = writer.AddIndent()
        let stringFromSeq xs = "[" + (xs |> Seq.map string |> String.concat "; ") + "]"
        writer.WriteLine("TO: " + stringFromSeq message.Destination.Tos)
        writer.WriteLine("CC: " + stringFromSeq message.Destination.CCs)
        writer.WriteLine("BCC: " + stringFromSeq message.Destination.Bccs)
        writer.WriteLine("本文: |")
        use indentation = writer.AddIndent()
        writer.WriteLine(message.Body)
      stringWriter.ToString() + "よろしいですか？"

    let password (submitConfig: DailyReportSubmitConfig) notifier =
      submitConfig.Password |> Option.getOrElse
        (fun () -> (notifier: INotifier).GetPassword("メールアカウントのパスワード"))

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
            let password = password submitConfig notifier
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
