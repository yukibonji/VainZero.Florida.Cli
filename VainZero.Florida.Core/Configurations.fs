namespace VainZero.Florida.Configurations

open System
open System.IO
open FsYaml
open VainZero.Florida.Misc
open VainZero.Florida.UI.Notifications

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Config =
  let private empty =
    {
      RootDirectory =
        "data"
      Department =
        None
      UserName =
        None
      DailyReportSubmitConfig =
        None
      WeeklyReportConfig =
        {
          MeetingDay =
            DayOfWeek.Friday
        }
      TimeSheetConfig =
        {
          DefaultFirstTime =
            TimeSpan(9, 0, 0)
          DefaultRecess =
            TimeSpan(1, 0, 0)
        }
    }

  let private configPath = @"./data/config.yaml"
  let private configTemplatePath = @"./data/config-template.yaml"

  let private tryLoad () =
    try
      if File.Exists(configPath)
      then File.ReadAllText(configPath) |> Some
      else None
    with
    | _ -> None

  let private createFromTemplate () =
    try
      File.Copy(configTemplatePath, configPath)
    with
    | e ->
      exn("設定ファイルの生成に失敗しました。", e) |> raise

  let private loadOrCreate () =
    match tryLoad () with
    | Some yaml -> yaml
    | None ->
      createFromTemplate ()
      File.ReadAllText(configPath)

  let load () =
    try
      let yaml = loadOrCreate ()
      Yaml.myLoad<Config> yaml
    with
    | e ->
      exn("設定ファイルが見つからないか、解析に失敗しました。", e) |> raise
