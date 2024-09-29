# Azure Cosmos DB Document Creation Example

## Description
This C# console application demonstrates how to create a new document in an Azure Cosmos DB container. It creates a new airport record for Dubai International Airport (DXB) and inserts it into a specified Cosmos DB container.

## Features
- Connects to an Azure Cosmos DB account
- Creates a new document in a specified container
- Demonstrates error handling for Cosmos DB operations
- Shows an alternative method for upserting documents

## Prerequisites
- .NET 6.0 SDK or later
- An Azure account with an active subscription
- An Azure Cosmos DB account with a database and container

## Required Packages
- Microsoft.Azure.Cosmos

To install the required package, run:
```
dotnet add package Microsoft.Azure.Cosmos
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

2. Defining the document to insert:
   ```csharp
   var airportRecord = new 
   {
       airport_code = "DXB",
       airport_name = "Dubai International Airport",
       country_code = "UAE"
   };
   ```

3. Inserting the document:
   ```csharp
   var response = await container.CreateItemAsync(airportRecord);
   ```

4. Alternative method for upserting (create or replace) a document:
   ```csharp
   // var response = await container.UpsertItemAsync(airportRecord);
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
- You can modify the `airportRecord` object to include different fields or data for other airports or entities.
- Uncomment and use the `UpsertItemAsync` method if you want to create or replace documents based on their ID.

## Troubleshooting
- If you encounter authentication errors, double-check your Cosmos DB endpoint and key.
- Ensure that the database name and container name are correct.
- Verify that your Azure subscription is active and that you have the necessary permissions to access the Cosmos DB account.
- If you're getting conflict errors, it might mean a document with the same ID already exists. In this case, consider using `UpsertItemAsync` instead of `CreateItemAsync`.

## Additional Resources
- [Azure Cosmos DB documentation](https://docs.microsoft.com/en-us/azure/cosmos-db/)
- [Cosmos DB .NET SDK documentation](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-sdk-dotnet-standard)
- [Create and manage documents in Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/how-to-crud-documents)
