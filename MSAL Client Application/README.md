# MSAL Graph Token Acquisition Example

## Description
This C# console application demonstrates how to use the Microsoft Authentication Library (MSAL) to acquire an access token for Microsoft Graph using the client credentials flow. This is typically used in daemon or service applications that run without user interaction.

## Features
- Uses MSAL to authenticate with Azure AD
- Acquires an access token for Microsoft Graph
- Demonstrates error handling for token acquisition

## Prerequisites
- .NET 6.0 SDK or later
- An Azure AD tenant
- A registered application in Azure AD with a client secret

## Required Packages
- Microsoft.Identity.Client

To install the required package, run:
```
dotnet add package Microsoft.Identity.Client
```

## Configuration
Before running the application, you need to configure the following variables in the `Program` class:

- `tenantId`: Your Azure AD tenant ID
- `clientId`: Your application's client ID (also known as application ID)
- `clientSecret`: Your application's client secret

You can find these values in the Azure Portal under your application's registration.

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required package.
3. Configure the application as described in the Configuration section.
4. Run the application:
   ```
   dotnet run
   ```

## Code Explanation

### Creating the Confidential Client Application
```csharp
IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(clientId)
    .WithClientSecret(clientSecret)
    .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
    .Build();
```
This code creates a confidential client application using the provided client ID, client secret, and tenant ID.

### Acquiring the Token
```csharp
var result = await app.AcquireTokenForClient(scopes)
                      .ExecuteAsync();
```
This line acquires an access token for the specified scopes (in this case, for Microsoft Graph).

### Displaying the Token
```csharp
Console.WriteLine("Access Token:");
Console.WriteLine(result.AccessToken);
```
The acquired access token is displayed in the console. In a real application, you would use this token to make authenticated requests to Microsoft Graph.

## Error Handling
The application includes a try-catch block to handle MSAL-specific exceptions:
```csharp
catch (MsalServiceException ex)
{
    Console.WriteLine($"Error acquiring token: {ex.Message}");
}
```

## Security Considerations
- Keep your client secret secure. Never commit it to source control or share it publicly.
- Use Azure Key Vault or similar secure storage for managing secrets in production environments.
- Ensure your application is granted only the necessary permissions in Azure AD.
- Regularly rotate your client secrets.

## Customization
- Modify the `scopes` array to request different permissions or access different resources.
- Implement token caching to improve performance and reduce the number of token requests.

## Troubleshooting
- If you encounter authentication errors, double-check your tenant ID, client ID, and client secret.
- Ensure that your application has the necessary API permissions granted in Azure AD.
- Check that your client secret hasn't expired.
- Verify that your Azure AD tenant and application are in a healthy state.

## Additional Resources
- [Microsoft Authentication Library (MSAL) documentation](https://docs.microsoft.com/en-us/azure/active-directory/develop/msal-overview)
- [Microsoft Graph documentation](https://docs.microsoft.com/en-us/graph/)
- [Azure Active Directory documentation](https://docs.microsoft.com/en-us/azure/active-directory/)
- [Best practices for Azure AD app registrations](https://docs.microsoft.com/en-us/azure/active-directory/develop/security-best-practices-for-app-registration)
