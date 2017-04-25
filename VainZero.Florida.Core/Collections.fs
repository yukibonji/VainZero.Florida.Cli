namespace VainZero.Collections

module Seq =
  open System.Collections.Generic

  let tryMinBy (f: 'x -> 'y) (xs: seq<'x>) =
    use e = xs.GetEnumerator()
    if e.MoveNext() then
      let mutable minX = e.Current
      let mutable minY = f minX
      while e.MoveNext() do
        let x = e.Current
        let y = f x
        if y < minY then
          minX <- x
          minY <- y
      Some minX
    else
      None

  let tryMin (xs: seq<'x>) =
    xs |> tryMinBy id

  let bundle (inject: 'v -> 'w) (append: 'w -> 'v -> 'w) (kvs: seq<'k * 'v>) =
    let list = ResizeArray<'k * 'w>()
    let dictionary = Dictionary<'k, int>()
    for (k, v) in kvs do
      match dictionary.TryGetValue(k) with
      | (true, index) ->
        let w = list.[index] |> snd
        list.[index] <- (k, append w v)
      | (false, _) ->
        let index = list.Count
        list.Add(k, inject v)
        dictionary.Add(k, index)
    list.ToArray()

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

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Dictionary =
  open System.Collections.Generic

  let tryItem key (dictionary: IReadOnlyDictionary<_, _>) =
    match dictionary.TryGetValue(key) with
    | (true, value) ->
      Some value
    | (false, _) ->
      None

  let addOrSet key value (dictionary: IDictionary<_, _>) =
    if dictionary.ContainsKey(key)
    then dictionary.[key] <- value
    else dictionary.Add(key, value)
