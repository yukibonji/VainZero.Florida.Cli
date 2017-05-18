namespace VainZero.Florida
  open System

  [<RequireQualifiedAccess>]
  type Command =
    | Empty
    | Usage
      of string
    | DailyReportCreate
      of DateTime
    | DailyReportSendMail
      of DateTime
    | WeeklyReportCreate
      of DateTime
    | WeeklyReportConvertToExcel
      of DateTime
    | TimeSheetUpdate
      of DateTime
    | TimeSheetExcel
      of DateTime
    /// Composite command of DailyReprotCreate and TimeSheetUpdate.
    | DailyReportFinalize
      of DateTime
