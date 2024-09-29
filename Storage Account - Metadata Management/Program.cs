using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "your_connection_string_here";
        string containerName = "your_container_name";
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var metadata = new Dictionary<string, string>
        {
            { "owner", "admin" },
            { "environment", "production" }
        };

        await containerClient.SetMetadataAsync(metadata);
        Console.WriteLine("Metadata has been set successfully.");

 
        BlobContainerProperties properties = await containerClient.GetPropertiesAsync();
        Console.WriteLine("Container Metadata:");
        foreach (var kvp in properties.Metadata)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}


