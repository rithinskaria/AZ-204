# Azure Event Hubs Example

## Description
This C# console application demonstrates how to interact with Azure Event Hubs. It includes examples of inspecting an Event Hub, publishing events, and reading events from the Event Hub.

## Features
- Connects to an Azure Event Hub
- Inspects Event Hub properties (partition IDs)
- Publishes events to the Event Hub
- Reads events from the Event Hub

## Prerequisites
- .NET 6.0 SDK or later
- An Azure account with an active subscription
- An Azure Event Hubs namespace and an Event Hub

## Required Packages
- Azure.Messaging.EventHubs

To install the required package, run:
```
dotnet add package Azure.Messaging.EventHubs
```

## Configuration
Before running the application, you need to configure the following variables in the `Program` class:

- `connectionString`: Your Event Hubs namespace connection string
- `eventHubName`: The name of your Event Hub

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required package.
3. Configure the application as described in the Configuration section.
4. Run the application:
   ```
   dotnet run
   ```

## Code Explanation

The application performs three main operations:

### 1. Inspecting the Event Hub
```csharp
static async Task InspectEventHub()
{
    await using (var producer = new EventHubProducerClient(connectionString, eventHubName))
    {
        string[] partitionIds = await producer.GetPartitionIdsAsync();
        Console.WriteLine("Partition IDs: " + string.Join(", ", partitionIds));
    }
}
```
This method connects to the Event Hub and retrieves the partition IDs.

### 2. Publishing Events
```csharp
static async Task PublishEventsToEventHub()
{
    await using (var producer = new EventHubProducerClient(connectionString, eventHubName))
    {
        using EventDataBatch eventBatch = await producer.CreateBatchAsync();
        eventBatch.TryAdd(new EventData(new BinaryData("Hello, let's learn Azure Event Hubs!")));
        eventBatch.TryAdd(new EventData(new BinaryData("Let's learn to send events to Event Hubs!")));
        await producer.SendAsync(eventBatch);
    }
}
```
This method creates a batch of events and sends them to the Event Hub.

### 3. Reading Events
```csharp
static async Task ReadEventsFromEventHub()
{
    await using (var consumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName))
    {
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));
        await foreach (PartitionEvent receivedEvent in consumer.ReadEventsAsync(cancellationSource.Token))
        {
            Console.WriteLine("Event Received: " + receivedEvent.Data.EventBody.ToString());
        }
    }
}
```
This method reads events from the Event Hub for 45 seconds and prints them to the console.

## Error Handling
The application does not include explicit error handling. In a production environment, you should add try-catch blocks to handle potential exceptions, such as:
- `EventHubsException`: For Event Hubs-specific errors
- `TaskCanceledException`: If the read operation is cancelled
- General `Exception`: For any other unexpected errors

## Customization
- Modify the event data in the `PublishEventsToEventHub` method to send different types of events.
- Adjust the `CancelAfter` duration in the `ReadEventsFromEventHub` method to change how long the application listens for events.
- Implement custom event processing logic in the `ReadEventsFromEventHub` method.

## Performance Considerations
- For high-volume scenarios, consider using the `EventProcessorClient` class for more efficient event processing across multiple partitions.
- Use batching when sending events to improve throughput.

## Additional Resources
- [Azure Event Hubs documentation](https://docs.microsoft.com/en-us/azure/event-hubs/)
- [Event Hubs .NET SDK documentation](https://docs.microsoft.com/en-us/dotnet/api/overview/azure/event-hubs?view=azure-dotnet)
- [Event Hubs programming guide](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-programming-guide)
- [Scaling with Event Hubs](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-scalability)
