namespace TestRouter3


module App =

    open Elmish.React
    open Fable.Core.JsInterop
    open Feliz
    open Feliz.Router
    open Elmish
    open State
    open Types

    importSideEffects "./styles.css"

    let render (state: State) (dispatch: Msg -> unit) =
        let urlChanged url =
            printf $"Outer Router: Ur Changed: {url}"
            dispatch (UrlChanged url)

        printf $"Outer Router Current url: {state.CurrentUrl}"

        React.router [ router.onUrlChanged urlChanged
                       router.children [ match state.CurrentUrl with
                                         | [] -> Html.h1 "Index"
                                         | [ "hello" ] -> Components.HelloWorld()
                                         | [ "counter" ] -> Components.Counter()
                                         | "nested":: _ -> Components.NestedRouter()
                                         | "custom":: _ -> CustomComponent.render()
                                         | otherwise -> Html.h1 "Not found" ] ]

    Program.mkProgram init update render
    |> Program.withReactSynchronous Config.appRootId
    |> Program.run
