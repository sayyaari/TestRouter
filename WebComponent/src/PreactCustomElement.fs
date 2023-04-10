namespace WebComponent

[<RequireQualifiedAccess>]
module PreactCustomElement =

  open Fable.Core

  [<ImportDefault("preact-custom-element")>]
  let private registerInternal (input: obj * string ): unit = jsNative
  let register reactComponent name = registerInternal (reactComponent, name)
  
