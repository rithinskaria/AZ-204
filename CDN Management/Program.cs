using System;
using Microsoft.Azure.Management.Cdn;
using Microsoft.Azure.Management.Cdn.Models;
using Azure.Identity;
using Microsoft.Rest;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {

        var credential = new DefaultAzureCredential();
        var tokenRequestContext = new Azure.Core.TokenRequestContext(new[] { "https://management.azure.com/.default" });
        var token = await credential.GetTokenAsync(tokenRequestContext);
        var serviceClientCredentials = new TokenCredentials(token.Token);


        var cdnClient = new CdnManagementClient(serviceClientCredentials)
        {
            SubscriptionId = "<sub-id>"  
        };

        ListProfilesAndEndpoints(cdnClient, "<rg>"); 
    }

    static void ListProfilesAndEndpoints(CdnManagementClient cdnClient, string resourceGroupName)
    {
        var profileList = cdnClient.Profiles.ListByResourceGroup(resourceGroupName);
        foreach (Profile p in profileList)
        {
            Console.WriteLine($"CDN profile: {p.Name}");

            Console.WriteLine("Endpoints:");
            var endpointList = cdnClient.Endpoints.ListByProfile(resourceGroupName, p.Name);
            foreach (Endpoint e in endpointList)
            {
                Console.WriteLine($"   - {e.Name} ({e.HostName})");
            }
            Console.WriteLine();
        }
    }
}
