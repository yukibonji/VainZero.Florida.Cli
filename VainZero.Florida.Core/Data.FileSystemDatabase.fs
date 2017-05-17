namespace VainZero.Florida.Data

open System
open System.Collections.Generic
open System.Diagnostics
open System.IO
open FSharpKit.ErrorHandling
open FsYaml
open VainZero.Collections
open VainZero.Misc
open VainZero.IO
open VainZero.Florida.Misc
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
    Option.build {
      let! dateString = fileName |> String.trySubstring 0 10
      return! dateString |> DateTime.tryParse
    }

  let templateFile =
    FileInfo(Path.Combine(subdirectory.FullName, "template.yaml"))

  do subdirectory |> DirectoryInfo.createUnlessExists

  interface IDailyReportRepository with
    override this.Open(date) =
      Process.openFile (filePath date) |> ignore

    override this.ScaffoldAsync(date) =
      async {
        File.Copy(templateFile.FullName, filePath date, overwrite = true)
      }

    override this.FindAsync(date) =
      AsyncResult.build {
        let! yaml = File.tryReadAllTextAsync (filePath date)
        let! report = Yaml.tryMyLoad<DailyReport> yaml
        return (yaml, report)
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
    Option.build {
      let tryParseDate index =
        Option.build {
          let! substring = fileName |> String.trySubstring index 10
          return! substring |> DateTime.tryParse
        }
      let! firstDate = tryParseDate 5
      let! lastDate = tryParseDate 19
      return (firstDate, lastDate)
    }

  do subdirectory |> DirectoryInfo.createUnlessExists

  interface IWeeklyReportRepository with
    override this.Open(dateRange) =
      Process.openFile (filePath dateRange) |> ignore

    override this.FindAsync(dateRange) =
      AsyncResult.build {
        let! yaml = File.tryReadAllTextAsync (filePath dateRange)
        return! Yaml.tryMyLoad<WeeklyReport> yaml |> AsyncResult.ofResult
      }

    override this.AddOrUpdateAsync(dateRange, report) =
      async {
        let yaml = Yaml.myDump report
        return! File.writeAllTextAsync yaml (filePath dateRange)
      }

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

  interface IWeeklyReportExcelRepository with
    override this.Open(dateRange) =
      Process.Start("excel", filePath dateRange) |> ignore

    override this.AddOrUpdateAsync(dateRange, xml) =
      File.writeAllTextAsync xml (filePath dateRange)

type FileSystemTimeSheetRepository(root: DirectoryInfo) =
  let subdirectory =
    DirectoryInfo(Path.Combine(root.FullName, "time-sheets"))

  let filePath (month: DateTime) =
    let fileName = month.ToString("yyyy-MM") + ".yaml"
    Path.Combine(subdirectory.FullName, fileName)

  do subdirectory |> DirectoryInfo.createUnlessExists

  interface ITimeSheetRepository with
    override this.FindAsync(month) =
      async {
        try
          let! source = File.readAllTextAsync (filePath month)
          let timeSheet = Yaml.myLoad source
          return Some timeSheet
        with
        | _ ->
          return None
      }

    override this.AddOrUpdateAsync(month, timeSheet) =
      async {
        let yaml = timeSheet |> Yaml.myDump
        return! File.writeAllTextAsync yaml (filePath month)
      }

type FileSystemTimeSheetExcelRepository(root: DirectoryInfo) =
  let subdirectory =
    DirectoryInfo(Path.Combine(root.FullName, "time-sheet-excels"))

  let filePath (month: DateTime) =
    let fileName = month.ToString("yyyy-MM") + ".yaml"
    Path.Combine(subdirectory.FullName, fileName)

  do subdirectory |> DirectoryInfo.createUnlessExists

  interface ITimeSheetExcelRepository with
    override this.Open(month) =
      Process.openFile (filePath month) |> ignore

    override this.ExistsAsync(month) =
      async {
        return File.Exists(filePath month)
      }

    override this.AddOrUpdateAsync(month, content) =
      async {
        return! File.writeAllTextAsync content (filePath month)
      }

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

    override val TimeSheets =
      FileSystemTimeSheetRepository(root) :> ITimeSheetRepository

    override val TimeSheetExcels =
      FileSystemTimeSheetExcelRepository(root) :> ITimeSheetExcelRepository

type FileSystemDatabase(root: DirectoryInfo) =
  interface IDatabase with
    override this.Connect() =
      new FileSystemDataContext(root) :> IDataContext
