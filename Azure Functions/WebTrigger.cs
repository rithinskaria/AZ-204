using System;
using System.IO;
using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public static class WebToTableFunction
{
    [FunctionName("WebToTableFunction")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Processing HTTP request to write to Azure Table Storage.");

        string requestBody = new StreamReader(req.Body).ReadToEnd();
        dynamic data = JsonConvert.DeserializeObject(requestBody);

        string partitionKey = data?.partitionKey ?? "defaultPartition";
        string rowKey = data?.rowKey ?? Guid.NewGuid().ToString();

        var tableClient = new TableClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "UserDetails");

        var entity = new TableEntity(partitionKey, rowKey)
        {
            { "Name", data?.name },
            { "Email", data?.email }
        };

        tableClient.AddEntity(entity);

        return new OkObjectResult("Data written to table storage");
    }
}
