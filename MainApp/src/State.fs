namespace TestRouter3

open Types
open Elmish
open Feliz.Router

module State =

    let init () =
        let currentPage = Router.currentUrl ()

        let state = { CurrentUrl = currentPage }

        state, Cmd.none

    let update (msg: Msg) (state: State) =
        match msg with
        | UrlChanged segments -> { state with CurrentUrl = segments }, Cmd.none
