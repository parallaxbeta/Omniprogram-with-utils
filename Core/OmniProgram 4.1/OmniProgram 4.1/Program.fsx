#light
open System
open System.IO
open System.Security.Cryptography
#r "Nessos.MBrace"
open Nessos
open Nessos.MBrace
open Nessos.MBrace.CloudModule
open Nessos.MBrace.CloudRefModule
open Nessos.MBrace.CloudExtensions
#load "STRCOMP.fsx"
open Main.StringCompile
module Main = 
    core |> ignore
    let toInt input = int input
    let encript (data : string, key : string) = Security.Cryptography.RSA.Create(data).EncryptValue(System.Text.Encoding.ASCII.GetBytes(key))
    let decript (data : byte [], key : string) = Security.Cryptography.RSA.Create(System.Text.Encoding.ASCII.GetString (data)).DecryptValue(System.Text.Encoding.ASCII.GetBytes(key))
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
    ///Determines the OmniProgramScript (OPS) of the System.String
    let rec commandDetermine (input : string) =
        
            let lCaseInput : string = input.ToLower()
            let data = lCaseInput.Split ' '
            match data.[0] with 
                |"login" -> login
                |"new" -> newFunc (data.[1..])
                |_ -> 1



     