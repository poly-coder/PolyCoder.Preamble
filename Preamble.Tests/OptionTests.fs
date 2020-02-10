module PolyCoder.OptionTests

open NUnit.Framework
open FsCheck
open FsCheck.NUnit
open Swensen.Unquote

[<SetUp>]
let Setup () =
  ()

// Maybe

[<Property>]
let ``Maybe.result value should return Some value`` (value: int) =
  test <@ Maybe.result value = Some value @>

[<Property>]
let ``Maybe.zero should return Some()`` () =
  test <@ Maybe.zero = Some () @>

[<Property>]
let ``Maybe.returnFrom (Some value) should return Some value`` (value: int) =
  test <@ Maybe.returnFrom (Some value) = Some value @>

[<Property>]
let ``Maybe.returnFrom None should return None`` () =
  test <@ Maybe.returnFrom None = None @>

[<Property>]
let ``Maybe.bind on None should return None`` (mb: int option) =
  test <@ None |> Maybe.bind (fun _ -> mb) = None @>

[<Property>]
let ``Maybe.bind on None should not call the function`` (mb: int option) =
  let mutable calledTimes = 0
  let f _ =
    calledTimes <- calledTimes + 1
    mb
  None |> Maybe.bind f |> ignore
  test <@ calledTimes = 0 @>

[<Property>]
let ``Maybe.bind on Some value should return the second value`` (a: int) (mb: int option) =
  test <@ Some a |> Maybe.bind (fun _ -> mb) = mb @>

[<Property>]
let ``Maybe.bind on Some value should call the function exactly once`` (a: int) (mb: int option) =
  let mutable calledTimes = 0
  let f _ =
    calledTimes <- calledTimes + 1
    mb
  Some a |> Maybe.bind f |> ignore
  test <@ calledTimes = 1 @>

[<Property>]
let ``Maybe.bind on Some value should call the function with the value`` (a: int) (mb: int option) =
  let mutable calledWith = None
  let f x =
    calledWith <- Some x
    mb
  Some a |> Maybe.bind f |> ignore
  test <@ calledWith = Some a @>

// maybe

[<Property>]
let ``Maybe.maybe { return value } should return Some value`` (value: int) =
  test <@ maybe { return value } = Some value @>

[<Property>]
let ``Maybe.maybe { return! Some value } should return Some value`` (value: int) =
  test <@ maybe { return! Some value } = Some value @>

[<Property>]
let ``Maybe.maybe { return! None } should return None`` () =
  test <@ maybe { return! None } = None @>
