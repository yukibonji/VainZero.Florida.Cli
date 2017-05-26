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
          WeeklyReport.dateRangeFromDateAsync dataContext date |> Async.RunSynchronously
        do! firstDate |> is date
        do! lastDate |> is date
      }

    let ``case if no weekly report exists`` =
      test {
        use dataContext = emptyDataContext ()
        let firstDate = DateTime(2017, 4, 1)
        let middleDate = DateTime(2017, 4, 3)
        let lastDate = DateTime(2017, 4, 6)

        // Generate daily reports.
        [|firstDate; middleDate; lastDate|] |> Array.iter
          (fun date ->
            dataContext.DailyReports.ScaffoldAsync(date) |> Async.RunSynchronously
          )

        let date = DateTime(2017, 4, 7)
        do!
          WeeklyReport.dateRangeFromDateAsync dataContext date |> Async.RunSynchronously
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
            dataContext.DailyReports.ScaffoldAsync(date) |> Async.RunSynchronously
          )

        // Generate a weekly report.
        dataContext.WeeklyReports.AddOrUpdateAsync((firstDate, lastDate), WeeklyReport.empty johnDoe)
        |> Async.RunSynchronously

        let date = DateTime(2017, 4, 14)
        do!
          WeeklyReport.dateRangeFromDateAsync dataContext date |> Async.RunSynchronously
          |> is (DateTime(2017, 4, 8), date)
      }
