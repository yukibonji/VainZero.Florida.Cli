﻿namespace VainZero.Misc

[<AutoOpen>]
module Operators =
  let tap f x = f x; x

  let flip f x y = f y x

  let fold (xs: seq<'x>) (f: 'x -> 's -> 's) (s: 's): 's =
    xs |> Seq.fold (fun s x -> f x s) s

module String =
  open System

  let contains (infix: string) (s: string): bool =
    s.Contains(infix)

  let replace (src: string) (dst: string) (s: string) =
    s.Replace(src, dst)

  let replaceEach (xs: list<string * string>) (s: string): string =
    s |> fold xs (fun (src, dst) s -> s.Replace(src, dst))

  let concatWithLineBreak xs =
    xs |> String.concat Environment.NewLine

  let splitBySpaces (self: string) =
    self.Split([| ' '; '\t'; '\r'; '\n' |], StringSplitOptions.RemoveEmptyEntries)

module Enum =
  open System

  let getValues<'t> =
    Enum.GetValues(typeof<'t>) |> Seq.cast<'t> |> Array.ofSeq

module DayOfWeek =
  open System

  let all = Enum.getValues<DayOfWeek>

  let kanjis = [|"日"; "月"; "火"; "水"; "木"; "金"; "土"|]

  let toKanji (dow: DayOfWeek) =
    kanjis.[int dow]

  let ofKanji k =
    kanjis
    |> Array.tryFindIndex ((=) k)
    |> Option.map (fun i -> all.[i])

module DateTime =
  open System

  let dayOfWeekId (date: DateTime) =
    date.DayOfWeek |> int

  let theLatestSunday (date: DateTime) =
    date.AddDays(date |> dayOfWeekId |> (~-) |> float)

  let weekDays date =
    let sunday = date |> theLatestSunday
    in [ for i in 0..6 -> sunday.AddDays(float i) ]

module Console =
  open System
  open Printf

  let readYesNo msg =
    Console.Write(msg + " (Y/n)")
    let rec loop () =
      match Console.ReadLine() with
      | "Y" | "Yes" | "yes"         -> true
      | "N" | "No" | "no" | "n"     -> false
      | _ -> loop ()
    in loop ()

  let runApp f =
    try
      f ()
      0
    with
    | e ->
      eprintfn "ERROR! %s" e.Message
      1
