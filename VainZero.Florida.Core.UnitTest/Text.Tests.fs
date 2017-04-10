namespace VainZero.Text

open Persimmon

module ``test Xml`` =
  let ``test escape`` =
    test {
      do!
        "<tag>crlf\r\ncr\rlf\n</tag>"
        |> Xml.escape
        |> is "&lt;tag&gt;crlf&#10;cr&#10;lf&#10;&lt;/tag&gt;"
    }
