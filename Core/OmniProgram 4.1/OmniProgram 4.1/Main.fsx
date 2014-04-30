namespace Main
open System
module user = 
    type UserLevel =  | Sys | Admin | ModuleWorker | SubAdmin | BasicUser | BackgroundWorker | None
    ///An abstract class which is the basis for all users
    [<AbstractClass>]
    type private User (username : string, password : string) = class
        member private x.level : UserLevel = UserLevel.None
        member private x.password = password
        member public x.username = username
        member public x.hashID = x.password.GetHashCode() + x.username.GetHashCode()
        member public x.changePassword (newPassword:string) = x.password = newPassword   
        member public x.passwordVerify (input : int) = if input = password.GetHashCode() then true else false
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

    //let anonUser = new User ("anon", "")
    //let mutable private allUsers : User list = [anonUser;]
    ///The current Main.user.User in the program
    //let mutable CurrentUsers = anonUser
    //let isAuthorised (testUser : User, level : UserLevel, allowBackground : bool) = 
      //  if testUser.ToString() =
///The basic code required to run OmniProgram
module CoreFunc =
    let ask (question : string) =    
        printf "%s" question
        Console.ReadLine() : string
    ///Main Runtime  
    let core = 
        0

