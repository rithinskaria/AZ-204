using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Azure.Identity;

class Program
{
    static async Task Main(string[] args)
    {

        string storageAccountName = "";
        string containerName = "";
        string blobName = ""; 
        var credential = new DefaultAzureCredential();
        string blobServiceUri = $"https://{storageAccountName}.blob.core.windows.net";
        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobServiceUri), credential);


        DateTimeOffset keyStart = DateTimeOffset.UtcNow;
        DateTimeOffset keyExpiry = keyStart.AddHours(1);
        UserDelegationKey userDelegationKey = await blobServiceClient.GetUserDelegationKeyAsync(keyStart, keyExpiry);
        BlobSasBuilder sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerName,
            BlobName = blobName, 
            Resource = "b", 
            StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5), 
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Write);
        string sasToken = sasBuilder.ToSasQueryParameters(userDelegationKey, storageAccountName).ToString();

        UriBuilder fullUri = new UriBuilder($"{blobServiceUri}/{containerName}/{blobName}")
        {
            Query = sasToken
        };

        Console.WriteLine($"SAS Token URL: {fullUri.Uri}");
    }
}
