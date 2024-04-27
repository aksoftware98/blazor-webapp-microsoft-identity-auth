# AKSoftware.Blazor.Templates v1.0.0 for Blazor Web App

Blazor Web App template with Server and WebAssembly interactivity enabled. The template has the Microsoft Identity Platform configured so you can start building apps authenticated with Microsoft Entra ID (Azure Active Directory) or Azure Active Directory B2C right out of the box.

## What is Blazor Web App

The new Blazor Web App model released with .NET 8.0 is a huge milestone in web development with .NET.
Blazor Web App allows you to have a traditional web flow experience in addition to an interactive experience using Blazor Server and WebAssembly all in the same application. It opens the door to develop not just an app that runs completely on the server like Razor Pages or MVC, or an app that runs completely on the client like WebAssembly, but brings the best of both worlds to deliver a state-of-the-art experience for your end-users.

The advantages of utilizing a Blazor Web App can be seen in different areas like:
- **Better development experience:** You can have your back-end and front-end all in the same project and you don't have to host two different projects.
- **Flexibility:** When you are developing a web app with the new Blazor Web App template, you have all the flexibility while developing, for example, one page can be loaded using normal web flow like (request/response) just like MVC request or Razor pages. If you want to provide your users with advanced interactions like forms, tables, and more complex UI components, you can mark that page or component to run via Blazor Server model or WebAssembly, Blazor Web App will handle the load of this component or page in the most efficient way.
- **Performance:** The app is running on the client and the server at the same time, so the user won't have to wait until the full WebAssembly app is loaded to the client, and the server won't be handling all the requests for everything including the complex interactive components. Blazor Web App also uses Server-Side Rendering by default which makes loading the pages and components to the client happen instantly.
- **Security:** As the main app is running on the server, the client security won't be a big deal here.

## Why this template
By default, the Blazor Web App template allows you to pick authentication with **Individual User Accounts**, the authentication model is utilizing **ASP.NET Core Identity** with a local database. This hosting model is great but many modern applications use an Open ID Connect identity provider like Active Directory B2C, Microsoft Entra ID, Auth0, and more. Open ID Connect is not provided by the default template, and setting this up from scratch is a tricky process especially when your app has WebAssembly components so you need to handle the server and the client authentication.

*AKSoftware.Blazor.Templates* provides an out-of-the-box template with the Microsoft Identity Platform enabled, so you can directly put your configuration from Azure and you have Microsoft Identity enabled in your app server and client for WebAssembly pages and components.

## Get Started
To get started you can run the following *dotnet CLI* command to install the template and that's all you need:
```bash
dotnet new install AKSoftware.Blazor.Templates
```
Once the template is installed you can create a new application using the following command:
```bash
dotnet new blazorweb-ms-auth -n YOUR_PROJECT_NAME -o FOLDER_NAME
```
That's it all!!!
After running the command, a Blazor Web App project will be created containing the server app and a client app that contains the WebAssembly part.

Open the **appsettings.json** in the server project and replace the placeholders of the **AzureAdB2C** object with your own:

```json
...
 "AzureAdB2C": {
    "Instance": "https://YOUR_INSTANCE_NAME.b2clogin.com",
    "ClientId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "Domain": "YOUR_INSTANCE_DOMAIN",
    "SignUpSignInPolicyId": "SIGN_UP_SIGN_IN_POLICY"
  }
...
```
By default, it's using AzureAdB2C, but you can simply add your config of Microsoft Entra ID app and you can start.
If you changed the name of the property for example from **AzureAdB2C** to **AzureAd**, make sure to change it in the **Program.cs** file line: 20.

## Issues
If you encounter any error while working on your app, make sure to submit an issue here [Open an issue](https://github.com/aksoftware98/blazor-webapp-microsoft-identity-auth/issues/new)

Thanks for using the template

## Developer
The template by Ahamd Mozaffar. You can check [my website](https://ahmadmozaffar.net)

If you want to learn Blazor you can check my book too [Amazon](https://www.amazon.com/Mastering-Blazor-WebAssembly-developing-Application/dp/1803235101/ref=tmm_pap_swatch_0?_encoding=UTF8&qid=&sr=)
