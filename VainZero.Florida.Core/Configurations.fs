namespace VainZero.Florida.Configurations

open System.IO
open FsYaml
open VainZero.Florida.UI.Notifications

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Config =
  let private empty =
    {
      RootDirectory =
        "./"
      Department =
        None
      UserName =
        None
      DailyReportSubmitConfig =
        None
    }

  let private configPath = @"florida-config.yaml"

  let load (notifier: INotifier) =
    try
      if File.Exists(configPath) |> not then
        notifier.NotifyWarning("設定ファイルが見つかりません。空の設定ファイルを生成しました。アプリケーションは既定の設定で起動します。")
        let yaml = empty |> Yaml.dump
        File.WriteAllText(configPath, yaml)
        empty
      else
        let yaml = File.ReadAllText(configPath)
        Yaml.load<Config> yaml
    with
    | e ->
      let message =
        sprintf "設定ファイルの解析に失敗しました。既定の設定を使用してアプリケーションを起動します。エラーメッセージは以下の通りです:\r\n%s" e.Message
      notifier.NotifyWarning(message)
      empty
