using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

class Program
{
    private static string tenantId = "";    
    private static string clientId = "";    
    private static string clientSecret = "";  
    private static string[] scopes = { "https://graph.microsoft.com/.default" }; 

    static async Task Main(string[] args)
    {
              IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(clientId)
            .WithClientSecret(clientSecret)
            .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
            .Build();

        try
        {
          
            var result = await app.AcquireTokenForClient(scopes)
                                  .ExecuteAsync();

 
            Console.WriteLine("Access Token:");
            Console.WriteLine(result.AccessToken);
        }
        catch (MsalServiceException ex)
        {
            // Handle the exception
            Console.WriteLine($"Error acquiring token: {ex.Message}");
        }
    }
}

