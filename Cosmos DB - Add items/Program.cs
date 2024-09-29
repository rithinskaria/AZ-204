using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

class Program
{
    static async Task Main(string[] args)
    {

        string endpoint = "https://<your-cosmosdb-account>.documents.azure.com:443/";
        string key = "<your-cosmosdb-account-key>";
        string databaseName = "<your-database-name>";
        string collectionName = "<your-container-name>";

        // Initialize Cosmos Client
        CosmosClient client = new CosmosClient(endpoint, key);
    
        Container container = client.GetContainer(databaseName, collectionName);

        // Create a new record for "DXB" airport
        var airportRecord = new 
        {
            airport_code = "DXB",
            airport_name = "Dubai International Airport",
            country_code = "UAE"
        };

        try
        {

            var response = await container.CreateItemAsync(airportRecord);

            Console.WriteLine($"DXB has been successfully added.");

    
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
