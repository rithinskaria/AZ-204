using System;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

class Program
{
    static void Main(string[] args)
    {

        string storageAccountName = "";
        string storageAccountKey = "";
        string containerName = "";


        StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);


        string blobServiceUrl = $"https://{storageAccountName}.blob.core.windows.net";
        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobServiceUrl), sharedKeyCredential);


        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);


        BlobSasBuilder sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerName,
            Resource = "c", 
            StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5), 
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1) 
        };


        sasBuilder.SetPermissions(BlobContainerSasPermissions.Read | BlobContainerSasPermissions.Write | BlobContainerSasPermissions.List);


        string sasToken = sasBuilder.ToSasQueryParameters(sharedKeyCredential).ToString();


        UriBuilder fullUri = new UriBuilder($"{blobServiceUrl}/{containerName}")
        {
            Query = sasToken
        };

        Console.WriteLine($"SAS Token URL for the container: {fullUri.Uri}");
    }
}
