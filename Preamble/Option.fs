namespace PolyCoder

[<RequireQualifiedAccess>]
module Option =
  ()

[<RequireQualifiedAccess>]
module Maybe =
  let result x = Some x
  
  let zero = result ()
  
  let returnFrom x = x

  let bind f = function Some x -> f x | None -> None

  let map f = bind (f >> result)
  
  let combine mb = bind (konst mb)

  type Builder() =
    member _.Zero() = zero

    member _.Return x = Some x

    member _.ReturnFrom x = x

    member _.Delay f = f()
    
    member _.Run f = f

    member _.Bind(ma, f) = ma |> bind f

    member _.Combine(ma, mb) = ma |> combine mb

    member _.TryWith(body, handler) =
      try body()
      with exn -> handler exn

    member _.TryFinally(body, handler) =
      try body()
      finally handler ()

    member _.Using(expression, body) =
      use expr = expression
      body expr

    member _.While(guard, body) =
      while guard() do
        body()

    member _.For(source: _ seq, body) =
      use enumerator = source.GetEnumerator()
      while enumerator.MoveNext() do
        body enumerator.Current


[<AutoOpen>]
module DefaultMaybeBuilder =
  let maybe = Maybe.Builder()
