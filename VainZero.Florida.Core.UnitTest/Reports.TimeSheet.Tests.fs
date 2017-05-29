namespace VainZero.Florida.Reports

open System
open System.Collections.Generic
open Persimmon
open VainZero.Misc
open VainZero.Florida.Configurations
open VainZero.Florida.Data

module ``test TimeSheet`` =
  module ``test ConvertToExcelXmlFunction`` =
    open TimeSheet.ConvertToExcelXmlFunction

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module TimeSheetItem =
      let regular =
        TimeSheetItem.create (TimeSpan(9, 0, 0)) (TimeSpan(7, 0, 0)) (TimeSpan(1, 0, 0))

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module WorkTime =
      let regular =
        TimeSheet.ConvertToExcelXmlFunction.WorkTime.Create
          ( TimeSpan(9, 0, 0)
          , TimeSpan(17, 0, 0)
          , TimeSpan(1, 0, 0)
          )

    module ``test WorkTime`` =
      let ``test Duration`` =
        test {
          do! WorkTime.regular.Duration |> is (TimeSpan(7, 0, 0))
        }

      let ``test ofTimeSheetItem`` =
        test {
          let case source expected =
            source
            |> TimeSheet.ConvertToExcelXmlFunction.WorkTime.ofTimeSheetItem
            |> is expected
          let item = TimeSheetItem.regular
          do! case item (Some WorkTime.regular)
          do! case { item with 開始時刻 = None } None
          do! case { item with 終了時刻 = None } None
          do! case { item with 休憩時間 = None } None
        }

    let timeSheet =
      [|
        // Saturday
        1 --> { TimeSheetItem.empty with 備考 = Some "First day of month." }
        // Monday
        3 --> TimeSheetItem.regular
        4 --> { TimeSheetItem.regular with 備考 = Some "It's tuesday." }
      |]
      |> Array.map KeyValuePair

    let ``test dateRows`` =
      test {
        let month = DateTime(2017, 4, 1)
        let dateRows =
          TimeSheet.ConvertToExcelXmlFunction.dateRows month timeSheet

        // Verify dates.
        do! dateRows.Length |> is 31

        // Verify contents.
        do!
          test {
            let (_, workTime, note) = dateRows.[1 - 1]
            do! workTime |> is None
            do! note |> is "First day of month."
          }
        do!
          test {
            let (_, workTime, note) = dateRows.[2 - 1]
            do! workTime |> is None
            do! note |> is ""
          }
        do!
          test {
            let (_, workTime, note) = dateRows.[3 - 1]
            do! workTime |> is (Some WorkTime.regular)
            do! note |> is ""
          }
        do!
          test {
            let (_, workTime, note) = dateRows.[4 - 1]
            do! workTime |> is (Some WorkTime.regular)
            do! note |> is "It's tuesday."
          }
      }

    module ``test convertToExcelXmlAsync`` =
      let dataContext () = (MemoryDatabase() :> IDatabase).Connect()
      let config = Config.test
      let month = DateTime(2017, 4, 1)

      let ``test case no-timesheet error`` =
        test {
          use dataContext = dataContext ()
          let! e = trap { it (convertToExcelXmlAsync dataContext config month |> sync) }
          do! e.Message |> is "4月分の勤務表がありません。"
        }

      let ``test success`` =
        test {
          use dataContext = dataContext ()
          dataContext.TimeSheets.AddOrUpdateAsync(month, timeSheet) |> sync
          convertToExcelXmlAsync dataContext config month |> sync
          do!
            dataContext.TimeSheetExcels.ExistsAsync(month) |> sync
            |> is true
        }
