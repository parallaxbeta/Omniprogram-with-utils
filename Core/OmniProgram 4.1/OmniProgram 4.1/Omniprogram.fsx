namespace Omniprogram
open System
module UID = 
    let mutable private x:uint64 = uint64 0
    let getNextUID =
        x
        x <- x+1 |> ignore
open System.Collections.Generic
module user = 
    type UserLevel =  | Sys | Admin | ModuleWorker | SubAdmin | BasicUser | BackgroundWorker | None
    ///An abstract class which is the basis for all users
    [<AbstractClass>]
    type public User (username : string, password : string) = class
        member private x.level : UserLevel = UserLevel.None
        member private x.password = password
        member public x.username = username
        member public x.hashID = x.password.GetHashCode() + x.username.GetHashCode()
        member public x.changePassword (newPassword:string) = x.password = newPassword   
        member public x.passwordVerify (input : int) = if input = password.GetHashCode() then true else false
        new (username : string, password : string) = 

    end
    type private Sys () = class inherit User ("","") end
    type private Admin (username : string, password : string) = class
        inherit User (username, password)
        member private x.level = UserLevel.BasicUser
    end
    //type private ModuleWorker (username : sting, password : string, from : util) = class inherits  end
    type private Basic (username : string, password, string) = class
        inherit User (username, password)
        member private x.level = UserLevel.BasicUser
    end
    let allUsers = new Dictionary <
