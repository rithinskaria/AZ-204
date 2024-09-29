# Azure Storage SAS Token Generator

## Description
This C# console application generates a Shared Access Signature (SAS) token for Azure Storage. The SAS token provides temporary, secure access to resources in an Azure Storage account without needing to share the account key.

## How to Use
1. Ensure you have the necessary Azure SDK packages installed (see Required Packages section).
2. Replace the placeholder values for `storageAccountName` and `storageAccountKey` with your actual Azure Storage account details.
3. Run the application.
4. The program will output a SAS Token URL that can be used to access your Azure Storage resources.

### Configuration
You need to configure the following variables in the `Main` method:
- `storageAccountName`: Your Azure Storage account name
- `storageAccountKey`: Your Azure Storage account key

## Required Packages
- Azure.Storage.Blobs (version 12.x.x or later)

To install the required package, run:
```
dotnet add package Azure.Storage.Blobs
```

## Code Explanation
1. The code creates a `StorageSharedKeyCredential` using your storage account name and key.
2. It then builds an `AccountSasBuilder` with the following properties:
   - Services: Blobs and Files
   - Resource Types: Service, Container, and Object
   - Protocol: HTTPS only
   - Expiration: 1 hour from the current time
   - Permissions: Read, Write, and List
3. The SAS token is generated using the `ToSasQueryParameters` method.
4. Finally, it constructs a full URL including the SAS token and prints it to the console.

```csharp
AccountSasBuilder sasBuilder = new AccountSasBuilder
{
    Services = AccountSasServices.Blobs | AccountSasServices.Files,  
    ResourceTypes = AccountSasResourceTypes.Service | AccountSasResourceTypes.Container | AccountSasResourceTypes.Object, 
    Protocol = SasProtocol.Https, 
    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1) 
};
sasBuilder.SetPermissions(AccountSasPermissions.Read | AccountSasPermissions.Write | AccountSasPermissions.List);
```

## Additional Notes
- The generated SAS token is valid for 1 hour. You can adjust the expiration time by modifying the `AddHours(1)` part of the code.
- Ensure that you keep your storage account key secure and never share it publicly.
- For production use, consider using Azure Key Vault to securely store and retrieve your storage account key.
