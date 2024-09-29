# Azure Cosmos DB Pre-Trigger Example

## Description
This C# console application demonstrates how to use a pre-trigger when inserting a document into an Azure Cosmos DB container. It creates a new airport record and applies a pre-trigger during the insertion process.

## Features
- Connects to an Azure Cosmos DB account
- Creates a new document with a pre-trigger
- Demonstrates error handling for Cosmos DB operations

## Prerequisites
- .NET 6.0 SDK or later
- An Azure account with an active subscription
- An Azure Cosmos DB account
- A pre-trigger named "pre-trig-01" created in your Cosmos DB container (code provided below)

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
4. Create a pre-trigger named "pre-trig-01" in your Cosmos DB container using the provided JavaScript code.
5. Run the application:
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
       id = Guid.NewGuid().ToString(),
       airport_code = "XYZ",
       airport_name = "XYZ Airport"
   };
   ```

3. Setting up the pre-trigger:
   ```csharp
   ItemRequestOptions requestOptions = new ItemRequestOptions
   {
       PreTriggers = new[] { preTrigger }  
   };
   ```

4. Inserting the document with the pre-trigger:
   ```csharp
   var response = await container.CreateItemAsync(airportRecord, null, requestOptions);

## Pre-Trigger
The pre-trigger "pre-trig-01" should be defined in your Cosmos DB container. Here's the JavaScript code for the pre-trigger:

```javascript
function preTrigger() {
    var item = getContext().getRequest().getBody();
    if (!item.country_code) {
        item.country_code = "unidentified";
    }
    item.timestamp = new Date().toISOString();
    getContext().getRequest().setBody(item);
}
```

This pre-trigger does the following:
1. Retrieves the item being inserted.
2. If the `country_code` field is not present, it sets it to "unidentified".
3. Adds a `timestamp` field with the current date and time.
4. Sets the modified item back as the request body.

To create this pre-trigger in your Cosmos DB container:
1. Open the Azure Portal and navigate to your Cosmos DB account.
2. Select your database and container.
3. Go to "Scale & Settings" for your container.
4. In the "Pre-Triggers" section, click "New Pre-Trigger".
5. Name it "pre-trig-01" and paste the above JavaScript code.
6. Save the pre-trigger.

When the C# application runs, it will use this pre-trigger, which will add the `country_code` (if missing) and `timestamp` fields to your document before insertion.

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
- You can modify the `airportRecord` object in the C# code to include different fields or data.
- Change the `preTrigger` variable to use a different pre-trigger if you have multiple defined in your container.
- Modify the pre-trigger JavaScript code to implement different logic or add/modify other fields as needed.

## Troubleshooting
- If you encounter authentication errors, double-check your Cosmos DB endpoint and key.
- Ensure that the database name, container name, and pre-trigger name are correct.
- Verify that your Azure subscription is active and that you have the necessary permissions to access the Cosmos DB account.
- If the pre-trigger doesn't seem to be executing, check its definition in the Azure portal and ensure it's named "pre-trig-01".
- Use the Azure Portal's Data Explorer to manually insert a document and test if the pre-trigger is working as expected.

## Additional Resources
- [Azure Cosmos DB documentation](https://docs.microsoft.com/en-us/azure/cosmos-db/)
- [Pre-trigger in Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/how-to-write-stored-procedures-triggers-udfs#pre-triggers)
- [Cosmos DB .NET SDK documentation](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-sdk-dotnet-standard)
- [Working with Azure Cosmos DB triggers](https://docs.microsoft.com/en-us/azure/cosmos-db/sql/how-to-use-stored-procedures-triggers-udfs#triggers)
