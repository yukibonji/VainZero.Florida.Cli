namespace VainZero.Florida.Misc

open Persimmon
open FsYaml
open VainZero.Misc

module ``test Yaml`` =
  let ``test Date`` =
    test {
      let date = Date.create 2017 04 20
      let str = "2017-04-20"
      do! (date |> Yaml.myDump).TrimEnd() |> is str
      do! str |> Yaml.myLoad<Date> |> is date
    }
