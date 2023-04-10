namespace TestRouter3

[<RequireQualifiedAccess>]
module Config =

  open Browser

  let appRootId = "testrouter3-app"

  let appRoot = Dom.document.getElementById appRootId
