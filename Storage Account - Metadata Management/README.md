# Azure Blob Container Metadata Manager

## Description
This C# console application demonstrates how to manage metadata for an Azure Blob Container. It performs the following operations:
1. Creates a new container if it doesn't exist
2. Sets metadata on the container
3. Retrieves and displays the container's metadata

## How to Use
1. Ensure you have the necessary Azure SDK packages installed (see Required Packages section).
2. Replace the placeholder values for `connectionString` and `containerName` with your actual Azure Storage account details.
3. Run the application.
4. The program will create the container (if it doesn't exist), set metadata, and then display the metadata.

### Configuration
You need to configure the following variables in the `Main` method:
- `connectionString`: Your Azure Storage account connection string
- `containerName`: The name of the container you want to work with

## Required Packages
- Azure.Storage.Blobs (version 12.x.x or later)

To install the required package, run:
```
dotnet add package Azure.Storage.Blobs
```

## Code Explanation
1. The code creates a `BlobServiceClient` using your connection string.
2. It then gets a `BlobContainerClient` for the specified container.
3. The container is created if it doesn't already exist.
4. Metadata is set on the container using a `Dictionary<string, string>`.
5. Finally, it retrieves the container properties and displays the metadata.

```csharp
var metadata = new Dictionary<string, string>
{
    { "owner", "admin" },
    { "environment", "production" }
};
await containerClient.SetMetadataAsync(metadata);

BlobContainerProperties properties = await containerClient.GetPropertiesAsync();
Console.WriteLine("Container Metadata:");
foreach (var kvp in properties.Metadata)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}
```

## Additional Notes
- This example sets two metadata key-value pairs: "owner" and "environment". You can modify these or add more as needed.
- The application uses asynchronous methods (`async`/`await`) for better performance and scalability.
- Make sure to handle exceptions in a production environment, especially when working with network operations.
- Keep your connection string secure and never share it publicly. Consider using Azure Key Vault for storing sensitive information in a production environment.

## Sample Output
```
Metadata has been set successfully.
Container Metadata:
owner: admin
environment: production
```
