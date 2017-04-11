namespace global

type Result<'x, 'e> =
  | Ok of 'x
  | Error of 'e

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Result =
  let map (f: 'x -> 'y) (result: Result<'x, 'e>): Result<'y, 'e> =
    match result with
    | Ok x ->
      Ok (f x)
    | Error e ->
      Error e

  let mapError (f: 'e -> 'f) (result: Result<'x, 'e>): Result<'x, 'f> =
    match result with
    | Ok x ->
      Ok x
    | Error e ->
      Error (f e)

  let bind (f: 'x -> Result<'y, 'e>) (result: Result<'x, 'e>): Result<'y, 'e> =
    match result with
    | Ok x ->
      f x
    | Error e ->
      Error e
