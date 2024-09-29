using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;

class Program
{
    private const string connectionString = "";
    private const string eventHubName = "";
    private const string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

    static async Task Main(string[] args)
    {
        await InspectEventHub();
        await PublishEventsToEventHub();
        await ReadEventsFromEventHub();
    }

    static async Task InspectEventHub()
    {
        Console.WriteLine("Inspecting Event Hub...");

        await using (var producer = new EventHubProducerClient(connectionString, eventHubName))
        {
            string[] partitionIds = await producer.GetPartitionIdsAsync();
            Console.WriteLine("Partition IDs: " + string.Join(", ", partitionIds));
        }
    }

    static async Task PublishEventsToEventHub()
    {
        Console.WriteLine("Publishing Events to Event Hub...");

        await using (var producer = new EventHubProducerClient(connectionString, eventHubName))
        {
            using EventDataBatch eventBatch = await producer.CreateBatchAsync();
            eventBatch.TryAdd(new EventData(new BinaryData("Hello, let's learn Azure Event Hubs!")));
            eventBatch.TryAdd(new EventData(new BinaryData("Let's learn to send events to Event Hubs!")));

            await producer.SendAsync(eventBatch);
            Console.WriteLine("Events sent.");
        }
    }

    static async Task ReadEventsFromEventHub()
    {
        Console.WriteLine("Reading Events from Event Hub...");

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
}
