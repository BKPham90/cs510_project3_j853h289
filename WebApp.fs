module Project3.WebApp

open Giraffe

open Project3.Model
// open Project3.View

open ParseAndRun

let indexHandler (name : string) =
    // let greetings = sprintf "Hello %s, from Giraffe!" name
    let greetings = sprintf "%A" (fromString("let y = 7 in y + 2 end"))
    let model     = { Text = greetings }
    let view      = View.index model
    htmlView view

let webApp: HttpHandler =
    choose [
        GET >=>
            choose [
                route "/" >=> indexHandler "world"
                routef "/hello/%s" indexHandler
            ]
        setStatusCode 404 >=> text "Not Found" ]