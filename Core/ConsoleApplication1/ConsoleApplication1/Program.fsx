#r "FSharp.Compiler.CodeDom.dll"
open System 
open System.CodeDom.Compiler 
open Microsoft.FSharp.Compiler.CodeDom

// Our (very simple) code string consisting of just one function: unit -> string 
let codeString =
    "module Synthetic.Code\n    let syntheticFunction() = \"I've been compiled on the fly!\""

// Assembly path to keep compiled code
let synthAssemblyPath = "synthetic.dll"
///Compiles and runs a System.String
let runString(codeString, synthAssemblyPath) =
        use provider = new FSharpCodeProvider() 
        let options = CompilerParameters([||], synthAssemblyPath) 
        let result = provider.CompileAssemblyFromSource( options, [|codeString|] ) 
        // If we missed anything, let compiler show us what's the problem
        if result.Errors.Count <> 0 then  
            for i = 0 to result.Errors.Count - 1 do
                printfn "%A" (result.Errors.Item(i).ErrorText)
        result.Errors.Count = 0

if runString(codeString, synthAssemblyPath) then
    let synthAssembly = Reflection.Assembly.LoadFrom(synthAssemblyPath) 
    let synthMethod  = synthAssembly.GetType("Synthetic.Code").GetMethod("syntheticFunction") 
    printfn "Success: %A" (synthMethod.Invoke(null, null))
else
    failwith "Compilation failed"
Console.ReadLine()
