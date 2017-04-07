namespace VainZero.Text

module Regex =
  open System.Text.RegularExpressions

  let replace pattern (replacement: string) input =
    Regex.Replace(input, pattern, replacement)

module Xml =
  open System.Security

  let escape (unescaped: string): string =
    [|"\r\n"; "\r"; "\n"|]
    |> Array.fold
      (fun linebreak unescaped -> unescaped.Replace(linebreak, "&#10;"))
      (SecurityElement.Escape(unescaped))
