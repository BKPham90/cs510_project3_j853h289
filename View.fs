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
            script [ _type "module"; _src "/syntaxtree.js"; ] []
        ]
        body [] content
    ]

let index (model : Message) =
    [
        h1 [] [ encodedText "Project3" ]
        form [ _method "GET"; _action "/parse" ] [
            button [ _type "submit" ] [ encodedText "Parse" ]
            input [ _type "text"; _name "program"; _placeholder "Enter program here" ]
        ]
        p [ _id "parsed" ] [ encodedText model.ParsedProgramText ]
        p [ _id "code" ] [ encodedText model.BracketNotation ]
        // button [ _id "drawButton"; _type "button" ] [ encodedText "Draw Tree"]
        canvas [ _id "canvas"; ] []
    ]
    |> layout