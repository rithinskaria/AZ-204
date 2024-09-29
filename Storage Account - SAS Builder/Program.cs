using System;
using Azure.Storage;
using Azure.Storage.Sas;

class Program
{
    static void Main(string[] args)
    {
  
        string storageAccountName = "";
        string storageAccountKey = "";
        StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);

   
        AccountSasBuilder sasBuilder = new AccountSasBuilder
        {
            Services = AccountSasServices.Blobs | AccountSasServices.Files,  
            ResourceTypes = AccountSasResourceTypes.Service | AccountSasResourceTypes.Container | AccountSasResourceTypes.Object, 
            Protocol = SasProtocol.Https, 
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1) 
        };


        sasBuilder.SetPermissions(AccountSasPermissions.Read | AccountSasPermissions.Write | AccountSasPermissions.List);


        string sasToken = sasBuilder.ToSasQueryParameters(sharedKeyCredential).ToString();

        string blobServiceUrl = $"https://{storageAccountName}.blob.core.windows.net/";
        UriBuilder fullUri = new UriBuilder(blobServiceUrl)
        {
            Query = sasToken
        };

        Console.WriteLine($"SAS Token URL: {fullUri.Uri}");
    }
}
