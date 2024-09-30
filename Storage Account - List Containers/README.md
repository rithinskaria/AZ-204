# Azure Blob Storage: List Containers

## Description
This C# console application demonstrates how to connect to Azure Blob Storage using DefaultAzureCredential and list all containers in the storage account. It's a simple example of interacting with Azure Blob Storage using the Azure SDK.

## Features
- Connects to Azure Blob Storage using DefaultAzureCredential
- Lists all containers in the storage account

## Prerequisites
- .NET 6.0 SDK or later
- An Azure account with an active subscription
- An Azure Storage account
- Appropriate RBAC roles assigned to the user or service principal (e.g., "Storage Blob Data Reader")

## Required Packages
- Azure.Identity
- Azure.Storage.Blobs

To install the required packages, run:
```
dotnet add package Azure.Identity
dotnet add package Azure.Storage.Blobs
```

## Configuration
Before running the application, you need to replace the placeholder in the `Main` method:

- Replace `<your-storage-account-name>` with your actual Azure Storage account name in the `blobServiceEndpoint` string.

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required packages.
3. Configure the application as described in the Configuration section.
4. Ensure you're logged in with Azure CLI or have the appropriate environment variables set for DefaultAzureCredential.
5. Run the application:
   ```
   dotnet run
   ```

## Code Explanation

### Connecting to Blob Storage
```csharp
string blobServiceEndpoint = "https://<your-storage-account-name>.blob.core.windows.net";
var credential = new DefaultAzureCredential();
BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobServiceEndpoint), credential);
```
This code creates a `BlobServiceClient` using the storage account endpoint and DefaultAzureCredential.

### Listing Containers
```csharp
static async Task ListContainersAsync(BlobServiceClient blobServiceClient)
{
    Console.WriteLine("Listing containers...");
    await foreach (var container in blobServiceClient.GetBlobContainersAsync())
    {
        Console.WriteLine($"Container: {container.Name}");
    }
}
```
This method uses the `GetBlobContainersAsync()` method to list all containers in the storage account.

## Security Considerations
- Use Azure Key Vault to store and manage the storage account connection string securely.
- Ensure that the DefaultAzureCredential has the minimum necessary permissions in Azure RBAC.
- Always use HTTPS (which this code does by default) when connecting to Azure Storage.

## Troubleshooting
- If you encounter authentication errors, ensure you're properly authenticated with Azure (e.g., logged in with Azure CLI).
- Verify that the storage account name is correct.
- Check that your Azure account has the necessary permissions to list containers in the storage account.

## Additional Resources
- [Azure Blob Storage documentation](https://docs.microsoft.com/en-us/azure/storage/blobs/)
- [DefaultAzureCredential documentation](https://docs.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential)
- [Azure.Storage.Blobs SDK documentation](https://docs.microsoft.com/en-us/dotnet/api/azure.storage.blobs)