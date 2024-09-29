using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Identity;
using System;

public static class QueuePost
{
    private const string queueName = "";
    private const string storageAccount = "";

    [FunctionName("QueuePost")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Processing a POST request to write message to Azure Queue.....");

        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            if (data == null)
            {
                return new BadRequestObjectResult("Invalid payload");
            }

            var credential = new DefaultAzureCredential();
            var queueClient = new QueueClient(new Uri($"https://{storageAccount}.queue.core.windows.net/{queueName}"), credential);
            await queueClient.CreateIfNotExistsAsync();

            if (queueClient.Exists())
            {
                await queueClient.SendMessageAsync(requestBody);
                log.LogInformation($"Message added to the queue: {requestBody}");
            }
            else
            {
                log.LogError("Queue does not exist.");
                return new BadRequestObjectResult("Queue does not exist.");
            }

            return new OkObjectResult($"Message added to queue: {queueName}");
        }
        catch (Exception ex)
        {
            log.LogError($"Error occurred while writing to the queue: {ex.Message}");
            return new StatusCodeResult(500); 
        }
    }
}

