namespace VainZero.Collections

module Seq =
  let inline toCollection< ^t, ^x when ^t: (member Add: ^x -> unit) and ^t: (new: unit -> ^t)> xs =
    let c = new ^t()
    for x in xs do
      (^t: (member Add: ^x -> unit) (c, x))
    c

module Array =
  open System.Collections.Generic

  /// Add elements on the bottom of the specified array
  /// so that the result array has at least the specified length.
  let tailpad count x xs =
    let paddingCount = count - (xs |> Array.length) |> max 0
    Array.append xs (Array.replicate paddingCount x)

  let unique (xs: array<'x>) =
    let ys = ResizeArray(xs.Length)
    let set = HashSet()
    for x in xs do
      if set.Add(x) then
        ys.Add(x)
    ys |> Seq.toArray

module Map =
  let singleton k v =
    Map.empty |> Map.add k v

  let appendWith f l r =
    let combine m k v =
      let v' =
        match m |> Map.tryFind k with
        | None -> v
        | Some u -> f v u
      in m |> Map.add k v'
    r |> Map.fold combine l
