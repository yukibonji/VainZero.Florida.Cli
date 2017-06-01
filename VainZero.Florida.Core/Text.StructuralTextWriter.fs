namespace VainZero.Text

open System
open System.IO
open System.Text

type StructuralTextWriter(writer: TextWriter) =
  let indent = ref 0

  let createIndent () =
    String(' ', !indent * 2)

  let unindentDisposable =
    { new IDisposable with
        override this.Dispose() =
          indent |> decr
    }

  member this.AddIndent() =
    indent |> incr
    unindentDisposable

  member this.WriteLine(str: string) =
    if !indent = 0 then
      writer.WriteLine(str)
    else
      let indent = createIndent ()
      let rec loop canIndent i =
        if i < str.Length then
          let c = str.[i]
          if c = '\r' || c = '\n' then
            writer.Write(c)
            loop true (i + 1)
          else
            if canIndent then writer.Write(indent)
            writer.Write(c)
            loop false (i + 1)
      loop true 0
      writer.WriteLine()
