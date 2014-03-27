(*
#r "Nessos.MBrace"
#r "Nessos.MBrace.Utils"
#r "Nessos.MBrace.Common"
#r "Nessos.MBrace.Actors"
#r "Nessos.MBrace.Store"
#r "Nessos.MBrace.Client"

open Nessos.MBrace
open Nessos.MBrace.Client

let runtime = MBrace.InitLocal 4
[<Cloud>]
let compile input cloud{
    
}
*)
// Assembly references for intellisense purposes only
#r "Nessos.MBrace"
#r "Nessos.MBrace.Utils"
#r "Nessos.MBrace.Common"
#r "Nessos.MBrace.Actors"
#r "Nessos.MBrace.Store"
#r "Nessos.MBrace.Client"

open Nessos.MBrace
open Nessos.MBrace.Client

// a simple cloud expression

[<Cloud>]
let hello () =
    cloud {
        return "hello, world!"
    }


// create a local-only runtime
let init (runtimes:int)= // create a local-only runtime
        let runtime = MBrace.InitLocal 4
        runtime

let init (runtimes, URIs:string list)= // create a remote runtime
    if isLocal then
        let runtime = MBrace.InitLocal 4
        runtime
    else 
         if isMaster then
            let runtime = MBrace.Boot(URIs)
            runtime
         else
            let runtime = MBrace.ConnectAsync()
            0
let runtime = MBrace.InitLocal 4

// upload & execute
runtime.Run <@ hello () @>

// non-blocking process creation
let proc = runtime.CreateProcess <@ hello () @>

proc

proc.AwaitResult()

// show information
//runtime.ShowProcessInfo()