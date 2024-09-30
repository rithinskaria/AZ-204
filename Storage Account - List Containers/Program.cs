using Azure.Identity;
using Azure.Storage.Blobs;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string blobServiceEndpoint = "https://<your-storage-account-name>.blob.core.windows.net";
        var credential = new DefaultAzureCredential();
        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(blobServiceEndpoint), credential);
        Console.WriteLine("Connected to Azure Blob Storage using DefaultAzureCredential.");

        await ListContainersAsync(blobServiceClient);
    }

    static async Task ListContainersAsync(BlobServiceClient blobServiceClient)
    {
        Console.WriteLine("Listing containers...");
        await foreach (var container in blobServiceClient.GetBlobContainersAsync())
        {
            Console.WriteLine($"Container: {container.Name}");
        }
    }
}
