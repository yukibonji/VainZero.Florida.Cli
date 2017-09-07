namespace VainZero.Florida.Reports

open System
open Persimmon
open VainZero.Misc
open VainZero.Florida.Data
open VainZero.Florida.Misc

module ``test WeeklyReport`` =
  let emptyDataContext () = (MemoryDatabase() :> IDatabase).Connect()

  let johnDoe =
    {
      所属部署 =
        "Testing Department"
      名前 =
        "John Doe"
    }

  module ``test dateRangeFromDateAsync`` =
    let ``case if no daily report exists`` =
      test {
        use dataContext = emptyDataContext ()
        let date = DateTime(2017, 4, 1)
        let (firstDate, lastDate) =
          WeeklyReport.dateRangeFromDateAsync dataContext date |> sync
        do! firstDate |> is date
        do! lastDate |> is date
      }

    let ``test case if no weekly report exists`` =
      test {
        use dataContext = emptyDataContext ()
        let firstDate = DateTime(2017, 4, 1)
        let middleDate = DateTime(2017, 4, 3)
        let lastDate = DateTime(2017, 4, 6)

        // Generate daily reports.
        [|firstDate; middleDate; lastDate|] |> Array.iter
          (fun date ->
            dataContext.DailyReports.ScaffoldAsync(date) |> sync
          )

        let date = DateTime(2017, 4, 7)
        do!
          WeeklyReport.dateRangeFromDateAsync dataContext date |> sync
          |> is (firstDate, date)
      }

    let ``test case if weekly reports exist`` =
      test {
        use dataContext = emptyDataContext ()
        let firstDate = DateTime(2017, 4, 1)
        let lastDate = DateTime(2017, 4, 7)

        // Generate daily reports.
        [|firstDate; lastDate|] |> Array.iter
          (fun date ->
            dataContext.DailyReports.ScaffoldAsync(date) |> sync
          )

        // Generate a weekly report.
        dataContext.WeeklyReports.AddOrUpdateAsync((firstDate, lastDate), WeeklyReport.empty johnDoe)
        |> sync

        let date = DateTime(2017, 4, 14)
        do!
          WeeklyReport.dateRangeFromDateAsync dataContext date |> sync
          |> is (DateTime(2017, 4, 8), date)
      }

  module ``test GenerateFromDailyReportsFunction`` =
    open VainZero.Florida.Reports.WeeklyReport.GenerateFromDailyReportsFunction

    module Seed =
      let date1 = DateTime(2017, 4, 1)
      let report1 =
        DailyReport.create
          [|
            Work.create "Florida" "Implementation" 1.0 (Some "1. Florida Implementation")
            Work.create "Florida" "Testing" 1.0 (Some "1. Florida Testing")
          |]
          "1. Next plan"
          (Some "1. Note")
      let date2 = DateTime(2017, 4, 2)
      let report2 =
        DailyReport.create
          [|
            Work.create "Florida" "Implementation" 1.0 (Some "2. Florida Implementation")
            Work.create "Alaska" "Design" 1.0 (Some "2. Alaska Design")
          |]
          "2. Next plan"
          (Some "2. Note")
      let date6 = DateTime(2017, 4, 6)
      let report6 =
        DailyReport.create
          [|
            Work.create "Florida" "Testing" 1.0 (Some "6. Florida Testing")
          |]
          "6. Next plan"
          (Some "6. Note")
      let reports =
        [|
          (date1, report1)
          (date2, report2)
          (date6, report6)
        |]

    let seed () =
      let dataContext = emptyDataContext ()
      let repository = dataContext.DailyReports :?> MemoryDailyReportRepository
      Seed.reports |> Seq.iter
        (fun (date, report) ->
          let entry = ParsableEntry (Yaml.myDump report, report)
          repository.Dictionary.Add(date, entry)
        )
      dataContext

    let ``test dailyReportsAsync`` =
      test {
        use dataContext = seed ()
        let reports = dailyReportsAsync dataContext (Seed.date1, Seed.date6) |> sync
        do! reports |> is Seed.reports
      }

    let ``test minimalDateRange`` =
      test {
        let dateRange = minimalDateRange Seed.reports
        do! dateRange |> is (Seed.date1, Seed.date6)
      }

    let ``test activities`` =
      test {
        let (names, map) = activities Seed.reports
        do!
          names |> is
            [|
              "Florida/Implementation"
              "Florida/Testing"
              "Alaska/Design"
            |]
        do! map.Count |> is 3
        do!
          map |> Map.find "Florida/Implementation" |> is
            ( "1. Florida Implementation" + Environment.NewLine
            + "2. Florida Implementation"
            )
        do!
          map |> Map.find "Florida/Testing" |> is
            ( "1. Florida Testing" + Environment.NewLine
            + "6. Florida Testing"
            )
        do!
          map |> Map.find "Alaska/Design" |> is
            "2. Alaska Design"
      }

    let ``test progressList`` =
      test {
        do! [|"a"; "b"; "c"|] |> progressList |> is
              ( "a: 0%" + Environment.NewLine
              + "b: 0%" + Environment.NewLine
              + "c: 0%"
              )
      }

    let ``test simplifiedDailyReports`` =
      test {
        do!
          [|(Seed.date1, Seed.report1)|] |> simplifiedDailyReports
          |> is
            [|
              {
                日付 =
                  Date.create 2017 4 1
                作業実績 =
                  [|
                    Work.create "Florida" "Implementation" 1.0 None
                    Work.create "Florida" "Testing" 1.0 None
                  |]
              }
            |]
      }

    let ``test weeklyActivity`` =
      test {
        let (activityNames, activityNotes) = activities Seed.reports
        do!
          weeklyActivity activityNames activityNotes |> is
            """Florida/Implementation: |
  1. Florida Implementation
  2. Florida Implementation
Florida/Testing: |
  1. Florida Testing
  6. Florida Testing
Alaska/Design: 2. Alaska Design
"""
      }

  module ``test ConvertToExcelXmlFunction`` =
    open VainZero.Florida.Reports.WeeklyReport.ConvertToExcelXmlFunction

    let ``test dayRowsFromDailyReport`` =
      test {
        let simplifiedDailyReport =
          {
            日付 =
              Date.create 2017 4 1
            作業実績 =
              [|
                Work.create "Florida" "Implementation" 1.0 None
                Work.create "Florida" "Testing" 1.245 None
              |]
          }
        do!
          simplifiedDailyReport |> dayRowsFromDailyReport |> is
            [|
              DayRow.Create("04/01", "土", "Florida", "Implementation", "1.00")
              DayRow.Create("", "", "Florida", "Testing", "1.25")
            |]
      }
