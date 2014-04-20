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
let ask (question : string) : string =    
    Console.Write(question)
    Console.ReadLine()
type user (username : string, password : string) = class
    member private x.password = password
    member public x.username = username
    member public x.hashID = x.password.GetHashCode() + x.username.GetHashCode()
    member public x.changePassword (newPassword:string) = x.password = newPassword   
    member public x.passwordVerify (input : int) = if input = password.GetHashCode() then true else false
end
let mutable anonUser = new user ("anon", "")
let mutable allUsers : user list = [anonUser;]
let mutable loc : int = 100
let mutable allUsersCurrent = anonUser
let login = 
    let u = ask("Username:\n>")
    let isRightUser (username : string, usertest : user) = 
        if usertest.username = username then true else false
    let p = ask("Password:\n>")
    let imaginaryUser = new user (u,p)
    let mutable i = 0
    let mutable found = false
    while i < allUsers.Length || found = false do 
        if allUsers.[i] = imaginaryUser then found <- true
    if found then
        let loc = Array.find(isRightUser)
        printf "loc = %A" loc
    else
        printf "failed"
    0
let newFunc (inputs : string []) = 
    if inputs.[0] = "user" then
        allUsers.[allUsers.Length] = new user (inputs.[1], inputs.[2]) |> ignore
    0
let recombineStr (input : string []) : string =
    input |> String.concat " "
let rec commandDetermine (input : string) =
        
        let lCaseInput : string = input.ToLower()
        let data = lCaseInput.Split ' '
        match data.[0] with 
            |"login" -> login
            |"new" -> newFunc (data.[1..])
            |_ -> 1
 
 
let core = 
        init |> ignore
        while true do
            commandDetermine (Console.ReadLine())|> ignore
        Console.ReadKey()|> ignore
        0
core

     