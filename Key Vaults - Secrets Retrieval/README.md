# Azure Function: Key Vault Secret Retrieval

## Description
This Azure Function demonstrates how to securely retrieve a secret from Azure Key Vault. It uses Managed Identity for authentication, providing a secure way to access Key Vault without storing credentials in your code or configuration.

## Features
- HTTP-triggered Azure Function
- Retrieves a secret from Azure Key Vault
- Uses Managed Identity for authentication
- Demonstrates error handling and logging

## Prerequisites
- Azure subscription
- Azure Function App with Managed Identity enabled
- Azure Key Vault
- .NET 6.0 SDK or later

## Required Packages
- Microsoft.NET.Sdk.Functions
- Azure.Identity
- Azure.Security.KeyVault.Secrets

To install the required packages, run:
```
dotnet add package Microsoft.NET.Sdk.Functions
dotnet add package Azure.Identity
dotnet add package Azure.Security.KeyVault.Secrets
```

## Configuration
The function requires the following environment variables to be set in the Azure Function App settings:

- `AZURE_TENANT_ID`: The Azure AD tenant ID
- `KEY_VAULT_URL`: The URL of your Azure Key Vault
- `SECRET_NAME`: The name of the secret you want to retrieve

## How to Use
1. Clone the repository or copy the code into a new Azure Function project.
2. Install the required packages.
3. Configure the environment variables in your Azure Function App settings.
4. Deploy the function to Azure.
5. Make a GET or POST request to the function URL.

## Code Explanation

### Function Trigger
```csharp
[FunctionName("GetSecretFromKeyVault")]
public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
    ILogger log)
```
This function can be triggered by both GET and POST HTTP requests. It requires function-level authorization.

### Environment Variable Retrieval and Validation
```csharp
string tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
string keyVaultUrl = Environment.GetEnvironmentVariable("KEY_VAULT_URL");
string secretName = Environment.GetEnvironmentVariable("SECRET_NAME");

if (string.IsNullOrEmpty(tenantId) ||  string.IsNullOrEmpty(keyVaultUrl) ||
    string.IsNullOrEmpty(secretName))
{
    return new BadRequestObjectResult("Missing one or more environment variables: AZURE_TENANT_ID, KEY_VAULT_URL, SECRET_NAME");
}
```
This section retrieves and validates the necessary environment variables.

### Key Vault Secret Retrieval
```csharp
var credential = new DefaultAzureCredential();
var secretClient = new SecretClient(new Uri(keyVaultUrl), credential);  
KeyVaultSecret secret = await secretClient.GetSecretAsync(secretName);
```
This code uses the DefaultAzureCredential to authenticate with Azure Key Vault and retrieve the specified secret.

## Error Handling
The function includes a try-catch block to handle exceptions:
```csharp
catch (Exception ex)
{
    log.LogError($"Error retrieving secret: {ex.Message}");
    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
}
```

## Security Considerations
- Ensure that the Function App's Managed Identity has the necessary permissions to access the Key Vault and read the secret.
- Never log or display the actual secret value in production environments.
- Use HTTPS for all communications with the Function App.

## Customization
- Modify the function to handle multiple secrets or different Key Vault operations.
- Implement caching to reduce the number of Key Vault requests for frequently accessed secrets.

## Troubleshooting
- If you encounter authentication errors, ensure that:
  - Managed Identity is enabled for your Function App.
  - The Function App's Managed Identity has the correct permissions in Key Vault.
- Verify that all environment variables are correctly set in the Function App settings.
- Check the function logs for detailed error messages.

## Additional Resources
- [Azure Functions documentation](https://docs.microsoft.com/en-us/azure/azure-functions/)
- [Azure Key Vault documentation](https://docs.microsoft.com/en-us/azure/key-vault/)
- [DefaultAzureCredential documentation](https://docs.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential)
- [Managed Identities for Azure resources](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview)
