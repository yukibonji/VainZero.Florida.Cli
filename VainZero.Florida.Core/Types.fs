namespace VainZero.Florida.Configurations
  open System

  type SmtpServer =
    {
      Name:
        string
      Port:
        int
    }

  /// 日報を送信するときの設定を表す。
  type DailyReportSubmitConfig =
    {
      /// メールの送信元のホストサーバーを取得する。
      SmtpServer:
        SmtpServer
      /// メールの送信者の名前を取得する。
      SenderName:
        string
      /// メールの送信者のメールアドレスを取得する。
      SenderAddress:
        string
      Password:
        option<string>
      To:
        array<string>
      CC:
        array<string>
      Bcc:
        array<string>
      Header:
        option<string>
      Footer:
        option<string>
    }

  /// 週報の設定を表す。
  type WeeklyReportConfig =
    {
      /// 週例会議が開催される曜日を取得する。
      MeetingDay: DayOfWeek
    }

  type TimeSheetConfig =
    {
      DefaultFirstTime:
        TimeSpan
      DefaultRecess:
        TimeSpan
    }

  type Config =
    {
      // 日報などを保存するディレクトリ―を取得する。
      RootDirectory:
        string
      /// 所属部署を取得する。
      Department:
        option<string>
      /// ユーザーの名前を取得する。
      UserName:
        option<string>
      /// 週報の設定を取得する。
      WeeklyReportConfig:
        WeeklyReportConfig
      /// メール送信時の設定を取得する。
      /// 省略された場合、メールは送信できない。
      DailyReportSubmitConfig:
        option<DailyReportSubmitConfig>
      TimeSheetConfig:
        TimeSheetConfig
    }

namespace VainZero.Florida.Reports
  open System
  open System.Collections.Generic
  open VainZero.Misc

  /// 作業の記録を表す。
  type Work =
    {
      ``案件``:
        string
      ``内容``:
        string
      ``工数``:
        float
      ``備考``:
        option<string>
    }

  /// 日報を表す。
  type DailyReport =
    {
      ``作業実績``:
        array<Work>
      ``翌営業日の予定``:
        string
      ``その他``:
        option<string>
      CC:
        option<array<string>>
    }

  /// 職員を表す。
  type Staff =
    {
      ``所属部署``:
        string
      ``名前``:
        string
    }

  /// 日報を簡略化したものを表す。週報に埋め込まれる。
  type SimplifiedDailyReport =
    {
      /// 作業日を取得する。
      ``日付``:
        Date
      /// その日の作業内容のリストを取得する。備考は無視される。
      ``作業実績``:
        array<Work>
    }

  /// 週報を表す。
  type WeeklyReport =
    {
      ``担当者``:
        Staff
      ``今週の主な活動``:
        string
      ``進捗``:
        string
      ``日別の内容``:
        array<SimplifiedDailyReport>
      ``今週実績``:
        string
      ``来週予定``:
        string
      ``その他``:
        string
    }

  type TimeSheetItem =
    {
      ``開始時刻``:
        option<TimeSpan>
      ``終了時刻``:
        option<TimeSpan>
      ``休憩時間``:
        option<TimeSpan>
      ``備考``:
        option<string>
    }

  /// 勤務表を表す。
  type TimeSheet =
    array<KeyValuePair<int, TimeSheetItem>>

namespace VainZero.Florida.Data
  open System
  open VainZero.Misc
  open VainZero.Florida
  open VainZero.Florida.Reports

  type ParsableEntry<'TSource, 'TTarget> =
    | ParsableEntry
      of 'TSource * 'TTarget
    | UnparsableEntry
      of 'TSource * exn
    | UnexistingParsableEntry

  type IDailyReportRepository =
    abstract Open: DateTime -> unit
    abstract FindAsync: DateTime -> Async<ParsableEntry<string, DailyReport>>

    /// 指定された日付の日報の雛形を生成する。
    abstract ScaffoldAsync: DateTime -> Async<unit>

    /// 日報のうち日付が最小のものを取得する。
    abstract FirstDateAsync: Async<option<DateTime>>

  type IWeeklyReportRepository =
    abstract Open: DateRange -> unit
    abstract FindAsync: DateRange -> Async<ParsableEntry<string, WeeklyReport>>
    abstract AddOrUpdateAsync: DateRange * WeeklyReport -> Async<unit>

    /// 指定された日付より前にある週報のうち最新のものの、日付の区間を取得する。
    abstract LatestDateRangeAsync: DateTime -> Async<option<DateRange>>

  /// エクセルファイルを格納するリポジトリーを表す。
  /// エクセルファイルは XML 形式で保存される。
  type IWeeklyReportExcelRepository =
    abstract Open: DateRange -> unit
    abstract AddOrUpdateAsync: DateRange * string -> Async<unit>

  type ITimeSheetRepository =
    abstract FindAsync: month: DateTime -> Async<option<TimeSheet>>
    abstract AddOrUpdateAsync: month: DateTime * TimeSheet -> Async<unit>

  type ITimeSheetExcelRepository =
    abstract Open: month: DateTime -> unit
    abstract ExistsAsync: month: DateTime -> Async<bool>
    abstract AddOrUpdateAsync: month: DateTime * content: string -> Async<unit>

  type IDataContext =
    inherit IDisposable

    abstract DailyReports: IDailyReportRepository
    abstract WeeklyReports: IWeeklyReportRepository
    abstract WeeklyReportExcels: IWeeklyReportExcelRepository
    abstract TimeSheets: ITimeSheetRepository
    abstract TimeSheetExcels: ITimeSheetExcelRepository

  type IDatabase =
    abstract Connect: unit -> IDataContext

namespace VainZero.Florida.UI.Notifications

  type INotifier =
    abstract NotifyWarning: string -> unit
    abstract Confirm: string -> bool
