using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    private static string clientId = ""; 
    private static string tenantId = ""; 
    private static string[] scopes = new[] { "User.Read" }; 

    static async Task Main(string[] args){

        var publicClientApp = PublicClientApplicationBuilder
            .Create(clientId)
            .WithAuthority($"https://login.microsoftonline.com/{tenantId}")
            .WithRedirectUri("http://localhost")
            .Build();

        var authResult = await publicClientApp
            .AcquireTokenWithDeviceCode(scopes, deviceCodeResult =>
            {
                Console.WriteLine(deviceCodeResult.Message);
                return Task.FromResult(0);
            })
            .ExecuteAsync();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

        var graphClient = new GraphServiceClient(httpClient);
        var user = await graphClient.Me.GetAsync();

        Console.WriteLine($"Display Name: {user?.DisplayName}");
        Console.WriteLine($"User Principal Name: {user?.UserPrincipalName}");
        Console.WriteLine($"Job Title: {user?.JobTitle ?? "N/A"}");
        Console.WriteLine($"Mobile Phone: {user?.MobilePhone ?? "N/A"}");
    }
}
