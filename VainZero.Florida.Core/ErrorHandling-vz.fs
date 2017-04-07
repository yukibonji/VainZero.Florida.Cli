namespace VainZero.ErrorHandling

module Option =
  let getOr x =
    function
    | Some x -> x
    | None -> x

  let getOrElse f =
    function
    | Some x -> x
    | None -> f ()

module Trial =
  open System
  open Chessie.ErrorHandling

  let catch (f: unit -> 'x): Result<'x, exn> =
    try
      f () |> pass
    with
    | :? AggregateException as e ->
        e.InnerExceptions |> Seq.toList |> Bad
    | e ->
        fail e

  let getOr (x: 'x): Result<'x, _> -> 'x =
    function
    | Ok (x, _) -> x
    | Bad _ -> x

  let toMessages: Result<_, 'm> -> list<'m> =
    function
    | Pass _ -> []
    | Warn (_, msgs)
    | Fail msgs
      -> msgs

  let consumeMessages (f: list<'m> -> unit): Result<'x, 'm> -> option<'x> =
    function
    | Pass x ->
        Some x
    | Warn (x, msgs) ->
        f msgs
        Some x
    | Fail msgs ->
        f msgs
        None

  let inline runConsoleApp (result: Result<unit, 'x>): int =
    let printMessages msgs =
      for msg in msgs do
        eprintfn "%s" (string msg)
    match result with
    | Pass () ->
        0
    | Warn ((), msgs) ->
        eprintfn "WARNING"
        printMessages msgs
        0
    | Fail msgs ->
        eprintfn "ERROR"
        printMessages msgs
        1
