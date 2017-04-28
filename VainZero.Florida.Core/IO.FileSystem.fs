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
    async {
      use stream = File.OpenText(filePath)
      return! stream.ReadToEndAsync() |> Async.AwaitTask
    }

  let tryReadAllTextAsync (filePath: string) =
    async {
      try
        if File.Exists(filePath) then
          let! content = readAllTextAsync filePath
          return Ok content
        else
          let message = sprintf "File not found: '%s'." filePath
          return FileNotFoundException(message, filePath) :> exn |> Error
      with
      | e ->
        return e |> Error
    }
