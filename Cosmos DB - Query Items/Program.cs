using System;
using System.Threading.Tasks;
using Azure.Cosmos;

class Program
{
    static async Task Main(string[] args)
    {
        string endpoint = "https://<your-cosmosdb-account>.documents.azure.com:443/";
        string key = "<your-cosmosdb-account-key>";
        string databaseName = "<your-database-name>";
        string collectionName = "<your-container-name>";

        CosmosClient client = new CosmosClient(endpoint, key);
        
        Container container = client.GetContainer(databaseName, collectionName);

        var query = "SELECT * FROM c WHERE c.country_code = 'United States'";
        
        try
        {
            FeedIterator<dynamic> iterator = container.GetItemQueryIterator<dynamic>(query);

            while (iterator.HasMoreResults)
            {
                FeedResponse<dynamic> response = await iterator.ReadNextAsync();
                foreach (var airportItem in response)
                {
                    Console.WriteLine($"Airport Code: {airportItem.airport_code}, Name: {airportItem.airport_name}, Country: {airportItem.country_code}");
                }
            }
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

