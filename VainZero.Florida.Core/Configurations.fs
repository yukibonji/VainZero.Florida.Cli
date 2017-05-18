namespace VainZero.Florida.Configurations

open System
open System.IO
open FsYaml
open VainZero.IO
open VainZero.Florida.Misc
open VainZero.Florida.UI.Notifications

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Config =
  let private configPath = @"./data/config.yaml"
  let private configTemplatePath = @"./data/config-template.yaml"

  let private createFromTemplate () =
    try
      File.Copy(configTemplatePath, configPath)
    with
    | e ->
      exn("設定ファイルの生成に失敗しました。", e) |> raise

  let private loadOrCreateAsync () =
    async {
      let! yaml = File.tryReadAllTextAsync configPath
      match yaml with
      | Ok yaml ->
        return yaml
      | Error _ ->
        createFromTemplate ()
        return! File.readAllTextAsync configPath
    }

  let loadAsync () =
    async {
      try
        let! yaml = loadOrCreateAsync ()
        return Yaml.myLoad<Config> yaml
      with
      | e ->
        return! exn("設定ファイルが見つからないか、解析に失敗しました。", e) |> raise
    }

  let internal test =
    {
      RootDirectory =
        "data"
      Department =
        Some "Testing Deparment"
      UserName =
        Some "John Doe"
      WeeklyReportConfig =
        {
          MeetingDay =
            DayOfWeek.Monday
        }
      DailyReportSubmitConfig =
        None
      TimeSheetConfig =
        {
          DefaultFirstTime =
            TimeSpan(9, 0, 0)
          DefaultRecess =
            TimeSpan(1, 0, 0)
        }
    }
