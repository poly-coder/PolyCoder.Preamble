module PolyCoder.PreambleTests

open NUnit.Framework
open FsCheck.NUnit
open Swensen.Unquote

[<SetUp>]
let Setup () =
  ()

[<Property>]
let ``konst should always return a constant function`` (k: int) (any: int) =
  test <@ (konst k) any = k @>

[<Property>]
let ``flip should always return a flipped function`` (a: int) (b: int) =
  let f x y = 10 * x + y  
  test <@ (flip f) a b = f b a @>

[<Property>]
let ``curry should always return a curried function`` (a: int) (b: int) =
  let f (x, y) = 10 * x + y  
  test <@ (curry f) a b = f(a, b) @>

[<Property>]
let ``uncurry should always return an uncurried function`` (a: int) (b: int) =
  let f x y = 10 * x + y
  test <@ (uncurry f) (a, b) = f a b @>

[<Property>]
let ``curry3 should always return a curried function`` (a: int) (b: int) (c: int) =
  let f (x, y, z) = 100 * x + 10 * y + z
  test <@ (curry3 f) a b c = f(a, b, c) @>

[<Property>]
let ``uncurry3 should always return an uncurried function`` (a: int) (b: int) (c: int) =
  let f x y z = 100 * x + 10 * y + z
  test <@ (uncurry3 f) (a, b, c) = f a b c @>

[<Property>]
let ``tee should always return the input value`` (input: int) =
  test <@ input |> tee ignore = input @>

[<Property>]
let ``tee should always call the given function with the given input`` (input: int) =
  let mutable calledWith = None
  let f x = calledWith <- Some x
  input |> tee f |> ignore
  test <@ calledWith = Some input @>

[<Property>]
let ``teeIgnore should always return the input value`` (input: int) (any: int) =
  test <@ input |> teeIgnore (konst any) = input @>

[<Property>]
let ``teeIgnore should always call the given function with the given input`` (input: int) (any: int) =
  let mutable calledWith = None
  let f x =
    calledWith <- Some x
    any
  input |> teeIgnore f |> ignore
  test <@ calledWith = Some input @>
