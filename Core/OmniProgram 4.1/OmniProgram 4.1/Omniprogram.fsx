namespace Omniprogram
open System
module omniAddOn = 
    type IOAO =
        abstract member name : string
        abstract member location : string
module Logs = 
    type IStream =
        abstract member write : string -> unit
        ///retrieves the data at the specified line number (starting at one)
        abstract member readln : int -> string
        abstract member data : string list
    type ILog =
        inherit IStream
        abstract member lastLog : unit -> string
        abstract member length : int
    type GeneralLog() = class
        interface ILog with
            member x.data : string list = []
            member x.write (input) = input :: (x :> ILog).data |> ignore
            member x.readln (lineNo) = (x :> ILog).data.[lineNo-1]
            member x.lastLog () = (x :> ILog).data.[(x :> ILog).data.Length-1]
            member x.length = (x :> ILog).data.Length-1
    end
    let private secretLog = new GeneralLog()
    let DiagLog = secretLog :> ILog
module UID = 
    let mutable private x:uint64 = uint64 0
    let getNextUID () =
        x <- x+ uint64 1
        x - 1uL
module UI = 
    ///Writes a string, then reads in the rest of the line, which is typed in by the user
    let prompt input=
        printf "%s" input
        Console.ReadLine()
    ///Writes a line of a string, then reads in the following user-written line
    let ask input = 
        printfn "%s" input
        Console.ReadLine()
open System.Collections.Generic
module User = 
    open System.Threading
    type UserLevel =  | Sys | Admin | ModuleWorker | BasicUser | BackgroundWorker | Undefined
    ///An abstract class which is the basis for all users
    [<AbstractClass>]
    type user (username : string, password : string) = class
        member x.ID = UID.getNextUID
        member x.level : UserLevel = Undefined
        member private x.password = password
        member x.username = username
        member x.hashID = x.password.GetHashCode() + x.username.GetHashCode()
        member x.changePassword (newPassword:string) = x.password = newPassword   
        member x.passwordVerify (input : int) = if input = password.GetHashCode() then true else false
    end
    ///The non-creatable System Worker class
    type private Sys () = class inherit user ("","") end
    type private Admin (username, password) = class
        inherit user (username, password)
        member x.level = UserLevel.Sys
    end
    ///The OAO worker class
    type private ModuleWorker (username, password, owner : omniAddOn.IOAO) = class
         inherit user (username, password)
         member x.owner = owner
         member x.level = UserLevel.ModuleWorker
     end
    type private Basic (username, password) = class
        inherit user (username, password)
        member x.level = UserLevel.BasicUser
    end
    type private BackgroundWorker (username : string, password) = class
        inherit user (username, password)
        member x.level = UserLevel.BackgroundWorker
    end
    let allUsers = new Dictionary<uint64, user>()
    let public addUser (usrname, pswd, level, init : user -> user, OAO : omniAddOn.IOAO option) = 
        let newObject : user = match level with
            |Admin -> new Admin(usrname, pswd) :> user
            |BackgroundWorker -> new BackgroundWorker(usrname,pswd) :> user
            |BasicUser -> new Basic(usrname,pswd) :> user
            |ModuleWorker -> new ModuleWorker (usrname,pswd,OAO.Value) :> user
        let x = new Threading.ParameterizedThreadStart(fun input -> ())
        let y = new Thread(x)
        y.Name <- usrname
        y.Start(newObject)
        let var = newObject.ID()
        allUsers.Add (var, newObject)
        y
module Encription = 
    let encript (data : string, key : string) = Security.Cryptography.RSA.Create(data).EncryptValue(System.Text.Encoding.ASCII.GetBytes(key))
    let decript (data : byte [], key : string) = Security.Cryptography.RSA.Create(System.Text.Encoding.ASCII.GetString (data)).DecryptValue(System.Text.Encoding.ASCII.GetBytes(key))
module OEXT = 
    let readOFSF (location : string) = 
        let reader = new System.IO.FileStream (location, IO.FileMode.Open)
        let buffer:byte[] = Array.empty
        let fileLength = reader.Length
        reader.Read(buffer, 0, int fileLength) |> ignore
        