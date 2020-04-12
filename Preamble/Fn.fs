namespace PolyCoder

open System.Threading.Tasks

type Fn<'a, 'b> = 'a -> 'b

[<RequireQualifiedAccess>]
module Fn =
  ()

type Sink<'a> = Fn<'a, unit>

module Sink =
  let ofAsync sink fn =
    async {
      let! result = fn()
      sink result
    } |> Async.Start

  let toAsync (processFn: Sink<Sink<'value>>) =
    let source = TaskCompletionSource()
    let sink value = source.TrySetResult(value) |> ignore
    processFn(sink)
    source.Task |> Async.AwaitTask

type ResultSink<'a> = Sink<Result<'a, exn>>

module ResultSink =
  let ofAsync sink fn =
    async {
      try
        let! result = fn()
        sink (Ok result)
      with
        exn -> sink (Error exn)
    } |> Async.Start

  let toAsync (processFn: Sink<ResultSink<'value>>) =
    let source = TaskCompletionSource()
    let sink = function
      | Ok value -> source.TrySetResult(value) |> ignore
      | Error exn -> source.TrySetException(exn: exn) |> ignore
    processFn(sink)
    source.Task |> Async.AwaitTask
