namespace VainZero.Misc

open System

type Date =
  {
    Year: int
    Month: int
    Day: int
  }
with
  override this.ToString() =
    sprintf "%04d-%02d-%02d" this.Year this.Month this.Day

type DateRange = DateTime * DateTime

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Date =
  let create year month day =
    {
      Year = year
      Month = month
      Day = day
    }

  let ofDateTime (dateTime: DateTime) =
    create dateTime.Year dateTime.Month dateTime.Day

  let toDateTime date =
    DateTime(date.Year, date.Month, date.Day)

  let today () =
    DateTime.Now |> ofDateTime

  let parse (str: string) =
    DateTime.Parse(str) |> ofDateTime
