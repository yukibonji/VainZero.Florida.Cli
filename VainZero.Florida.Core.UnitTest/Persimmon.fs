namespace Persimmon

[<AutoOpen>]
module Operators =
  let test = Syntax.UseTestNameByReflection.test

  let is = assertEquals

  let sync = Async.RunSynchronously
