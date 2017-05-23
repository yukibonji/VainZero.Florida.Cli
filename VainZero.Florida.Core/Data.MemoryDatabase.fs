namespace VainZero.Florida.Data

open System
open System.Collections.Generic
open FsYaml
open VainZero.Collections
open VainZero.IO
open VainZero.Misc
open VainZero.Florida
open VainZero.Florida.Misc
open VainZero.Florida.Reports

type MemoryDailyReportRepository() =
  let dictionary = Dictionary<DateTime, string * DailyReport>()

  interface IDailyReportRepository with
    override this.Open(_) =
      ()

    override this.ScaffoldAsync(date) =
      async {
        let dailyReport = DailyReport.empty
        let yaml = Yaml.myDump dailyReport
        dictionary.[date] <- (yaml, dailyReport)
      }

    override this.FindAsync(date) =
      async {
        match dictionary.TryGetValue(date) with
        | (true, value) ->
          return ParsableEntry value |> Ok
        | (false, _) ->
          return UnexistingParsableEntry |> Ok
      }

    override this.FirstDateAsync =
      async {
        return dictionary.Keys |> Seq.tryMin
      }

type MemoryWeeklyReportRepository() =
  let dictionary = Dictionary<DateRange, WeeklyReport>()

  interface IWeeklyReportRepository with
    override this.Open(_) =
      ()

    override this.FindAsync(dateRange) =
      async {
        match dictionary.TryGetValue(dateRange) with
        | (true, report) ->
          let yaml = Yaml.myDump report
          return ParsableEntry (yaml, report) |> Ok
        | (false, _) ->
          return UnexistingParsableEntry |> Ok
      }

    override this.AddOrUpdateAsync(dateRange, report) =
      async {
        dictionary.[dateRange] <- report
      }

    override this.LatestDateRangeAsync(date) =
      async {
        return dictionary.Keys |> Seq.tryMinBy snd
      }

type MemoryWeeklyReportExcelRepository() =
  let dictionary = Dictionary<DateRange, string>()

  interface IWeeklyReportExcelRepository with
    override this.Open(_) =
      ()

    override this.AddOrUpdateAsync(dateRange, xml) =
      async {
        dictionary.[dateRange] <- xml
      }

type MemoryTimeSheetRepository() =
  let dictionary = Dictionary<DateTime, TimeSheet>()

  interface ITimeSheetRepository with
    override this.FindAsync(month) =
      async {
        return dictionary |> Dictionary.tryItem month
      }

    override this.AddOrUpdateAsync(month, timeSheet) =
      async {
        dictionary |> Dictionary.addOrSet month timeSheet
      }

type MemoryTimeSheetExcelRepository() =
  let dictionary = Dictionary<DateTime, string>()

  interface ITimeSheetExcelRepository with
    override this.Open(_) =
      ()

    override this.ExistsAsync(month) =
      async {
        return dictionary.ContainsKey(month) 
      }

    override this.AddOrUpdateAsync(month, content) =
      async {
        dictionary |> Dictionary.addOrSet month content
      }

type MemoryDataContext() =
  interface IDisposable with
    override this.Dispose() = ()

  interface IDataContext with
    override val DailyReports =
      MemoryDailyReportRepository() :> IDailyReportRepository

    override val WeeklyReports =
      MemoryWeeklyReportRepository() :> IWeeklyReportRepository

    override val WeeklyReportExcels =
      MemoryWeeklyReportExcelRepository() :> IWeeklyReportExcelRepository

    override val TimeSheets =
      MemoryTimeSheetRepository() :> ITimeSheetRepository

    override val TimeSheetExcels =
      MemoryTimeSheetExcelRepository() :> ITimeSheetExcelRepository

type MemoryDatabase() =
  let context = new MemoryDataContext()

  interface IDatabase with
    override this.Connect() =
      context :> IDataContext
