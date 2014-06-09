#load @"./Omniprogram.fsx"
#load @"./GE.fsx"
#r @"C:\Program Files (x86)\Unity\Editor\Data\Managed\UnityEngine.dll"
namespace UnityStuffInFsharp
open UnityEngine
open System
    module Objects = 
        let makeSphere() = 
            let sphere : GameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere)
            ()