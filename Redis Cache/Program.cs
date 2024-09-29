using System;
using System.Threading.Tasks;
using StackExchange.Redis;

class Program
{
    static async Task Main(string[] args)
    {
        // Connection string to Azure Redis Cache
        string redisConnectionString = "";
        
        ConnectionMultiplexer redisConnection = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        IDatabase db = redisConnection.GetDatabase();

        Console.WriteLine("Welcome to Redis Console App!");
        string key = Guid.NewGuid().ToString();
        Console.WriteLine("Enter the value to store in Redis:");
        string? value = Console.ReadLine();
        bool wasSet = await db.StringSetAsync(key, value, TimeSpan.FromSeconds(30));


        if (wasSet)
        {
            Console.WriteLine($"Value '{value}' has been stored with key: {key} and TTL of 30 seconds.");
        }
        else
        {
            Console.WriteLine("Failed to store the value.");
        }


        string? retrievedValue = await db.StringGetAsync(key);
        Console.WriteLine($"Retrieved value: {retrievedValue}");
        Console.WriteLine("Waiting for 30 seconds to let the key expire...");
        await Task.Delay(30000);

        retrievedValue = await db.StringGetAsync(key);
        if (retrievedValue == null)
        {
            Console.WriteLine("The key has expired.");
        }
        else
        {
            Console.WriteLine($"The key is still available with value: {retrievedValue}");
        }
    }
}
