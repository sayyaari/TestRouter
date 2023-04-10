

## Description
We have a main app (MainApp in this solution) which is our micro-frontend and some web-components developed outside of the main project.
Here MainApp acts as our main app and WebComponent acts as the web-component which has its own internal router. 

WebComponent registers itself as a web component named custom-component by
```
PreactCustomElement.register CustomComponent componentName
```

For ease of use the generated javascript file of web-component was copied into the content folder of Main app
and has been referenced in the index.dev.html

The main App has the following routes
 /
 /#hello
 /#custom ---> this will render the WebComponent
 / etc
 
Web-component has the following routes
 /#custom
 /#custom/1
 /#custom/2


I am not able to reproduce the error here even though I tried to mimic all our projects' structure and references. 

<br><b>Here is the scenario of the error:</b>

### Run the main app

```bash
dotnet tool restore
dotnet restore
npm install

npm start


```

## Development

- When hitting http://localhost:8080/#hello it shows the hello 
- When hitting http://localhost:8080/#custom it shows the web component
- In our app from now on, if you enter http://localhost:8080/#hello the UrlChanged of the MainApp and then the 
  WebComponent is triggered, and the latter one prevents the MainApp to display hello. But here it works correctly.
  In out App, The WebComponent won't be unmounted but here will be unmounted and all this happened after
  we upgraded the WebComponent references including the Feliz.Router to version 3.10.0. 
  If we just downgrade the Feliz.Router to any version below 3.10 it works correctly 
- But here, if we hit http://localhost:8080/#hello again, the WebComponent 's UrlChanged won't be triggered and
  WebComponent will be unmounted and hello will be displayed from the main app. changes is called with its root value i.e /#component rather than the actual url. 


