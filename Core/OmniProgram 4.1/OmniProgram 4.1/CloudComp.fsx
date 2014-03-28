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
type cloudComp (isLocal : bool, ?isMaster : bool, ?runtimes : int, ?URIs : string list) = class
    ,e,x.
    member x.init = 
        if isLocal then
            static public member runtime = MBrace.InitLocal 4
        else
            static public member runtime = MBrace.Boot(URIs : string list)

end
// create a local-only runtime
let init (runtimes:int)= // create a local-only runtime
        let runtime = MBrace.InitLocal 4
        runtime

// upload & execute
runtime.Run <@ hello () @>

// non-blocking process creation
let proc = runtime.CreateProcess <@ hello () @>

proc

proc.AwaitResult()

// show information
//runtime.ShowProcessInfo()
*)