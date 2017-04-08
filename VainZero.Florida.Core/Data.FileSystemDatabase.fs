﻿namespace VainZero.Florida.Data

open System
open System.Diagnostics
open System.IO
open FsYaml
open VainZero.Collections
open VainZero.IO
open VainZero.Florida
open VainZero.Florida.Reports

module internal DirectoryInfo =
  let createUnlessExists (directory: DirectoryInfo) =
    if Directory.Exists(directory.FullName) |> not then
      directory.Create()

module internal Process =
  let openFile (filePath: string) =
    Process.Start(filePath)

type FileSystemDailyReportRepository(root: DirectoryInfo) =
  let subdirectory =
    DirectoryInfo(Path.Combine(root.FullName, "daily-reports"))

  let filePath (date: DateTime) =
    let fileName = sprintf "%04d-%02d-%02d.yaml" date.Year date.Month date.Day
    Path.Combine(subdirectory.FullName, fileName)

  let dateFromFileName (fileName: string) =
    // TODO: Don't use exceptions for the purpose.
    try
      fileName.[0..9] |> DateTime.Parse |> Some
    with
    | _ -> None

  do subdirectory |> DirectoryInfo.createUnlessExists

  interface IDailyReportRepository with
    override this.Open(date) =
      Process.openFile (filePath date) |> ignore

    override this.FindAsync(date) =
      async {
        try
          let! yaml = File.readAllTextAsync (filePath date)
          let report = Yaml.load<DailyReport> yaml
          return Some (yaml, report)
        with
        | _ ->
          return None
      }

    override this.AddOrUpdateAsync(date, report) =
      async {
        let yaml = Yaml.dump report
        return! File.writeAllTextAsync yaml (filePath date)
      }

    override this.FirstDateAsync =
      async {
        return
          subdirectory.GetFiles("????-??-??.yaml")
          |> Seq.choose (fun file -> dateFromFileName file.Name)
          |> Seq.tryMin
      }

type FileSystemWeeklyReportRepository(root: DirectoryInfo) =
  let subdirectory =
    DirectoryInfo(Path.Combine(root.FullName, "weekly-reports"))

  let filePath (firstDate: DateTime, lastDate: DateTime) =
    let fileName =
      sprintf
        "from-%04d-%02d-%02d-to-%04d-%02d-%02d.yaml"
        firstDate.Year firstDate.Month firstDate.Day
        lastDate.Year lastDate.Month lastDate.Day
    Path.Combine(subdirectory.FullName, fileName)

  let dateRangeFromFileName (fileName: string) =
    // TODO: Don't use exceptions for the purpose.
    try
      let firstDate =
        fileName.[5..14] |> DateTime.Parse
      let lastDate =
        fileName.[19..28] |> DateTime.Parse
      Some (firstDate, lastDate)
    with
    | _ -> None

  do subdirectory |> DirectoryInfo.createUnlessExists

  interface IKeyValueRepository<DateRange, WeeklyReport> with
    override this.Open(dateRange) =
      Process.openFile (filePath dateRange) |> ignore

    override this.FindAsync(dateRange) =
      async {
        try
          let! yaml = File.readAllTextAsync (filePath dateRange)
          let report = Yaml.load<WeeklyReport> yaml
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

  interface IWeeklyReportRepository with
    override this.LatestDateRangeAsync(date) =
      async {
        // 日付の区間の終点が「指定された日付の i 日前」である週報を検索する。
        // 通常は i = 8 で見つかる。
        // 無限ループを防ぐため、2015年より前の週報は検索しない。
        let rec loop i =
          let lastDate = date.AddDays(float (-i))
          if lastDate.Year >= 2015 then
            let pattern =
              sprintf
                "from-????-??-??-to-%04d-%02d-%02d.yaml"
                lastDate.Year lastDate.Month lastDate.Day
            let dateRange =
              subdirectory.GetFiles(pattern)
              |> Array.choose (fun file -> dateRangeFromFileName file.Name)
              |> Seq.tryMinBy snd
            match dateRange with
            | Some dateRange ->
              Some dateRange
            | None ->
              loop (i + 1)
          else None
        return loop 1
      }

type FileSystemWeeklyReportExcelRepository(root: DirectoryInfo) =
  let subdirectory =
    DirectoryInfo(Path.Combine(root.FullName, "weekly-report-excels"))

  let filePath (firstDate: DateTime, lastDate: DateTime) =
    let fileName =
      sprintf
        "from-%04d-%02d-%02d-to-%04d-%02d-%02d.xml"
        firstDate.Year firstDate.Month firstDate.Day
        lastDate.Year lastDate.Month lastDate.Day
    Path.Combine(subdirectory.FullName, fileName)

  do subdirectory |> DirectoryInfo.createUnlessExists

  interface IKeyValueRepository<DateRange, string> with
    override this.Open(dateRange) =
      Process.Start("excel", filePath dateRange) |> ignore

    override this.FindAsync(dateRange) =
      NotImplementedException() |> raise

    override this.AddOrUpdateAsync(dateRange, xml) =
      File.writeAllTextAsync xml (filePath dateRange)

  interface IWeeklyReportExcelRepository

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
