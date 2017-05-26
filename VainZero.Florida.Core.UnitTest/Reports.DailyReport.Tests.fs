namespace VainZero.Florida.Reports

open System
open System.Collections.Generic
open Persimmon
open VainZero.Florida
open VainZero.Florida.Data
open VainZero.Florida.Configurations
open VainZero.Florida.Net.Mail
open VainZero.Florida.UI.Notifications

module ``test DailyReport`` =
  module ``test SubmitFunction`` =

    type TestNotifier(confirmations: seq<bool>, passwords: seq<string>) =
      member val Confirmations = Queue(confirmations)
      member val Passwords = Queue(passwords)

      interface INotifier with
        override this.NotifyWarning(_) = ()

        override this.Confirm(_) =
          this.Confirmations.Dequeue()

        override this.GetPassword(_) =
          this.Passwords.Dequeue()

    type TestSmtpService() =
      member val SendLog = ResizeArray()

      interface ISmtpService with
        override this.SendAsync(server, credential, message) =
          async {
            this.SendLog.Add((server, credential, message))
          }

    let submitConfig =
      {
        SmtpServer =
          {
            Name =
              "smtp.example.com"
            Port =
              25
          }
        SenderName =
          "J.Doe"
        SenderAddress =
          "john-doe@example.com"
        Password =
          None
        To =
          [|"config-to@example.com"|]
        CC =
          [|"config-cc@example.com"|]
        Bcc =
          [|
            "config-bcc1@example.com"
            "config-bcc2@example.com"
          |]
        Header =
          Some "Header."
        Footer =
          Some "Footer."
      }

    let config =
      { Config.test with DailyReportSubmitConfig = Some submitConfig }

    let dailyReport =
      DailyReport.create
        [|
          Work.create "Florida" "Implementation" 1.0 (Some "Hard work.")
          Work.create "Florida" "Testing" 2.0 None
        |]
        "Next plan."
        None

    let ``test destination`` =
      test {
        let dailyReport =
          { dailyReport with
              CC = Some [|"report-cc@example.com"|]
          }
        let destination = DailyReport.Submit.destination submitConfig dailyReport
        do!
          destination.Tos |> Array.map string |> is [|"config-to@example.com"|]
        do!
          destination.CCs |> Array.map string |> is
            [|
              "config-cc@example.com"
              "report-cc@example.com"
            |]
        do!
          destination.Bccs |> Array.map string |> is
            [|
              "config-bcc1@example.com"
              "config-bcc2@example.com"
            |]
      }

    let ``test content`` =
      test {
        let yaml = "BODY\r\n"
        do!
          DailyReport.Submit.content submitConfig yaml
          |> is "Header.\r\nBODY\r\n\r\nFooter."

        // No header and footer.
        do!
          let submitConfig = { submitConfig with Header = None; Footer = None } in
          DailyReport.Submit.content submitConfig yaml
          |> is "BODY\r\n"
      }

    let ``test confirmationMessage`` =
      test {
        let submitConfig = { submitConfig with Header = None; Footer = None }
        let date = DateTime(2017, 4, 1)
        let yaml = "BODY"
        let message = DailyReport.Submit.message submitConfig date yaml dailyReport
        do! DailyReport.Submit.confirmationMessage message |> is
              """以下の設定で日報を送信します:
  TO: [config-to@example.com]
  CC: [config-cc@example.com]
  BCC: [config-bcc1@example.com; config-bcc2@example.com]
  本文: |
    BODY
よろしいですか？"""
      }

    module ``test password`` =
      let password = "password"

      let ``it invokes INotifier.GetPassword`` =
        test {
          let notifier = TestNotifier([||], [|password|])
          do! DailyReport.Submit.password submitConfig (notifier :> INotifier) |> is password
        }

      let ``it prefers default password`` =
        test {
          let notifier = TestNotifier([||], [||])
          let submitConfig = { submitConfig with Password = Some password }
          do! DailyReport.Submit.password submitConfig (notifier :> INotifier) |> is password
        }

    module ``test submitAsync`` =
      let notifier () = TestNotifier([|true|], [|"password"|]) :> INotifier
      let smtpService () = TestSmtpService() :> ISmtpService
      let dataContext () = (MemoryDatabase() :> IDatabase).Connect()
      let date = DateTime(2017, 4, 1)

      let submit config notifier dataContext smtpService date =
        DailyReport.Submit.submitAsync config notifier dataContext smtpService date
        |> sync

      let ``it raises a parse error if unparsable`` =
        test {
          let parseException = FsYaml.FsYamlException("") :> exn

          use dataContext = dataContext ()
          let repo = dataContext.DailyReports :?> MemoryDailyReportRepository
          repo.Dictionary.Add(date, UnparsableEntry (":broken:", parseException))

          let send () = submit config (notifier ()) dataContext (smtpService ()) date
          let! e = trap { it (send ()) }
          do! e.InnerException |> is parseException
        }

      let ``it raises an error if missing`` =
        test {
          use dataContext = dataContext ()
          let send () = submit config (notifier ()) dataContext (smtpService ()) date
          let! (_: exn) = trap { it (send ()) }
          return ()
        }

      let ``it raises an error unless configured`` =
        test {
          use dataContext = dataContext ()
          let config = { config with DailyReportSubmitConfig = None }

          let send () = submit config (notifier ()) dataContext (smtpService ()) date
          let! (_: exn) = trap { it (send ()) }
          return ()
        }

      let dataContextWithReport () =
        let dataContext = dataContext ()
        let repo = dataContext.DailyReports :?> MemoryDailyReportRepository
        repo.Dictionary.Add(date, ParsableEntry ("body", dailyReport))
        dataContext

      let ``it can send a mail`` =
        test {
          use dataContext = dataContextWithReport ()
          let smtpService = smtpService ()
          submit config (notifier ()) dataContext smtpService date
          let logs = (smtpService :?> TestSmtpService).SendLog.ToArray()
          do! logs |> Array.length |> is 1
        }

      let ``it can be cancelled`` =
        test {
          use dataContext = dataContextWithReport ()
          let smtpService = smtpService ()
          let notifier = TestNotifier([|false|], [||]) :> INotifier
          submit config notifier dataContext smtpService date
          let logs = (smtpService :?> TestSmtpService).SendLog.ToArray()
          do! logs |> Array.length |> is 0
        }
