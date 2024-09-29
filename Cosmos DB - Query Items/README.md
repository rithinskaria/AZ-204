# Azure Cosmos DB Query Example

## Description
This C# console application demonstrates how to query documents from an Azure Cosmos DB container. It retrieves all airport records for the United States and displays their details.

## Features
- Connects to an Azure Cosmos DB account
- Executes a SQL query on a Cosmos DB container
- Iterates through query results
- Demonstrates error handling for Cosmos DB operations

## Prerequisites
- .NET 6.0 SDK or later
- An Azure account with an active subscription
- An Azure Cosmos DB account with a database and container containing airport data

## Required Packages
- Azure.Cosmos

To install the required package, run:
```
dotnet add package Azure.Cosmos
```

## Configuration
Before running the application, you need to configure the following variables in the `Main` method:

- `endpoint`: Your Cosmos DB account endpoint URL
- `key`: Your Cosmos DB account primary key
- `databaseName`: The name of your Cosmos DB database
- `collectionName`: The name of your Cosmos DB container

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required package.
3. Configure the application as described in the Configuration section.
4. Run the application:
   ```
   dotnet run
   ```

## Code Explanation

1. Connecting to Cosmos DB:
   ```csharp
   CosmosClient client = new CosmosClient(endpoint, key);
   Container container = client.GetContainer(databaseName, collectionName);
   ```

2. Defining the SQL query:
   ```csharp
   var query = "SELECT * FROM c WHERE c.country_code = 'United States'";
   ```

3. Executing the query and iterating through results:
   ```csharp
   FeedIterator<dynamic> iterator = container.GetItemQueryIterator<dynamic>(query);
   while (iterator.HasMoreResults)
   {
       FeedResponse<dynamic> response = await iterator.ReadNextAsync();
       foreach (var airportItem in response)
       {
           Console.WriteLine($"Airport Code: {airportItem.airport_code}, Name: {airportItem.airport_name}, Country: {airportItem.country_code}");
       }
   }
   ```

## Error Handling
The application includes error handling for Cosmos DB-specific exceptions and general exceptions:

```csharp
catch (CosmosException ex)
{
    Console.WriteLine($"Cosmos DB error: {ex.StatusCode} - {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Customization
- You can modify the SQL query to filter for different countries or add more complex conditions.
- Change the fields being accessed in the `Console.WriteLine` statement to display different information from your documents.

## Troubleshooting
- If you encounter authentication errors, double-check your Cosmos DB endpoint and key.
- Ensure that the database name and container name are correct.
- Verify that your Azure subscription is active and that you have the necessary permissions to access the Cosmos DB account.
- If no results are returned, check that your container has documents matching the query criteria.

## Performance Considerations
- For large result sets, consider implementing paging or limiting the number of results returned.
- Use the appropriate consistency level for your needs to balance between performance and data freshness.

## Additional Resources
- [Azure Cosmos DB documentation](https://docs.microsoft.com/en-us/azure/cosmos-db/)
- [Cosmos DB .NET SDK documentation](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-sdk-dotnet-standard)
- [Query Azure Cosmos DB data with SQL queries](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-query-getting-started)
- [Optimize query performance in Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/how-to-optimize-query-performance)
