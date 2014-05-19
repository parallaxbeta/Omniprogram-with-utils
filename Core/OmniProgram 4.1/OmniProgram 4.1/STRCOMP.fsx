module Omniprogram.StringCompile
#r "FSharp.Compiler.dll"
#r "FSharp.Compiler.CodeDom.dll"
open System 
open System.CodeDom.Compiler 
open Microsoft.FSharp.Compiler.CodeDom 

// Our (very simple) code string consisting of just one function: unit -> string 
let codeString =
    "module fsharp
        let Omniprogram() = 
            printf \"Hello World\""

// Assembly path to keep compiled code
let synthAssemblyPath = "omniprogram.fsharp.dll"

let CompileFSharpCode(codeString, synthAssemblyPath) =
        use provider = new FSharpCodeProvider() 
        let options = CompilerParameters([||], synthAssemblyPath) 
        let result = provider.CompileAssemblyFromSource( options, [|codeString|] ) 
        // If we missed anything, let compiler show us what's the problem
        if result.Errors.Count <> 0 then  
            for i = 0 to result.Errors.Count - 1 do
                printfn "%A" (result.Errors.Item(i).ErrorText)
        result.Errors.Count = 0

if CompileFSharpCode(codeString, synthAssemblyPath) then
    try
        let synthAssembly = Reflection.Assembly.LoadFrom(synthAssemblyPath) 
        let synthMethod  = synthAssembly.GetType("fsharp").GetMethod("Omniprogram") 
        printfn "Success: %A" (synthMethod.Invoke(null, null))
    with
        |_ -> failwith "module non-existent"
else
    failwith "Compilation failed"