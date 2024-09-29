# Cosmos DB Stored Procedure: Insert Airport Data

## Description
This stored procedure is designed to insert airport data into an Azure Cosmos DB container. It includes logic to prevent duplicate entries based on the document's `id` field and ensures that the operation is performed within the same partition.

## Functionality
1. Checks if the input `airportData` has an `id` field.
2. Queries the container to check if a document with the same `id` already exists.
3. If no duplicate is found, inserts the new document.
4. Provides appropriate error messages for various scenarios.

## Usage

### Stored Procedure Code
```javascript
function insertAirportData(airportData) {
...............
}
```

### Calling the Stored Procedure
To call this stored procedure from your application, use the Cosmos DB SDK for your preferred language. Here's a general example:

```csharp
// C# example
dynamic airportData = new {
    id = "AirportCode",
    name = "Airport Name",
    // ... other airport properties ...
};

StoredProcedureExecuteResponse<string> result = await container.Scripts.ExecuteStoredProcedureAsync<string>(
    "insertAirportData",
    new PartitionKey(airportData.id),
    new dynamic[] { airportData }
);
```

## Important Considerations

1. **Partition Key**: This stored procedure assumes that the `id` field is used as the partition key. Ensure your container is set up accordingly.

2. **Error Handling**: The procedure includes error handling for various scenarios. Make sure to handle these errors appropriately in your application code.

3. **Performance**: This procedure performs a query before insertion. For high-volume scenarios, consider the performance implications and whether a direct upsert operation might be more suitable.

4. **Atomicity**: The operation is atomic within the scope of the stored procedure execution.

5. **Input Validation**: While the procedure checks for the presence of an `id`, you might want to add additional validation for other required fields.

## Customization

You can modify this stored procedure to fit your specific needs:

- Add additional fields to check for duplicates.
- Implement custom logic for handling existing documents (e.g., update instead of reject).
- Include additional data validation or transformation logic.

## Troubleshooting

If you encounter issues:

1. Check the Cosmos DB container's partition key setup.
2. Verify that the `id` field in your input data matches your partition key strategy.
3. Review the Cosmos DB collection's indexing policy to ensure efficient querying.
4. Monitor the RU (Request Unit) consumption of this operation, especially for large documents or high-frequency calls.

## Additional Resources

- [Azure Cosmos DB JavaScript SDK](https://docs.microsoft.com/en-us/azure/cosmos-db/sql-api-sdk-node)
- [Stored Procedures in Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/sql-api-stored-procedures)
- [Partitioning in Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/partitioning-overview)
