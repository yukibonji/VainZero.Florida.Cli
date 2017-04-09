﻿namespace VainZero.Misc

[<AutoOpen>]
module Operators =
  let tap f x = f x; x

  let flip f x y = f y x

  let fold (xs: seq<'x>) (f: 'x -> 's -> 's) (s: 's): 's =
    xs |> Seq.fold (fun s x -> f x s) s

module Option =
  let getOr x =
    function
    | Some x -> x
    | None -> x

  let getOrElse f =
    function
    | Some x -> x
    | None -> f ()

module String =
  open System

  let contains (infix: string) (s: string): bool =
    s.Contains(infix)

  let replace (src: string) (dst: string) (s: string) =
    s.Replace(src, dst)

  let replaceEach (xs: seq<string * string>) (s: string): string =
    s |> fold xs (fun (src, dst) s -> s.Replace(src, dst))

  let concatWithLineBreak xs =
    xs |> String.concat Environment.NewLine

  let splitBySpaces (self: string) =
    self.Split([| ' '; '\t'; '\r'; '\n' |], StringSplitOptions.RemoveEmptyEntries)

module DayOfWeek =
  open System

  let all =
    [|
      DayOfWeek.Sunday
      DayOfWeek.Monday
      DayOfWeek.Tuesday
      DayOfWeek.Wednesday
      DayOfWeek.Thursday
      DayOfWeek.Friday
      DayOfWeek.Saturday
    |]

  let kanjis = [|"日"; "月"; "火"; "水"; "木"; "金"; "土"|]

  let toKanji (dow: DayOfWeek) =
    kanjis.[int dow]

  let ofKanji k =
    kanjis
    |> Array.tryFindIndex ((=) k)
    |> Option.map (fun i -> all.[i])

module DateTime =
  open System

  let private dayOfWeekIndex (date: DateTime) =
    date.DayOfWeek |> int

  let theLatestSunday (date: DateTime) =
    date.AddDays(date |> dayOfWeekIndex |> (~-) |> float)

  let weekDays date =
    let sunday = date |> theLatestSunday
    [|for i in 0..6 -> sunday.AddDays(float i)|]

  let dates (firstDate: DateTime) (lastDate: DateTime) =
    let rec loop date =
      seq {
        if date < lastDate then
          yield date
          yield! loop (date.AddDays(1.0))
      }
    loop firstDate.Date

module Console =
  open System
  open Printf

  let readYesNo msg =
    Console.Write(msg + " (Y/n)")
    let rec loop () =
      match Console.ReadLine() with
      | null | "N" | "No" | "no" | "n"
        -> false
      | "Y" | "Yes" | "yes"
        -> true
      | _ -> loop ()
    loop ()