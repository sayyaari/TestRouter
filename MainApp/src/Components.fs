namespace TestRouter3

open Feliz
open Feliz.Router

type Components =
    /// <summary>
    /// The simplest possible React component.
    /// Shows a header with the text Hello World
    /// </summary>
    [<ReactComponent>]
    static member HelloWorld() = Html.h1 "Hello World"

    /// <summary>
    /// A stateful React component that maintains a counter
    /// </summary>
    [<ReactComponent>]
    static member Counter() =
        let (count, setCount) = React.useState (0)

        React.useEffectOnce (fun () ->
            printf "Counter Mounted"
            React.createDisposable (fun () -> printf "Counter Unmounted"))

        Html.div [ Html.h1 count
                   Html.button [ prop.onClick (fun _ -> setCount (count + 1))
                                 prop.text "Increment" ] ]


    /// <summary>
    /// A React component that uses Feliz.Router
    /// to determine what to show based on the current URL
    /// </summary>
    [<ReactComponent>]
    static member NestedRouter() =
        let (currentUrl, updateUrl) =
            React.useState (Router.currentUrl ())

        React.useEffectOnce (fun () ->
            printf "Nested Router Mounted"
            React.createDisposable (fun () -> printf "Nested Router Unmounted"))

        let urlChanged url =
            printf $"Nested Router Url Changed: {url}"
            updateUrl url

        printf $"Nested Router Current url: {currentUrl}"

        React.router [ router.onUrlChanged urlChanged
                       router.children [ match currentUrl with
                                         | [ "nested" ] -> Html.h1 "Nested Index"
                                         | [ "nested"; "1" ] -> Html.h1 "1"
                                         | [ "nested"; "2" ] -> Html.h1 "2"
                                         | otherwise -> Html.h1 "Nested: Not found" ] ]

    /// <summary>
    /// A React component that uses Feliz.Router
    /// to determine what to show based on the current URL
    /// </summary>
    [<ReactComponent>]
    static member Router() =
        let (currentUrl, updateUrl) =
            React.useState (Router.currentUrl ())

        let urlChanged url =
            printf $"Outer Router: Ur Changed: {url}"
            updateUrl url

        printf $"Outer Router Current url: {currentUrl}"

        React.router [ router.onUrlChanged urlChanged
                       router.children [ match currentUrl with
                                         | [] -> Html.h1 "Index"
                                         | [ "hello" ] -> Components.HelloWorld()
                                         | [ "counter" ] -> Components.Counter()
                                         | [ "nested"; _ ] -> Components.NestedRouter()
                                         | otherwise -> Html.h1 "Not found" ] ]
