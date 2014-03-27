// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
#light
#r "Nessos.MBrace"
#r "Nessos.MBrace.Utils"
#r "Nessos.MBrace.Common"
#r "Nessos.MBrace.Actors"
#r "Nessos.MBrace.Store"
#load "./CloudComp.fsx"
open Nessos.MBrace
open Nessos.MBrace.Client
open System
open System.IO
let toInt input = int input
let CopyrightNotice = "OmniProgram 4.1.1 © Harlan Connor"

let init = 
    MBraceSettings.MBracedExecutablePath <- @"C:\Program Files (x86)\Nessos\MBrace\bin\mbraced.exe"
    MBraceSettings.StoreProvider <- LocalFS
    printf "%s"CopyrightNotice
    0
    
let commandDetermine (input:string) = 
    let mutable data = input.ToLower()
    let data = data.Split(' ')
    match (data.[1]) with
    |"cloudinit" -> if data.[2] = "local" then CloudComp.init(toInt(data.[3])) |> ignore 
    |"cloudscript" -> CloudComp.hello |> ignore 
    |_ -> printf "I do not understand what %s means" input
    0
[<EntryPoint>]
let main argv = 
    init |> ignore

    1



     