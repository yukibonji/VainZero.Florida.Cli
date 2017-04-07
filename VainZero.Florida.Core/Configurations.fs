namespace VainZero.Florida.Configurations

open System.IO
open FsYaml

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Config =
  let empty =
    {
      ReportsDir =
        "./"
      Department =
        None
      UserName =
        None
      Mail =
        None
    }

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module App =
  let configPath = @"florida-config.yaml"

  let config =
    try
      let yaml = File.ReadAllText(configPath)
      in Yaml.load<Config> yaml
    with
    | e ->
        eprintfn "WARNING! The settings file is not found or invalid. Start with default settings. Error message is following:"
        eprintfn "%s" e.Message
        Config.empty
