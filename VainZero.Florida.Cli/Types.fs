﻿namespace VainZero.Florida
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