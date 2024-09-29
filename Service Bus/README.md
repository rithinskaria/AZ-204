# Azure Service Bus Messaging Application

## Description
This C# console application demonstrates how to interact with Azure Service Bus queues. It provides functionality to both send messages to a queue and receive messages from a queue.

## Features
- Send messages to an Azure Service Bus queue
- Receive and process messages from an Azure Service Bus queue
- Interactive console interface for choosing operations
- Asynchronous message handling
- Error handling for message processing

## Prerequisites
- .NET 6.0 SDK or later
- An Azure account with an active subscription
- An Azure Service Bus namespace and queue

## Required Packages
- Azure.Messaging.ServiceBus

To install the required package, run:
```
dotnet add package Azure.Messaging.ServiceBus
```

## Configuration
Before running the application, you need to configure the following constants in the `Program` class:

- `connectionString`: Your Azure Service Bus connection string
- `queueName`: The name of your Azure Service Bus queue

You can find the connection string in the Azure portal under your Service Bus namespace.

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required package.
3. Configure the application as described in the Configuration section.
4. Run the application:
   ```
   dotnet run
   ```
5. Choose an option:
   - Enter '1' to send a message
   - Enter '2' to receive messages

## Code Explanation

### Main Method
```csharp
static async Task Main(string[] args)
{
    // ... (user input handling)
    if (input == "1")
    {
        await SendMessageAsync();
    }
    else if (input == "2")
    {
        await ReceiveMessagesAsync();
    }
    // ...
}
```
This method provides a simple console interface for choosing between sending and receiving messages.

### Sending Messages
```csharp
static async Task SendMessageAsync()
{
    await using var client = new ServiceBusClient(connectionString);
    ServiceBusSender sender = client.CreateSender(queueName);
    // ... (get message from user input)
    await sender.SendMessageAsync(message);
}
```
This method creates a `ServiceBusClient`, a sender for the specified queue, and sends a user-input message.

### Receiving Messages
```csharp
static async Task ReceiveMessagesAsync()
{
    await using var client = new ServiceBusClient(connectionString);
    ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;
    await processor.StartProcessingAsync();
    // ... (wait for user input to stop)
    await processor.StopProcessingAsync();
}
```
This method sets up a `ServiceBusProcessor` to continuously process messages from the queue until the user stops it.

### Message Handler
```csharp
static async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Received message: {body}");
    await args.CompleteMessageAsync(args.Message);
}
```
This handler processes each received message, printing its contents and marking it as complete.

## Error Handling
The application includes an error handler for the message processor:
```csharp
static Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine($"Error: {args.Exception.Message}");
    return Task.CompletedTask;
}
```

## Security Considerations
- Keep your Service Bus connection string secure. Never commit it to source control.
- Consider using Managed Identities for Azure resources in production environments.
- Implement proper access controls on your Service Bus namespace and queues.

## Customization
- Modify the message handling logic to process messages according to your specific needs.
- Implement message batching for sending multiple messages at once.
- Add support for topics and subscriptions in addition to queues.

## Troubleshooting
- If you encounter connection errors, double-check your Service Bus connection string.
- Ensure that the queue name is correct and that the queue exists in your Service Bus namespace.
- Verify that your Azure subscription is active and that you have the necessary permissions.
- Check the Azure portal for any Service Bus-specific issues or quota limitations.

## Additional Resources
- [Azure Service Bus documentation](https://docs.microsoft.com/en-us/azure/service-bus-messaging/)
- [Azure.Messaging.ServiceBus SDK documentation](https://docs.microsoft.com/en-us/dotnet/api/azure.messaging.servicebus)
- [Best practices for performance improvements using Service Bus Messaging](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-performance-improvements)
- [Service Bus Explorer](https://github.com/paolosalvatori/ServiceBusExplorer) - A useful tool for managing and testing Service Bus resources
