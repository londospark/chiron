[<AutoOpen>]
module internal Prelude

open System.Reflection
//open BenchmarkDotNet
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Analysers
open BenchmarkDotNet.Diagnosers
open BenchmarkDotNet.Jobs
//open BenchmarkDotNet.Diagnostics.Windows
open BenchmarkDotNet.Validators

type Dummy = Dummy

type CoreConfig() =
    inherit ManualConfig()
    do
        base.AddJob(Job.MediumRun) |> ignore
        base.AddAnalyser(EnvironmentAnalyser.Default) |> ignore
        base.AddDiagnoser(MemoryDiagnoser.Default) |> ignore
        base.AddValidator(BaselineValidator.FailOnError) |> ignore
        base.AddValidator(JitOptimizationsValidator.FailOnError) |> ignore

let thisAssembly = typeof<Dummy>.GetTypeInfo().Assembly

let loadJsonResource name =
        thisAssembly.GetManifestResourceStream(name + ".json")

let loadJsonResourceAsString name =
    use stream = loadJsonResource name
    use reader = new System.IO.StreamReader(stream)
    reader.ReadToEnd()
