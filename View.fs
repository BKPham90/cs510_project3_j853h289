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
        h1 [] [ encodedText "Project 3 - MicroML Abstract Syntax Tree Generator" ]
        form [ _method "GET"; _action "/parse" ] [
            button [ _type "submit"; _class "button-action" ] [ encodedText "Parse" ]
            input [ _type "text"; _name "program"; _placeholder "Enter program here" ]
        ]
        
        br []

        label [ _class "code-title" ] [ encodedText "Entered Program:" ]
        p [ _id "entered"; _class "code" ] [ encodedText model.EnteredProgramText ]

        label [ _class "code-title" ] [ encodedText "Parsed Program:"]
        p [ _id "parsed"; _class "code" ] [ encodedText model.ParsedProgramText ]
        
        label [ _class "code-title" ] [ encodedText "Bracket Notation:"]
        p [ _id "code"; _class "code" ] [ encodedText model.BracketNotation ]
        
        // label [] [ encodedText "Abstract Syntax Tree:"]
        canvas [ _id "canvas"; ] []
    ]
    |> layout