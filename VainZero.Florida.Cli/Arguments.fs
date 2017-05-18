namespace VainZero.Florida

open System
open Argu

type DailyReportArguments =
  | [<CliPrefix(CliPrefix.None)>]
    New
  | [<CliPrefix(CliPrefix.None)>]
    Mail
with
  interface IArgParserTemplate with
    override this.Usage =
      match this with
      | New ->
        "日報の雛形を生成します。"
      | Mail ->
        "日報のメールを送信します。"

type WeeklyReportArguments =
  | [<CliPrefix(CliPrefix.None)>]
    New
  | [<CliPrefix(CliPrefix.None)>]
    Excel
with
  interface IArgParserTemplate with
    override this.Usage =
      match this with
      | New ->
        "週報の雛形を生成します。"
      | Excel ->
        "週報をエクセルファイルに変換して開きます。"

type TimeSheetArguments =
  | [<CliPrefix(CliPrefix.None)>]
    Update
  | [<CliPrefix(CliPrefix.None)>]
    Excel
with
  interface IArgParserTemplate with
    override this.Usage =
      match this with
      | Update ->
        "勤務表を更新します。"
      | Excel ->
        "前月の勤務表をエクセルファイルに変換して開きます。"

type Arguments =
  | [<CliPrefix(CliPrefix.None)>]
    [<AltCommandLine("dr")>]
    Daily_Report
    of ParseResults<DailyReportArguments>
  | [<CliPrefix(CliPrefix.None)>]
    [<AltCommandLine("wr")>]
    Weekly_Report
    of ParseResults<WeeklyReportArguments>
  | [<CliPrefix(CliPrefix.None)>]
    [<AltCommandLine("ts")>]
    Time_Sheet
    of ParseResults<TimeSheetArguments>
with
  interface IArgParserTemplate with
    override this.Usage =
      match this with
      | Daily_Report _ ->
        "日報の作業を行います。"
      | Weekly_Report _ ->
        "週報の作業を行います。"
      | Time_Sheet _ ->
        "勤務表の作業を行います。"

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Arguments =
  let parser = ArgumentParser.Create<Arguments>()

  let private usageCommand (parser: ArgumentParser<_>) =
    Command.Usage (parser.PrintUsage())

  let parse args =
    let date = DateTime.Now
    let parseResults = parser.ParseCommandLine(args, raiseOnUsage = false)
    if parseResults.IsUsageRequested then
      parseResults.Parser |> usageCommand
    else
      match parseResults.GetSubCommand() with
      | Daily_Report parseResults ->
        if parseResults.Contains(<@ DailyReportArguments.New @>) then
          Command.DailyReportCreate date
        else if parseResults.Contains(<@ DailyReportArguments.Mail @>) then
          Command.DailyReportSendMail date
        else
          parseResults.Parser |> usageCommand
      | Weekly_Report parseResults ->
        if parseResults.Contains(<@ WeeklyReportArguments.New @>) then
          Command.WeeklyReportCreate date
        else if parseResults.Contains(<@ WeeklyReportArguments.Excel @>) then
          Command.WeeklyReportConvertToExcel date
        else
          parseResults.Parser |> usageCommand
      | Time_Sheet parseResults ->
        if parseResults.Contains(<@ TimeSheetArguments.Update @>) then
          Command.TimeSheetUpdate date
        else if parseResults.Contains(<@ TimeSheetArguments.Excel @>) then
          Command.TimeSheetExcel date
        else
          parseResults.Parser |> usageCommand
