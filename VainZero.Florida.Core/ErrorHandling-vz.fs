namespace VainZero.ErrorHandling

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

  let collectToArray (results: seq<Result<'x, 'e>>) =
    let values = ResizeArray()
    let messages = ResizeArray()
    use e = results.GetEnumerator()
    let rec loop isOk =
      if e.MoveNext() then
        let isOk =
          match (isOk, e.Current) with
          | (true, Ok (x, warnings)) ->
            values.Add(x)
            messages.AddRange(warnings)
            true
          | (true, Bad errors) ->
            messages.Clear()
            messages.AddRange(errors)
            false
          | (false, Ok _) ->
            false
          | (false, Bad errors) ->
            messages.AddRange(errors)
            false
        loop isOk
      else
        if isOk then
          Ok (values.ToArray(), messages |> Seq.toList)
        else
          Bad (messages |> Seq.toList)
    loop true

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
