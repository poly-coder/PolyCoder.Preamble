namespace PolyCoder

type FoldStep<'a> =
  | Continue
  | ContinueWith of 'a
  | Break
  | BreakWith of 'a

module Seq =
  let foldWhile folder initialState (source: 'a seq) =
    use enumerator = source.GetEnumerator()

    let rec loop state =
      if enumerator.MoveNext() then
        match folder state enumerator.Current with
          | Continue -> loop state
          | ContinueWith state' -> loop state'
          | Break -> state
          | BreakWith state' -> state'
      else state

    loop initialState

  let foldWhileNone folder initialState source =
    let folder' state value =
      match folder state value with
        | None -> Continue
        | Some x -> BreakWith x
    foldWhile folder' initialState source

  let foldWhileSome folder initialState source =
    let folder' state value =
      match folder state value with
        | Some x -> ContinueWith x
        | None -> Break
    foldWhile folder' initialState source
