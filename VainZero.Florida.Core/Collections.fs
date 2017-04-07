namespace VainZero.Collections

module Seq =
  let inline toCollection< ^t, ^x when ^t: (member Add: ^x -> unit) and ^t: (new: unit -> ^t)> xs =
    let c = new ^t()
    for x in xs do
      (^t: (member Add: ^x -> unit) (c, x))
    c

module List =
  /// Add x's to back so that the list has at least n elements.
  let tailpad n x self =
    let len     = self |> List.length
    let count   = max 0 (n - len)
    in self @ (List.replicate count x)

  let unique (xs: list<'x>) =
    xs |> List.fold
      (fun (acc, set) x ->
          if set |> Set.contains x
          then (acc, set)
          else (x :: acc, set |> Set.add x)
          )
      ([], Set.empty)
    |> fst
    |> List.rev

module Map =
  let singleton k v =
    Map.ofList [(k, v)]

  let appendWith f l r =
    let combine m k v =
      let v' =
        match m |> Map.tryFind k with
        | None -> v
        | Some u -> f v u
      in m |> Map.add k v'
    r |> Map.fold combine l
