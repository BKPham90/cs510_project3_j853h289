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
        
        label [] [ encodedText "Entered Program:" ]
        p [ _id "entered" ] [ encodedText model.EnteredProgramText ]

        label [] [ encodedText "Parsed Program:"]
        p [ _id "parsed" ] [ encodedText model.ParsedProgramText ]
        
        label [] [ encodedText "Bracket Notation:"]
        p [ _id "code" ] [ encodedText model.BracketNotation ]
        
        // label [] [ encodedText "Abstract Syntax Tree:"]
        canvas [ _id "canvas"; ] []
    ]
    |> layout