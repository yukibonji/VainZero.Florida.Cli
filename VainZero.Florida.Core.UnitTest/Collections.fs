namespace VainZero.Collections

open Persimmon

module ``test Seq`` =
  let ``test bundle`` =
    let source =
      [|
        ("Japan", "Kyoto")
        ("USA", "Florida")
        ("Japan", "Tokyo")
        ("China", "Shanghai")
        ("USA", "Washington")
      |]
    let expected =
      [|
        ("Japan", [|"Kyoto"; "Tokyo"|])
        ("USA", [|"Florida"; "Washington"|])
        ("China", [|"Shanghai"|])
      |]
    test {
      do!
        source
        |> Seq.bundle (fun x -> ResizeArray([|x|])) (fun a x -> a.Add(x); a)
        |> Array.map (fun (k, v) -> (k, v.ToArray()))
        |> is expected
    }
