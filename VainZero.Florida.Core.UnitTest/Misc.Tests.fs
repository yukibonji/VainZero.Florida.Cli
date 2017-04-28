namespace VainZero.Misc

open Persimmon

module ``test String`` =
  let ``test trySubstring`` =
    test {
      let case str index count expected =
        str |> String.trySubstring index count |> is expected
      do! case "" 0 0 (Some "")
      do! case "ab" 0 1 (Some "a")
      do! case "ab" 0 2 (Some "ab")
      do! case "ab" 1 1 (Some "b")
      do! case "ab" 2 0 (Some "")
      do! case "ab" (-1) 0 None
      do! case "ab" 1 (-1) None
      do! case "ab" 2 1 None
      do! case "ab" 3 0 None
      do! case "ab" 3 (-1) None
    }
