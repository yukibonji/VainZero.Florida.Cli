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

  let private configPath = @"./data/florida-config.yaml"

  let load (notifier: INotifier) =
    try
      let yaml = File.ReadAllText(configPath)
      Yaml.myLoad<Config> yaml
    with
    | e ->
      let message =
        sprintf "設定ファイルが見つからないか、解析に失敗しました。既定の設定を使用してアプリケーションを起動します。エラーメッセージは以下の通りです:\r\n%s" e.Message
      notifier.NotifyWarning(message)
      empty
