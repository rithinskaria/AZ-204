using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace FunctionAppKeyVault
{
    public static class GetSecretFromKeyVault
    {
        [FunctionName("GetSecretFromKeyVault")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
            string keyVaultUrl = Environment.GetEnvironmentVariable("KEY_VAULT_URL");
            string secretName = Environment.GetEnvironmentVariable("SECRET_NAME");


            if (string.IsNullOrEmpty(tenantId) ||  string.IsNullOrEmpty(keyVaultUrl) ||
                string.IsNullOrEmpty(secretName))
            {
                return new BadRequestObjectResult("Missing one or more environment variables: AZURE_TENANT_ID, KEY_VAULT_URL, SECRET_NAME");
            }

            try
            {
                var credential = new DefaultAzureCredential();
                var secretClient = new SecretClient(new Uri(keyVaultUrl), credential);  

                KeyVaultSecret secret = await secretClient.GetSecretAsync(secretName);
                return new OkObjectResult(secret.Value);
            }
            catch (Exception ex)
            {
                log.LogError($"Error retrieving secret: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
