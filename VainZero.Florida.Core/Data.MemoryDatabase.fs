﻿namespace VainZero.Florida.Data

open System
open System.Collections.Generic
open FsYaml
open VainZero.Collections
open VainZero.IO
open VainZero.Florida
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
          return Some value
        | (false, _) ->
          return None
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
          return Some report
        | (false, _) ->
          return None
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

type MemoryDatabase() =
  let context = new MemoryDataContext()

  interface IDatabase with
    override this.Connect() =
      context :> IDataContext