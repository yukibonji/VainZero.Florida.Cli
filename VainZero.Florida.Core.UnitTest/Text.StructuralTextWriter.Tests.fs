namespace VainZero.Text

open System
open System.IO
open System.Text
open Persimmon

module ``test StructuralTextWriter`` =
  let ``test AddIndent and WriteLine`` =
    test {
      let stringWriter = new StringWriter(NewLine = "\n")
      let writer = StructuralTextWriter(stringWriter)
      writer.WriteLine("1")
      let () =
        use indentation = writer.AddIndent()
        writer.WriteLine("2")
        writer.WriteLine("3")
        use indentation = writer.AddIndent()
        writer.WriteLine("4\n5")
      do! stringWriter.ToString() |> is "1\n  2\n  3\n    4\n    5\n"
    }
