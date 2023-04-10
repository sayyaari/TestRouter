namespace TestRouter3

module Types =

    type State = { CurrentUrl: string list }

    type Msg = UrlChanged of string list
