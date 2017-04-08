namespace VainZero.Florida

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

type Arguments =
  | [<CliPrefix(CliPrefix.None)>]
    [<AltCommandLine("dr")>]
    Daily_Report
    of ParseResults<DailyReportArguments>
  | [<CliPrefix(CliPrefix.None)>]
    [<AltCommandLine("wr")>]
    Weekly_Report
    of ParseResults<WeeklyReportArguments>
with
  interface IArgParserTemplate with
    override this.Usage =
      match this with
      | Daily_Report _ ->
        "日報の作業を行います。"
      | Weekly_Report _ ->
        "週報の作業を行います。"
