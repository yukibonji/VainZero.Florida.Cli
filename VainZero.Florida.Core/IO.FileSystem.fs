namespace VainZero.IO

open System
open System.IO

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module File =
  let writeAllTextAsync (content: string) (filePath: string) =
    async {
      use stream = File.OpenWrite(filePath)
      stream.SetLength(0L)
      use writer = new StreamWriter(stream)
      return! writer.WriteAsync(content) |> Async.AwaitTask
    }

  let readAllTextAsync (filePath: string) =
    File.OpenText(filePath).ReadToEndAsync() |> Async.AwaitTask
