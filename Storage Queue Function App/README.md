# Azure Function: Queue Post

## Description
This Azure Function receives HTTP POST requests and writes the message body to an Azure Storage Queue. It uses Managed Identity for authentication, providing a secure way to access Azure resources without storing credentials in your code or configuration.

## Features
- HTTP-triggered Azure Function (POST method)
- Writes messages to Azure Storage Queue
- Uses Managed Identity for authentication
- Creates the queue if it doesn't exist
- Implements error handling and logging

## Prerequisites
- Azure subscription
- Azure Function App with Managed Identity enabled
- Azure Storage account
- .NET 6.0 SDK or later

## Required Packages
- Microsoft.NET.Sdk.Functions
- Azure.Storage.Queues
- Azure.Identity
- Newtonsoft.Json

To install the required packages, run:
```
dotnet add package Microsoft.NET.Sdk.Functions
dotnet add package Azure.Storage.Queues
dotnet add package Azure.Identity
dotnet add package Newtonsoft.Json
```

## Configuration
The function requires the following constants to be set in the `QueuePost` class:

- `queueName`: The name of the Azure Storage Queue
- `storageAccount`: The name of your Azure Storage account

## How to Use
1. Clone the repository or copy the code into a new Azure Function project.
2. Install the required packages.
3. Configure the `queueName` and `storageAccount` constants.
4. Deploy the function to Azure.
5. Make a POST request to the function URL with a JSON payload in the request body.

## Code Explanation

### Function Trigger
```csharp
[FunctionName("QueuePost")]
public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
    ILogger log)
```
This function is triggered by HTTP POST requests and requires function-level authorization.

### Reading the Request Body
```csharp
string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
dynamic data = JsonConvert.DeserializeObject(requestBody);
```
The function reads the request body and deserializes it from JSON.

### Queue Client Initialization
```csharp
var credential = new DefaultAzureCredential();
var queueClient = new QueueClient(new Uri($"https://{storageAccount}.queue.core.windows.net/{queueName}"), credential);
```
This code uses the DefaultAzureCredential to authenticate and create a QueueClient.

### Sending Message to Queue
```csharp
await queueClient.CreateIfNotExistsAsync();
if (queueClient.Exists())
{
    await queueClient.SendMessageAsync(requestBody);
    log.LogInformation($"Message added to the queue: {requestBody}");
}
```
The function creates the queue if it doesn't exist, then sends the message.

## Error Handling
The function includes a try-catch block to handle exceptions:
```csharp
catch (Exception ex)
{
    log.LogError($"Error occurred while writing to the queue: {ex.Message}");
    return new StatusCodeResult(500);
}
```

## Security Considerations
- Ensure that the Function App's Managed Identity has the necessary permissions to access the Azure Storage account and manage queues.
- Use HTTPS for all communications with the Function App.
- Implement appropriate authorization checks in your function to control who can post messages.

## Customization
- Modify the function to handle different types of payloads or implement custom message formatting before queueing.
- Implement message validation logic before adding to the queue.
- Add support for different queue names based on request parameters or payload content.

## Troubleshooting
- If you encounter authentication errors, ensure that:
  - Managed Identity is enabled for your Function App.
  - The Function App's Managed Identity has the correct permissions on the Azure Storage account.
- Verify that the storage account name and queue name are correct.
- Check the function logs for detailed error messages.
- Ensure that the POST request contains a valid JSON payload.

## Additional Resources
- [Azure Functions documentation](https://docs.microsoft.com/en-us/azure/azure-functions/)
- [Azure Storage Queues documentation](https://docs.microsoft.com/en-us/azure/storage/queues/)
- [DefaultAzureCredential documentation](https://docs.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential)
- [Managed Identities for Azure resources](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview)
