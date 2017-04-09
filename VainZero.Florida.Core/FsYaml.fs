﻿namespace FsYaml

open System
open FsYaml.CustomTypeDefinition
open FsYaml.NativeTypes
open FsYaml.RepresentationTypes

module Yaml =
  let private dayOfWeekTypeDefinition =
    {
      Accept =
        fun typ -> typ = typeof<DayOfWeek>
      Construct =
        fun cons typ source ->
          match source with
          | Scalar (Plain name, _)
          | Scalar (NonPlain name, _) ->
            DayOfWeek.Parse(typ, name)
          | _ ->
            mustBeScalar typ source |> raise
      Represent =
        fun represent typ target ->
          let name = (target :?> DayOfWeek) |> string
          Scalar (Plain name, None)
    }

  let private customTypeDefinitions =
    [
      dayOfWeekTypeDefinition
    ]

  let myLoad<'x> obj =
    Yaml.loadWith<'x> customTypeDefinitions obj

  let myDump<'x> obj =
    let yaml = Yaml.dumpWith<'x> customTypeDefinitions obj

    // Workaround the bug YamlDotNet can't handle linebreaks correctly.
    // https://github.com/aaubry/YamlDotNet/issues/246
    let yaml = yaml.Replace("\r\n\r\n", "\r\n")

    // Change folded style to literal style.
    let yaml = yaml.Replace(": >-", ": |").Replace(": >", ": |")

    yaml