namespace PolyCoder

type AsyncFn<'a, 'b> = 'a -> Async<'b>

[<RequireQualifiedAccess>]
module AsyncFn =
  let result v = fun _ -> Async.result v
  let raise e = fun _ -> Async.raise e

  let bind f ma = ma >> (Async.bind f)
  let map f ma = ma >> (Async.map f)
  let ignore ma = ma |> map ignore

  let ofTask ma = ma >> Async.ofTask
  let ofTaskVoid ma = ma >> Async.ofTaskVoid
  let toTask ma = ma >> Async.toTask
  let toTaskVoid ma = ma >> Async.toTaskVoid

  let ofFn fn = fn >> Async.result

  let bindTask f ma = ma >> (Async.bindTask f)
  let bindTaskVoid f ma = ma >> (Async.bindTaskVoid f)
  let mapTask f ma = ma >> (Async.mapTask f)
  let mapTaskVoid f ma = ma >> (Async.mapTaskVoid f)
