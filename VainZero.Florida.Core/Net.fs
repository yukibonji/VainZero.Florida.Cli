namespace VainZero.Net

open VainZero.Misc

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MailAddress =
  open System
  open System.Net
  open System.Net.Mail

  let ofString (address: string) =
    MailAddress(address)

  let displayName (address: MailAddress) =
    address.DisplayName

  /// Creates a string in the format of "display-name <mail-address>".
  let nameAddress (address: MailAddress) =
    let namePart =
      if String.IsNullOrWhiteSpace(address |> displayName)
      then ""
      else address.DisplayName + " "
    let addressPart =
      sprintf "<%s>" address.Address
    namePart + addressPart

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MailAddressCollection =
  open System.Net.Mail

  let addRange (xs: seq<MailAddress>) (collection: MailAddressCollection) =
    for x in xs do
      collection.Add(x)

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module SmtpClient =
  open System
  open System.Net
  open System.Net.Mail

  let create (host: string) (userName: string) (password: string) =
    new SmtpClient
      ( Host = host
      , Credentials = NetworkCredential(userName, password)
      )

  let send sender (tos, ccs, bccs) subject body (client: SmtpClient) =
    use message =
      new MailMessage
        ( Subject = subject
        , Body = body
        , From = sender
        )
    message.To |> MailAddressCollection.addRange tos
    message.CC |> MailAddressCollection.addRange ccs
    message.Bcc |> MailAddressCollection.addRange bccs
    client.Send(message)
