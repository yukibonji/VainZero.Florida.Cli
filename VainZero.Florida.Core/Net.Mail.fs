namespace VainZero.Florida.Net.Mail

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MailAddressCollection =
  open System.Net.Mail

  let addRange (xs: seq<MailAddress>) (collection: MailAddressCollection) =
    for x in xs do
      collection.Add(x)

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module SmtpService =
  let create () =
    { new ISmtpService with
        override this.SendAsync(server, credential, message) =
          use client =
            new System.Net.Mail.SmtpClient
              ( Host = server.Name
              , Port = server.Port
              , Credentials = credential
              )
          use mail =
            new System.Net.Mail.MailMessage
              ( Subject = message.Subject
              , Body = message.Body
              , From = message.Sender
              )
          mail.To |> MailAddressCollection.addRange message.Destination.Tos
          mail.CC |> MailAddressCollection.addRange message.Destination.CCs
          mail.Bcc |> MailAddressCollection.addRange message.Destination.Bccs
          client.SendMailAsync(mail) |> Async.AwaitTask
    }
