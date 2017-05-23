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

  let ``test indentEachLine`` =
    test {
      let case str expected =
        test {
          do! str |> String.indentEachLine 2 |> is expected
          do!
            str.Replace("\n", "\r\n") |> String.indentEachLine 2
            |> is (expected.Replace("\n", "\r\n"))
        }
      // The first line should be indented if not empty.
      do! case "" ""
      do! case "\n" "\n"
      do! case "1" "  1"
      do! case "  1" "    1"
      // Nonempty lines after linebreaks should be indented.
      do! case "\n1" "\n  1"
      // All lines should be indented.
      do! case "1\n2" "  1\n  2"
      // Empty lines after linebreaks should not be indented.
      do! case "\n\n1" "\n\n  1"
    }
