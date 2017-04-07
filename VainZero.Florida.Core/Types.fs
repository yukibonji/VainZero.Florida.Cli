namespace VainZero.Florida.Configurations

  type MailSettings =
    {
      Host                  : string
      /// Your short name is recommended.
      Name                  : string
      Addr                  : string
      Password              : option<string>
      TOs                   : array<string>
      CCs                   : array<string>
      BCCs                  : array<string>
      /// Mail body header.
      Header                : option<string>
      /// Mail body footer.
      Footer                : option<string>
    }

  type Config =
    {
      ReportsDir            : string
      /// 所属部署
      Department            : option<string>
      /// ユーザーの名前
      UserName              : option<string>
      /// メール送信時の設定
      Mail                  : option<MailSettings>
    }

namespace VainZero.Florida.Reports

  type ``作業項目`` =
    {
      ``案件``              : string
      ``内容``              : string
      ``工数``              : float
      ``備考``              : option<string>
    }

  type ``作業実績`` =
    array<``作業項目``>

  type ``日報`` =
    {
      ``作業実績``          : ``作業実績``
      ``翌営業日の予定``    : string
      ``その他``            : option<string>
      CCs                   : option<array<string>>
    }

  type ``担当者`` =
    {
      ``所属部署``          : string
      ``名前``              : string
    }

  type ``日別`` =
    ``作業項目``

  type ``日別の内容`` =
    {
      ``日``                : option<array<``日別``>>
      ``月``                : option<array<``日別``>>
      ``火``                : option<array<``日別``>>
      ``水``                : option<array<``日別``>>
      ``木``                : option<array<``日別``>>
      ``金``                : option<array<``日別``>>
      ``土``                : option<array<``日別``>>
    }

  type ``週報`` =
    {
      ``担当者``            : ``担当者``
      ``今週の主な活動``    : string
      ``進捗``              : string
      ``日別の内容``        : ``日別の内容``
      ``今週実績``          : string
      ``来週予定``          : string
      ``その他``            : string
    }
