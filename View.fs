module Project3.View

open Giraffe.ViewEngine

open Project3.Model

let layout (content: XmlNode list) =
    html [] [
        head [] [
            title []  [ encodedText "Project3" ]
            link [ 
                _rel "stylesheet"
                _type "text/css"
                _href "/main.css" 
            ]
        ]
        body [] content
    ]

let partial () =
    h1 [] [ encodedText "Project3" ]

let index (model : Message) =
    [
        partial()
        p [] [ encodedText model.Text ]
    ] |> layout