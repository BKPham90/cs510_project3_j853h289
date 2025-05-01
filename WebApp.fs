module Project3.WebApp

open Giraffe

open Project3.Model
// open Project3.View

open ParseAndRun

let indexHandler =
    let model     = { Text = "" }
    let view      = View.index model
    htmlView view

let parseHandler : HttpHandler =
    fun next ctx ->
        task {
            let inputProgram = ctx.TryGetQueryStringValue "program"
            match inputProgram with
            | Some programText -> 
                // let model = { Text = sprintf "%A" (fromString programText) }
                let model = { Text = "[ x \"3\" ]"}
                let view = View.index model
                return! htmlView view next ctx
            | None ->
                let model = { Text = "There was an error when trying to get the program text from the HTTP request." }
                let view = View.index model
                return! htmlView view next ctx
        }

let webApp: HttpHandler =
    choose [
        GET >=>
            choose [
                route "/" >=> indexHandler
                route "/parse" >=> parseHandler
            ]
        setStatusCode 404 >=> text "Not Found" ]

// Test programs:
//      let y = 7 in y + 2 end