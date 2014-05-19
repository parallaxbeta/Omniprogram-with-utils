module Omniprogram.GamingEngine
open System
open System.Collections.Generic
type coordinates (x : float, ?y : float, ?z : float) = 
    member val x = x with get, set
    member val y = y with get, set
    member val z = z with get, set
    member x.location = (x,y,z)
type IObject =
     abstract member init : unit -> unit
     abstract member name : string
     abstract member location : coordinates
type IMoveable =
    abstract member move : int -> coordinates -> unit
[<AbstractClass>]
type Destroyable (thing : unit) = 
     abstract member health : int with get,set
     abstract member damage : int with get,set
     abstract member shield : int with get,set
     abstract member destroy : unit
     member x.hurt (amount : int) =
        for i in [amount .. -1 .. 1] do
            if x.shield > 0 then
                 x.shield <- x.shield - 1
            else
                x.health <- x.health - 1 

type ship (name : string, spawnLocation : coordinates) = class
    interface IObject with
        member x.name = name
        member x.location = spawnLocation
        member x.init() = ()
end
let index = new Dictionary<string,ship>()
let addShip (input : ship) = 
    let x = input :> IObject
    index.Add (x.name, input)

