[<AutoOpen>]
module PolyCoder.Preamble

let konst k _ = k

let flip fn x y = fn y x

let curry fn x y = fn(x, y)
let uncurry fn (x, y) = fn x y

let curry3 fn x y z = fn(x, y, z)
let uncurry3 fn (x, y, z) = fn x y z

let tee fn x = fn x; x
let teeIgnore fn = tee (fn >> ignore)
