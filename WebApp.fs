module Project3.WebApp

open Giraffe

open Project3.Model
// open Project3.View

open Absyn

open ParseAndRun

let rec printExpr (e : Absyn.expr) : string =
    match e with
    | CstI i -> sprintf "[CstI \"%d\"]" i
    | CstB b -> sprintf "[CstB \"%b\"]" b
    | Var x -> sprintf "[Var %s]" x
    | Prim(op, e1, e2) ->
        let s1 = printExpr e1
        let s2 = printExpr e2
        sprintf "[Prim \"%s\" %s %s]" op s1 s2
    | Let(x, eRhs, eBody) ->
        let rhsStr = printExpr eRhs
        let bodyStr = printExpr eBody
        sprintf "[Let %s %s %s]" x rhsStr bodyStr
    | If(e1, e2, e3) ->
        sprintf "[If %s %s %s]" (printExpr e1) (printExpr e2) (printExpr e3)
    | Letfun(f, x, fBody, letBody) ->
        sprintf "[Letfun %s %s %s %s]" f x (printExpr fBody) (printExpr letBody)
    | Call(eFun, eArg) ->
        sprintf "[Call %s %s]" (printExpr eFun) (printExpr eArg)

let indexHandler =
    let model = {
        ParsedProgramText = ""
        BracketNotation = ""
    }
    let view = View.index model
    htmlView view

let parseHandler : HttpHandler =
    fun next ctx ->
        task {
            let inputProgram = ctx.TryGetQueryStringValue "program"
            match inputProgram with
            | Some programText -> 
                let model = {
                    ParsedProgramText = sprintf "%A" (fromString programText)
                    BracketNotation = printExpr (fromString programText)
                }
                // let model = { Text = "[ x \"3\" ]"}
                let view = View.index model
                return! htmlView view next ctx
            | None ->
                let model = {
                    ParsedProgramText = "There was an error when trying to get the program text from the HTTP request."
                    BracketNotation = ""
                }
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