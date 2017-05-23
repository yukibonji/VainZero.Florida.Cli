namespace VainZero.Florida.UI.Notifications

open System
open VainZero.Misc

type ConsoleNotifier() =
  interface INotifier with
    override this.NotifyWarning(message) =
      eprintfn "警告: %s" message

    override this.Confirm(message) =
      Console.readYesNo message

    override this.GetPassword(message) =
      printf "%s: " message
      Console.ReadLine()
