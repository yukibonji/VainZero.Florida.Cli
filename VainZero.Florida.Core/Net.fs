namespace VainZero.Net

open VainZero.Misc

module MailAddress =
  open System
  open System.Net
  open System.Net.Mail

  let ofString (s: string) =
    new MailAddress(s)

  let displayName (addr: MailAddress) =
    addr.DisplayName

  /// Returns a string in the format of "display-name <mail-address>".
  let nameAddr (addr: MailAddress) =
    if String.IsNullOrWhiteSpace(addr |> displayName)
    then ""
    else addr.DisplayName + " "
    + sprintf "<%s>" addr.Address

module MailAddressCollection =
  open System.Net.Mail

  let addRange (xs: seq<MailAddress>) (self: MailAddressCollection) =
    for x in xs do
      self.Add(x)

module SmtpClient =
  open System
  open System.Net
  open System.Net.Mail

  let create (host: string) (userName: string) (password: string) =
    new SmtpClient(Host = host) |> tap (fun self ->
      self.Credentials <-
        new NetworkCredential(userName, password)
      )

  let send sender tos ccs bccs subject body (self: SmtpClient) =
    let msg =
      new MailMessage() |> tap (fun msg ->
        msg.Subject     <- subject
        msg.Body        <- body
        msg.From        <- sender
        msg.To    |> MailAddressCollection.addRange tos
        msg.CC    |> MailAddressCollection.addRange ccs
        msg.Bcc   |> MailAddressCollection.addRange bccs
        )
    in self.Send(msg)
