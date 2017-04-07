namespace Reports

open System
open System.IO
open FsYaml
open WeeklyReports

module WeeklyReports =
  let ofYaml yaml =
    Yaml.load<``週報``> yaml

  let path date =
    let sunday    = date |> DateTime.theLatestSunday
    let saturday  = sunday.AddDays(6.0)
    in
      Path.Combine
        ( App.config.ReportsDir
        , string sunday.Year
        , sprintf "%02d" sunday.Month
        , sprintf "%02d-%02d.yaml" sunday.Day saturday.Day
        )

  type DayRow =
    {
      Date              : string
      DayOfWeek         : string
      Project           : string
      Content           : string
      Hours             : string
    }
  with
    static member Empty =
      {
        Date            = ""
        DayOfWeek       = ""
        Project         = ""
        Content         = ""
        Hours           = ""
      }

  let dayRows (wr: ``週報``) =
    let sunday =
      DateTime.Now |> DateTime.theLatestSunday
    let w = wr.``日別の内容`` |> ``日別の内容``.toMap
    [
      for dow in DayOfWeek.all do
        let dowKanji = dow |> DayOfWeek.toKanji
        match w |> Map.tryFind dowKanji |> Option.getOr [] with
        | [] -> ()
        | dayList ->
            yield!
              dayList |> List.mapi (fun i d ->
                {
                  Date            =
                    if i = 0
                    then sunday.AddDays(dow |> float).ToString("MM/dd")
                    else ""
                  DayOfWeek       =
                    if i = 0 then dowKanji else ""
                  Project         = d.``案件``
                  Content         = d.``内容``
                  Hours           = d.``工数`` |> sprintf "%.1f"
                })
    ]
    |> List.tailpad 10 DayRow.Empty

  let toExcelXml (wr: ``週報``) =
    let dayByDayXml =
      wr
      |> dayRows
      |> List.map (fun dayRow ->
          dayByDay
          |> String.replaceEach
              [
                ("{{日付}}", dayRow.Date     )
                ("{{曜日}}", dayRow.DayOfWeek)
                ("{{案件}}", dayRow.Project  )
                ("{{内容}}", dayRow.Content  )
                ("{{工数}}", dayRow.Hours    )
              ]
          )
      |> String.concat (Environment.NewLine)
    let xml =
      weeklyReport
      |> String.replaceEach
          [
            ("{{所属部署}}"   , wr.``担当者``.``所属部署`` )
            ("{{担当者名}}"   , wr.``担当者``.``名前``     )
            ("{{今週活動}}"   , wr.``今週の主な活動`` |> Xml.escape)
            ("{{進捗}}"       , wr.``進捗``           |> Xml.escape)
            ("{{日別の内容}}" , dayByDayXml                )
            ("{{今週実績}}"   , wr.``今週実績`` |> Xml.escape  )
            ("{{来週予定}}"   , wr.``来週予定`` |> Xml.escape  )
            ("{{その他}}"     , wr.``その他``   |> Xml.escape  )
          ]
    in xml

  let excelPath date =
    Path.ChangeExtension(path date, ".xml")

  let generateExcel date =
    let xmlPath   = excelPath date
    let yaml      = File.ReadAllText(path date)
    let data      = yaml |> ofYaml
    let xml       = data |> toExcelXml
    File.WriteAllText(xmlPath, xml)
