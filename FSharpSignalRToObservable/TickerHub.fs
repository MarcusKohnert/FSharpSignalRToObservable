namespace FSharpSignalRToObservable

open System
open System.Reactive
open System.Reactive.Linq
open Microsoft.AspNet.SignalR
open EkonBenefits.FSharp.Dynamic

type TickerHub() = 
    inherit Hub()
    
    static let subscription = 
        lazy (let obs = TimeSpan.FromSeconds 1.0 |> Observable.Interval
              obs.Subscribe (fun _ -> GlobalHost.ConnectionManager
                                                .GetHubContext<TickerHub>()
                                                .Clients.All?tick DateTime.Now)
        )
    
    member this.Register() = subscription.Value