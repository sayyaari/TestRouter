namespace WebComponent

module App =

    open Elmish
    open Feliz
    open Feliz.Router
    open Feliz.UseElmish

    [<Literal>]
    let componentName = "custom-component"

    type State = { CurrentUrl: string list }

    type Msg = UrlChanged of string list

    module State =

        let init () =

            { CurrentUrl = Router.currentUrl () }, Cmd.none

        let update (msg: Msg) (state: State) =
            match msg with
            | UrlChanged url -> { state with CurrentUrl = url }, Cmd.none


    [<ReactComponent>]
    let CustomComponent () =

        let state, dispatch =
            React.useElmish (State.init, State.update)


        React.useEffectOnce (fun () ->
            printf "Custom Component mount"
            React.createDisposable (fun () -> printf "Custom Component dismount"))

        let urlChanged url =
            printf $"Custom Component Router: Ur Changed: {url}"
            dispatch (UrlChanged url)

        printf $"Custom Component  Current url: {state.CurrentUrl}"


        Feliz.React.router [ router.onUrlChanged urlChanged
                             router.children [ match state.CurrentUrl with
                                               | [ "custom" ] -> Html.h1 "Custom Component Index"
                                               | [ "custom"; "1" ] -> Html.h1 "Custom Component Path 1"
                                               | [ "custom"; "2" ] -> Html.h1 "Custom Component Path 2"
                                               | otherwise -> Html.h1 "Custom Component Not found" ] ]

    PreactCustomElement.register CustomComponent componentName
