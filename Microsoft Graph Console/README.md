# MSAL Graph API with Device Code Flow

## Description
This C# console application demonstrates how to use the Microsoft Authentication Library (MSAL) with device code flow to authenticate a user and then use Microsoft Graph API to retrieve user information. This authentication method is particularly useful for devices without a web browser or with limited input capabilities.

## Features
- Uses MSAL to authenticate with Azure AD using device code flow
- Acquires an access token for Microsoft Graph
- Retrieves and displays user information using Microsoft Graph API

## Prerequisites
- .NET 6.0 SDK or later
- An Azure AD tenant
- A registered application in Azure AD (configured for public client)
- Microsoft Graph API permissions (User.Read) granted to the application

## Required Packages
- Microsoft.Identity.Client
- Microsoft.Graph

To install the required packages, run:
```
dotnet add package Microsoft.Identity.Client
dotnet add package Microsoft.Graph
```

## Configuration
Before running the application, you need to configure the following variables in the `Program` class:

- `clientId`: Your application's client ID (also known as application ID)
- `tenantId`: Your Azure AD tenant ID

You can find these values in the Azure Portal under your application's registration.

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required packages.
3. Configure the application as described in the Configuration section.
4. Run the application:
   ```
   dotnet run
   ```
5. Follow the device code instructions displayed in the console to authenticate.

## Code Explanation

### Creating the Public Client Application
```csharp
var publicClientApp = PublicClientApplicationBuilder
    .Create(clientId)
    .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
    .WithRedirectUri("http://localhost")
    .Build();
```
This code creates a public client application for the device code flow.

### Acquiring the Token with Device Code Flow
```csharp
var authResult = await publicClientApp
    .AcquireTokenWithDeviceCode(scopes, deviceCodeResult =>
    {
        Console.WriteLine(deviceCodeResult.Message);
        return Task.FromResult(0);
    })
    .ExecuteAsync();
```
This initiates the device code flow, displaying the authentication instructions to the user.

### Creating the Graph Client
```csharp
using var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
var graphClient = new GraphServiceClient(httpClient);
```
This creates a Graph client using the acquired access token.

### Retrieving User Information
```csharp
var user = await graphClient.Me.GetAsync();
Console.WriteLine($"Display Name: {user?.DisplayName}");
Console.WriteLine($"User Principal Name: {user?.UserPrincipalName}");
Console.WriteLine($"Job Title: {user?.JobTitle ?? "N/A"}");
Console.WriteLine($"Mobile Phone: {user?.MobilePhone ?? "N/A"}");
```
This code retrieves and displays user information using the Graph API.

## Security Considerations
- Ensure that your application is granted only the necessary permissions in Azure AD.
- The device code flow should only be used when browser-based authentication is not possible.
- Be cautious about displaying or logging sensitive user information.

## Customization
- Modify the `scopes` array to request different permissions.
- Extend the application to retrieve additional user information or perform other Graph API operations.

## Troubleshooting
- If you encounter authentication errors, double-check your client ID and tenant ID.
- Ensure that your application has the necessary API permissions granted in Azure AD.
- Verify that you're using the correct account to sign in during the device code flow.
- Check that your Azure AD tenant and application are in a healthy state.

## Additional Resources
- [Microsoft Authentication Library (MSAL) documentation](https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-overview)
- [Microsoft Graph documentation](https://docs.microsoft.com/en-us/graph/)
- [Device code flow documentation](https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-device-code)
- [Microsoft Graph .NET SDK documentation](https://docs.microsoft.com/en-us/graph/sdks/sdk-installation#install-the-microsoft-graph-net-sdk)
