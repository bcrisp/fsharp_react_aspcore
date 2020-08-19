namespace fsharp_react

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy;
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration    

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddControllers() |> ignore

        services.AddControllersWithViews() |> ignore
        
        services.AddSpaStaticFiles(fun configuration -> configuration.RootPath <- "ClientApp/build") |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseHttpsRedirection() |> ignore

        app.UseAuthorization() |> ignore

        app.UseSpaStaticFiles() |> ignore


        //app.UseEndpoints(fun endpoints ->
        //    endpoints.MapControllers() |> ignore
        //    ) |> ignore

        app.UseRouting() |> ignore

        app.UseEndpoints(fun endpoints ->
          //  endpoints.MapControllerRoute(name = "default", pattern = "{controller}/{action=Index}/{id?}") |> printf "%s"
          let a = endpoints.MapControllers()
          printfn "%s" "hi"
            ) |> ignore


        app.UseSpa(fun spa ->
            spa.Options.SourcePath <- "ClientApp"
            if env.IsDevelopment() then spa.UseReactDevelopmentServer("start")
        )
        //app.Use(fun environment next ->
        //    let awaitTask = Async.AwaitIAsyncResult >> Async.Ignore
            

        //    printfn "showing environment...."
        //    printfn "%A" environment.Items

        //    async { 
        //         do! awaitTask <| next.Invoke()
          //   } |> Async.StartAsTask :> Task) |> ignore

    member val Configuration : IConfiguration = null with get, set
