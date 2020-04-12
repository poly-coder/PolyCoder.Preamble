namespace PolyCoder

open System.Reflection

[<RequireQualifiedAccess>]
module Exn =
  let preserveStackTrace =
    lazy typeof<exn>.GetMethod(
      "InternalPreserveStackTrace",
      BindingFlags.Instance ||| BindingFlags.NonPublic)
    
  let inline reraise exn =
    (exn, null)
      |> preserveStackTrace.Value.Invoke
      |> ignore

    raise exn
