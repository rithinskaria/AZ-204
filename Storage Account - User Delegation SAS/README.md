# Azure Blob Storage User Delegation SAS Token Generator

## Description
This C# console application demonstrates how to generate a User Delegation SAS (Shared Access Signature) token for Azure Blob Storage. User Delegation SAS tokens provide a more secure way to grant limited access to Azure Storage resources without sharing your storage account key.

## Features
- Uses DefaultAzureCredential for authentication
- Generates a User Delegation Key
- Creates a SAS token with specified permissions and expiry time
- Outputs a full URL with the SAS token for accessing a specific blob

## Prerequisites
- .NET 6.0 SDK or later
- An Azure account with an active subscription
- An Azure Storage account
- Appropriate RBAC roles assigned to the user or service principal (e.g., "Storage Blob Data Contributor")

## Required Packages
- Azure.Storage.Blobs
- Azure.Identity

To install the required packages, run:
```
dotnet add package Azure.Storage.Blobs
dotnet add package Azure.Identity
```

## Configuration
Before running the application, you need to configure the following variables in the `Main` method:

- `storageAccountName`: Your Azure Storage account name
- `containerName`: The name of the container containing the blob
- `blobName`: The name of the blob you want to generate a SAS token for

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

### Authentication
```csharp
var credential = new DefaultAzureCredential();
```
This line uses DefaultAzureCredential, which attempts various authentication methods including environment variables, managed identity, and Azure CLI.

### Creating BlobServiceClient
```csharp
string blobServiceUri = $"https://{storageAccountName}.blob.core.windows.net";
BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobServiceUri), credential);
```
This creates a BlobServiceClient using the storage account URL and the DefaultAzureCredential.

### Generating User Delegation Key
```csharp
UserDelegationKey userDelegationKey = await blobServiceClient.GetUserDelegationKeyAsync(keyStart, keyExpiry);
```
This generates a User Delegation Key, which is used to create the SAS token.

### Building the SAS Token
```csharp
BlobSasBuilder sasBuilder = new BlobSasBuilder
{
    BlobContainerName = containerName,
    BlobName = blobName,
    Resource = "b",
    StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
};
sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Write);
```
This configures the SAS token with specific permissions and expiry time.

### Generating the SAS Token URL
```csharp
string sasToken = sasBuilder.ToSasQueryParameters(userDelegationKey, storageAccountName).ToString();
UriBuilder fullUri = new UriBuilder($"{blobServiceUri}/{containerName}/{blobName}")
{
    Query = sasToken
};
```
This generates the full URL with the SAS token for accessing the blob.

## Security Considerations
- User Delegation SAS tokens are more secure than Account SAS tokens as they don't require the storage account key.
- Ensure that the DefaultAzureCredential has the minimum necessary permissions in Azure RBAC.
- Always use HTTPS (which this code does by default) when working with SAS tokens.
- Set appropriate expiration times for SAS tokens. This example uses 1 hour, which may need adjustment based on your use case.
- Avoid sharing the generated SAS URL in logs or insecure channels.

## Customization
- Adjust the `StartsOn` and `ExpiresOn` times to change the validity period of the SAS token.
- Modify the permissions in `SetPermissions()` to grant different levels of access.
- Extend the code to generate SAS tokens for multiple blobs or containers.

## Troubleshooting
- If you encounter authentication errors, ensure you're properly authenticated with Azure (e.g., logged in with Azure CLI).
- Verify that the storage account name, container name, and blob name are correct.
- Check that your Azure account has the necessary permissions to generate user delegation keys and create SAS tokens.
- If the SAS token doesn't work, verify that the clock on your local machine is synchronized.

## Additional Resources
- [Azure Blob Storage documentation](https://docs.microsoft.com/en-us/azure/storage/blobs/)
- [User Delegation SAS documentation](https://docs.microsoft.com/en-us/rest/api/storageservices/create-user-delegation-sas)
- [DefaultAzureCredential documentation](https://docs.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential)
- [Best practices for using SAS](https://docs.microsoft.com/en-us/azure/storage/common/storage-sas-overview#best-practices-when-using-sas)
