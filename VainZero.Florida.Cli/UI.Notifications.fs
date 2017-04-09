namespace VainZero.Florida.UI.Notifications

open VainZero.Misc

type ConsoleNotifier() =
  interface INotifier with
    override this.NotifyWarning(message) =
      eprintfn "警告: %s" message

    override this.Confirm(message) =
      Console.readYesNo message
