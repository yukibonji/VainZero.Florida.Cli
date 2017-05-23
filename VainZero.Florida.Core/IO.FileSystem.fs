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

  /// Tries to open a text file (utf-8) and read the content asynchronously.
  /// Returns None if the file not found.
  let tryReadAllTextAsync (filePath: string) =
    async {
      try
        if File.Exists(filePath) then
          let! content = readAllTextAsync filePath
          return Some content
        else
          return None
      with
      | :? FileNotFoundException
      | :? DirectoryNotFoundException ->
        return None
    }
