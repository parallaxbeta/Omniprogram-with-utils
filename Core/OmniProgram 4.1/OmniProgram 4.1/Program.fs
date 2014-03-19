// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System
open System.IO
let readLines filePath:string = 
    let FileStream = new IO.FileStream(filePath,FileMode.Open)
    let buffer = []
    FileStream.BeginRead
    "bob"
let CopyrightNotice = "OmniProgram 4.1.0 Build © Harlan Connor"
let BuildInc =
    let curr = readLines("1.txt") |> ignore
    0
[<EntryPoint>]
let main argv = 
    BuildInc



