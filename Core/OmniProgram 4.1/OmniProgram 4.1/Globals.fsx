(*type GlobalRuntime =
    abstract member Peek: unit -> int

let makeCounter initialState =
    let state = ref initialState
    { new GlobalVar with
        member x.Poke(n) = state := !state + n
        member x.Peek() = !state }
        *)
