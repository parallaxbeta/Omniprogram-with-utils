// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
#light
(*
#r "Nessos.MBrace"
#r "Nessos.MBrace.Utils"
#r "Nessos.MBrace.Common"
#r "Nessos.MBrace.Actors"
#load "./CloudComp.fsx"
open Nessos.MBrace
open Nessos.MBrace.Client
*)
open System
open System.IO
let toInt input = int input
let CopyrightNotice = "OmniProgram 4.1.1 © Harlan Connor\n"
let init = 
    //MBraceSettings.MBracedExecutablePath <- @"C:\Program Files (x86)\Nessos\MBrace\bin\mbraced.exe"
    //MBraceSettings.StoreProvider <- LocalFS
    printf "%s"CopyrightNotice
    0
    
type user (username : string, password : string) = class
    member private x.password = password
    member public x.username = username
    member public x.hashID = x.password.GetHashCode() + x.username.GetHashCode()
    member public x.changePassword (newPassword:string) = x.password = newPassword   
    member public x.passwordVerify (input : int) = if input = password.GetHashCode() then true else false
end
let mutable currUser = new user ("anon", "")
let allUsers = [currUser;]
let login = 0
let newFunc (inputs : string []) = 
    if inputs.[0] = "user" then
        currUser <- new user (inputs.[1], inputs.[2])
    0
//let recombineStr (input : string []) =
   // input |> String.Join ' '
let rec commandDetermine (input : string) =
        
        let lCaseInput : string = input.ToLower()
        let data = lCaseInput.Split ' '
        match data.[0] with 
            |"login" -> login
            |"new" -> newFunc (data.[1..])
            |"please" -> commandDetermine data.[1..]
            |_ -> 1
 
 
let core = 
        init |> ignore
        while true do
            commandDetermine (Console.ReadLine())|> ignore
        Console.ReadKey()|> ignore
        0
core

     