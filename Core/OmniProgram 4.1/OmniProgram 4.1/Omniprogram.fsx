namespace Omniprogram
open System
module UID = 
    let mutable private x:uint64 = uint64 -1
    let getNextUID =
        x <- x+ uint64 1
        x
open System.Collections.Generic
module user = 
    type UserLevel =  | Sys | Admin | ModuleWorker | SubAdmin | BasicUser | BackgroundWorker | None
    ///An abstract class which is the basis for all users
    [<AbstractClass>]
    type public user (username : string, password : string) = class
        member private x.level : UserLevel = UserLevel.None
        member private x.password = password
        member public x.username = username
        member public x.hashID = x.password.GetHashCode() + x.username.GetHashCode()
        member public x.changePassword (newPassword:string) = x.password = newPassword   
        member public x.passwordVerify (input : int) = if input = password.GetHashCode() then true else false
        member x.ID = UID.getNextUID
        member val isInit = false with get, set
            
    end
    type private Sys () = class inherit user ("","") end
    type private Admin (username : string, password : string) = class
        inherit user (username, password)
        member private x.level = UserLevel.BasicUser
    end
    //type private ModuleWorker (username : sting, password : string, from : util) = class inherits  end
    type private Basic (username : string, password, string) = class
        inherit user (username, password)
        member private x.level = UserLevel.BasicUser
    end

    let allUsers = new Dictionary<uint64, user>()
    let addUser (input : user) = 
        allUsers.Add user.ID