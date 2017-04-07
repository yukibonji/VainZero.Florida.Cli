namespace Reports

open VainZero.Misc

module ``日別の内容`` =
  open Types.WeeklyReports

  let ofMap self =
    {
      ``日``                = self |> Map.tryFind "日"
      ``月``                = self |> Map.tryFind "月"
      ``火``                = self |> Map.tryFind "火"
      ``水``                = self |> Map.tryFind "水"
      ``木``                = self |> Map.tryFind "木"
      ``金``                = self |> Map.tryFind "金"
      ``土``                = self |> Map.tryFind "土"
    }

  let toList self =
    [
      self.``日``
      self.``月``
      self.``火``
      self.``水``
      self.``木``
      self.``金``
      self.``土``
    ]

  let toMap self =
    List.zip (DayOfWeek.kanjis |> Array.toList) (self |> toList)
    |> List.choose (fun (k, vOpt) -> vOpt |> Option.map (fun v -> (k, v)))
    |> Map.ofList
