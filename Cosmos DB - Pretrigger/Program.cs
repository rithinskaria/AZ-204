using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

class Program
{
    static async Task Main(string[] args)
    {
        string endpoint = "https://az204cosmosdb01.documents.azure.com:443/";
        string key = "CQVjPdqcMPKbByiLu4u664S6HOwyusm9JWvUYeyJhJcqTpMCTH0xuie5dNdVhLLobQruzAEyfgcJACDbsJCUEg==";
        string databaseName = "flightDetails";
        string collectionName = "airportCodes";
        CosmosClient client = new CosmosClient(endpoint, key);
        Container container = client.GetContainer(databaseName, collectionName);
        string preTrigger = "pre-trig-01";

        var airportRecord = new
        {
            id = Guid.NewGuid().ToString(),
            airport_code = "XYZ",
            airport_name = "XYZ Airport"
        };

        try
        {
           
            ItemRequestOptions requestOptions = new ItemRequestOptions
            {
                PreTriggers = new[] { preTrigger }  
            };

                       var response = await container.CreateItemAsync(airportRecord, null, requestOptions);

            // If CreateItemAsync was successful, display success message
            Console.WriteLine($"Successfully added ID: {response.Resource.id}");
        }
        catch (CosmosException ex)
        {
            Console.WriteLine($"Cosmos DB error: {ex.StatusCode} - {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Process completed.");
    }
}

