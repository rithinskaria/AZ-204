using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

class Program
{
    private const string connectionString = ""; 
    private const string queueName = "";  

    static async Task Main(string[] args)
    {
        Console.WriteLine("Azure Service Bus Demo: Send or Receive Messages");
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Send a message");
        Console.WriteLine("2. Receive messages");

        var input = Console.ReadLine();

        if (input == "1")
        {
            await SendMessageAsync();
        }
        else if (input == "2")
        {
            await ReceiveMessagesAsync();
        }
        else
        {
            Console.WriteLine("Invalid input. Exiting...");
        }
    }

    static async Task SendMessageAsync()
    {
        await using var client = new ServiceBusClient(connectionString);
        ServiceBusSender sender = client.CreateSender(queueName);

        Console.WriteLine("Enter the message to send:");
        string? messageBody = Console.ReadLine();
        ServiceBusMessage message = new ServiceBusMessage(messageBody);


        await sender.SendMessageAsync(message);
        Console.WriteLine($"Sent message: {messageBody}");
    }

    static async Task ReceiveMessagesAsync()
    {
        await using var client = new ServiceBusClient(connectionString);
        ServiceBusProcessor processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();

        Console.WriteLine("Receiving messages... Press any key to stop.");
        Console.ReadKey();
        await processor.StopProcessingAsync();
        Console.WriteLine("Stopped receiving messages.");
    }

    static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();
        Console.WriteLine($"Received message: {body}");
        await args.CompleteMessageAsync(args.Message);
    }


    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Error: {args.Exception.Message}");
        return Task.CompletedTask;
    }
}
