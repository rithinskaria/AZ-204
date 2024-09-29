# Azure Blob Container SAS Token Generator

## Description
This C# console application generates a Shared Access Signature (SAS) token for an Azure Blob Storage container. The SAS token provides temporary, secure access to the container without sharing the storage account key.

## Features
- Generates a SAS token for a specific Azure Blob Storage container
- Sets custom permissions (Read, Write, List)
- Configures token expiration time

## Prerequisites
- .NET 6.0 SDK or later
- An Azure account with an active subscription
- An Azure Storage account

## Required Packages
- Azure.Storage.Blobs

To install the required package, run:
```
dotnet add package Azure.Storage.Blobs
```

## Configuration
Before running the application, you need to configure the following variables in the `Main` method:

- `storageAccountName`: Your Azure Storage account name
- `storageAccountKey`: Your Azure Storage account key
- `containerName`: The name of the container for which you want to generate a SAS token

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required package.
3. Configure the application as described in the Configuration section.
4. Run the application:
   ```
   dotnet run
   ```
5. The application will output a SAS Token URL for the specified container.

## Code Explanation

1. Creating a `StorageSharedKeyCredential`:
   ```csharp
   StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);
   ```

2. Initializing `BlobServiceClient` and `BlobContainerClient`:
   ```csharp
   string blobServiceUrl = $"https://{storageAccountName}.blob.core.windows.net";
   BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobServiceUrl), sharedKeyCredential);
   BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
   ```

3. Building the SAS token:
   ```csharp
   BlobSasBuilder sasBuilder = new BlobSasBuilder
   {
       BlobContainerName = containerName,
       Resource = "c",
       StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
       ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
   };
   sasBuilder.SetPermissions(BlobContainerSasPermissions.Read | BlobContainerSasPermissions.Write | BlobContainerSasPermissions.List);
   ```

4. Generating the SAS token URL:
   ```csharp
   string sasToken = sasBuilder.ToSasQueryParameters(sharedKeyCredential).ToString();
   UriBuilder fullUri = new UriBuilder($"{blobServiceUrl}/{containerName}")
   {
       Query = sasToken
   };
   ```

## Customization
- You can adjust the token expiration time by modifying the `ExpiresOn` property of the `BlobSasBuilder`.
- Permissions can be customized by changing the parameters in the `SetPermissions` method.

## Security Considerations
- Keep your storage account key secure and never share it publicly.
- Use the principle of least privilege when setting SAS token permissions.
- Consider using Azure Key Vault to securely store and retrieve your storage account key.

## Troubleshooting
- If you encounter authentication errors, double-check your storage account name and key.
- Ensure that the container name is correct and that the container exists in your storage account.
- Verify that your Azure subscription is active and that you have the necessary permissions to access the storage account.

## Additional Resources
- [Azure Blob Storage documentation](https://docs.microsoft.com/en-us/azure/storage/blobs/)
- [SAS Token overview](https://docs.microsoft.com/en-us/azure/storage/common/storage-sas-overview)
- [Best practices when using SAS](https://docs.microsoft.com/en-us/azure/storage/common/storage-sas-overview#best-practices-when-using-sas)

