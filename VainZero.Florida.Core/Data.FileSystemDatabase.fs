namespace VainZero.Florida.Data

open System
open System.Diagnostics
open System.IO
open FsYaml
open VainZero.IO
open VainZero.Florida.Reports

type FileSystemDailyReportRepository(root: DirectoryInfo) =
  let filePath (date: DateTime) =
    Path.Combine
      ( root.FullName
      , string date.Year
      , sprintf "%02d" date.Month
      , sprintf "%02d.yaml" date.Day
      )

  interface IKeyValueRepository<DateTime, ``日報``> with
    override this.FindAsync(date) =
      async {
        try
          let! yaml = File.readAllTextAsync (filePath date)
          let report = Yaml.load<``日報``> yaml
          return Some report
        with
        | _ ->
          return None
      }

    override this.AddOrUpdateAsync(date, report) =
      async {
        let yaml = Yaml.dump report
        return! File.writeAllTextAsync yaml (filePath date)
      }

  interface IDailyReportRepository

type FileSystemWeeklyReportRepository(root: DirectoryInfo) =
  let filePath (firstDate: DateTime, lastDate: DateTime) =
    Path.Combine
      ( root.FullName
      , string firstDate.Year
      , sprintf "%02d" firstDate.Month
      , sprintf "%02d-%02d.yaml" firstDate.Day lastDate.Day
      )

  interface IKeyValueRepository<DateTime * DateTime, ``週報``> with
    override this.FindAsync(dateRange) =
      async {
        try
          let! yaml = File.readAllTextAsync (filePath dateRange)
          let report = Yaml.load<``週報``> yaml
          return Some report
        with
        | _ ->
          return None
      }

    override this.AddOrUpdateAsync(dateRange, report) =
      async {
        let yaml = Yaml.dump report
        return! File.writeAllTextAsync yaml (filePath dateRange)
      }

  interface IWeeklyReportRepository

type FileSystemWeeklyReportExcelRepository(root: DirectoryInfo) =
  let filePath (firstDate: DateTime, lastDate: DateTime) =
    Path.Combine
      ( root.FullName
      , string firstDate.Year
      , sprintf "%02d" firstDate.Month
      , sprintf "%02d-%02d.yaml" firstDate.Day lastDate.Day
      )

  interface IWeeklyReportExcelRepository with
    override this.Open(firstDate, lastDate) =
      Process.Start("excel", filePath (firstDate, lastDate)) |> ignore

    override this.AddOrUpdateAsync(firstDate, lastDate, report) =
      async {
        let xml = report |> WeeklyReports.toExcelXml
        return! File.writeAllTextAsync xml (filePath (firstDate, lastDate))
      }

  interface IKeyValueRepository<DateTime * DateTime, ``週報``> with
    override this.FindAsync(dateRange) =
      async {
        try
          let! yaml = File.readAllTextAsync (filePath dateRange)
          let report = Yaml.load<``週報``> yaml
          return Some report
        with
        | _ ->
          return None
      }

    override this.AddOrUpdateAsync(dateRange, report) =
      async {
        let yaml = Yaml.dump report
        return! File.writeAllTextAsync yaml (filePath dateRange)
      }

  interface IWeeklyReportRepository

type FileSystemDataContext(root: DirectoryInfo) =
  interface IDisposable with
    override this.Dispose() = ()

  interface IDataContext with
    override val DailyReports =
      FileSystemDailyReportRepository(root) :> IDailyReportRepository

    override val WeeklyReports =
      FileSystemWeeklyReportRepository(root) :> IWeeklyReportRepository

    override val WeeklyReportExcels =
      FileSystemWeeklyReportExcelRepository(root) :> IWeeklyReportExcelRepository

type FileSystemDatabase(root: DirectoryInfo) =
  interface IDatabase with
    override this.Connect() =
      new FileSystemDataContext(root) :> IDataContext
