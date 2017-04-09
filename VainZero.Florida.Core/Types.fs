namespace VainZero.Florida
  open System

  type DateRange = DateTime * DateTime

namespace VainZero.Florida.Configurations
  open System

  /// 日報を送信するときの設定を表す。
  type DailyReportSubmitConfig =
    {
      /// メールの送信元のホストサーバーを取得する。
      Host:
        string
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
      /// メール送信時の設定を取得する。
      /// 省略された場合、メールは送信できない。
      DailyReportSubmitConfig:
        option<DailyReportSubmitConfig>
    }

namespace VainZero.Florida.Reports
  open System

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
        DateTime
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

namespace VainZero.Florida.Data
  open System
  open VainZero.Florida
  open VainZero.Florida.Reports

  type IDailyReportRepository =
    abstract Open: DateTime -> unit
    abstract FindAsync: DateTime -> Async<option<string * DailyReport>>

    /// 日報のうち日付が最小のものを取得する。
    abstract FirstDateAsync: Async<option<DateTime>>

  type IWeeklyReportRepository =
    abstract Open: DateRange -> unit
    abstract FindAsync: DateRange -> Async<option<WeeklyReport>>
    abstract AddOrUpdateAsync: DateRange * WeeklyReport -> Async<unit>

    /// 指定された日付より前にある週報のうち最新のものの、日付の区間を取得する。
    abstract LatestDateRangeAsync: DateTime -> Async<option<DateRange>>

  /// エクセルファイルを格納するリポジトリーを表す。
  /// エクセルファイルは XML 形式で保存される。
  type IWeeklyReportExcelRepository =
    abstract Open: DateRange -> unit
    abstract AddOrUpdateAsync: DateRange * string -> Async<unit>

  type IDataContext =
    inherit IDisposable

    abstract DailyReports: IDailyReportRepository
    abstract WeeklyReports: IWeeklyReportRepository
    abstract WeeklyReportExcels: IWeeklyReportExcelRepository

  type IDatabase =
    abstract Connect: unit -> IDataContext

namespace VainZero.Florida.UI.Notifications

  type INotifier =
    abstract NotifyWarning: string -> unit
    abstract Confirm: string -> bool
