namespace FSharpSignalRToObservable

open Owin
open Microsoft.Owin

type Startup() =
    member this.Configuration(appBuilder:IAppBuilder) =
        appBuilder.MapSignalR() 
        |> ignore

module StartupModule =
    [<assembly:OwinStartup(typeof<Startup>)>]
    do()