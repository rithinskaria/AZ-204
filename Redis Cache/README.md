# Azure Redis Cache Console Application

## Description
This C# console application demonstrates how to interact with Azure Redis Cache. It performs basic operations such as storing a value with a time-to-live (TTL), retrieving the value, and demonstrating key expiration.

## Features
- Connects to Azure Redis Cache
- Stores a user-input value with a 30-second TTL
- Retrieves the stored value
- Demonstrates key expiration after 30 seconds

## Prerequisites
- An Azure account with an active subscription
- An Azure Redis Cache instance
- .NET 6.0 SDK or later

## Required Packages
- StackExchange.Redis

To install the required package, run:
```
dotnet add package StackExchange.Redis
```

## Configuration
Before running the application, you need to configure the following:

1. In the `Main` method, replace the empty `redisConnectionString` with your actual Azure Redis Cache connection string:
   ```csharp
   string redisConnectionString = "your-redis-connection-string";
   ```

## How to Use
1. Clone the repository or copy the code into a new C# console application.
2. Install the required package.
3. Configure the application as described in the Configuration section.
4. Run the application:
   ```
   dotnet run
   ```
5. Follow the prompts in the console to enter a value to store in Redis.

## Code Explanation

### Connecting to Redis
The application uses `ConnectionMultiplexer` to establish a connection to Azure Redis Cache:

```csharp
ConnectionMultiplexer redisConnection = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
IDatabase db = redisConnection.GetDatabase();
```

### Storing a Value
A new GUID is generated as the key, and the user is prompted to enter a value. The value is then stored with a 30-second TTL:

```csharp
string key = Guid.NewGuid().ToString();
Console.WriteLine("Enter the value to store in Redis:");
string? value = Console.ReadLine();
bool wasSet = await db.StringSetAsync(key, value, TimeSpan.FromSeconds(30));
```

### Retrieving the Value
The stored value is immediately retrieved and displayed:

```csharp
string? retrievedValue = await db.StringGetAsync(key);
Console.WriteLine($"Retrieved value: {retrievedValue}");
```

### Demonstrating Key Expiration
The application waits for 30 seconds and then attempts to retrieve the value again to demonstrate key expiration:

```csharp
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
```

## Sample Output
```
Welcome to Redis Console App!
Enter the value to store in Redis:
Hello, Redis!
Value 'Hello, Redis!' has been stored with key: 12345678-1234-1234-1234-123456789abc and TTL of 30 seconds.
Retrieved value: Hello, Redis!
Waiting for 30 seconds to let the key expire...
The key has expired.
```

## Additional Notes
- This application demonstrates basic Redis operations. Redis offers many more advanced features and data structures.
- In a production environment, consider implementing proper error handling, connection management, and logging.
- The connection to Redis is not explicitly closed in this example. In a real-world application, you should properly dispose of the `ConnectionMultiplexer` when it's no longer needed.

## Troubleshooting
- If you encounter connection errors, verify that your Redis connection string is correct and that your Azure Redis Cache instance is running.
- Ensure that your client IP address is allowed in the Azure Redis Cache firewall settings.
- If the key doesn't expire as expected, check that the Redis server time is correctly synchronized.
