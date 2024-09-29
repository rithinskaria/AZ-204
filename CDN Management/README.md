# Azure CDN Management Application

## Description
This C# console application demonstrates how to interact with Azure Content Delivery Network (CDN) using the Azure SDK. It lists all CDN profiles and their associated endpoints within a specified resource group.

## Features
- Authenticates with Azure using DefaultAzureCredential
- Connects to the Azure CDN Management API
- Lists all CDN profiles in a specified resource group
- For each profile, lists all associated endpoints

## Prerequisites
- An Azure account with an active subscription
- .NET 6.0 SDK or later
- Azure CLI installed and logged in (for DefaultAzureCredential to work)

## Required Packages
- Microsoft.Azure.Management.Cdn
- Azure.Identity

To install the required packages, run:
```
dotnet add package Microsoft.Azure.Management.Cdn
dotnet add package Azure.Identity
```

## Configuration
Before running the application, you need to configure the following:

1. Ensure you're logged in with Azure CLI:
   ```
   az login
   ```

2. In the `Main` method, replace the `SubscriptionId` with your actual Azure subscription ID:
   ```csharp
   SubscriptionId = "your-subscription-id"
   ```

3. In the `Main` method, replace the resource group name in the `ListProfilesAndEndpoints` call with your actual resource group name:
   ```csharp
   ListProfilesAndEndpoints(cdnClient, "your-resource-group-name");
   ```

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required packages.
3. Configure the application as described in the Configuration section.
4. Run the application:
   ```
   dotnet run
   ```

## Code Explanation

### Authentication
The application uses `DefaultAzureCredential` for authentication, which attempts to authenticate using multiple methods:

```csharp
var credential = new DefaultAzureCredential();
var tokenRequestContext = new Azure.Core.TokenRequestContext(new[] { "https://management.azure.com/.default" });
var token = await credential.GetTokenAsync(tokenRequestContext);
var serviceClientCredentials = new TokenCredentials(token.Token);
```

### CDN Client Initialization
A `CdnManagementClient` is created using the obtained credentials:

```csharp
var cdnClient = new CdnManagementClient(serviceClientCredentials)
{
    SubscriptionId = "your-subscription-id"
};
```

### Listing Profiles and Endpoints
The `ListProfilesAndEndpoints` method demonstrates how to list CDN profiles and their endpoints:

```csharp
static void ListProfilesAndEndpoints(CdnManagementClient cdnClient, string resourceGroupName)
{
    var profileList = cdnClient.Profiles.ListByResourceGroup(resourceGroupName);
    foreach (Profile p in profileList)
    {
        Console.WriteLine($"CDN profile: {p.Name}");
        Console.WriteLine("Endpoints:");
        var endpointList = cdnClient.Endpoints.ListByProfile(resourceGroupName, p.Name);
        foreach (Endpoint e in endpointList)
        {
            Console.WriteLine($"   - {e.Name} ({e.HostName})");
        }
        Console.WriteLine();
    }
}
```

## Sample Output
```
CDN profile: ProfileName1
Endpoints:
   - Endpoint1 (endpoint1.azureedge.net)
   - Endpoint2 (endpoint2.azureedge.net)

CDN profile: ProfileName2
Endpoints:
   - Endpoint3 (endpoint3.azureedge.net)
```

## Additional Notes
- This application uses the Azure CDN Management SDK, which allows for programmatic management of CDN resources.
- Ensure you have the necessary permissions in your Azure subscription to list CDN profiles and endpoints.
- For production use, consider implementing proper error handling and logging.
- The DefaultAzureCredential is suitable for most Azure SDK authentication scenarios. Make sure you're logged in with the correct Azure account when running the application.

## Troubleshooting
- If you encounter authentication errors, ensure you're logged in with Azure CLI and have the necessary permissions.
- Verify that the subscription ID and resource group name are correct.
- Check that you have CDN profiles created in the specified resource group.
