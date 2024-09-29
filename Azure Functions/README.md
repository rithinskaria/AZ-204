# Develop Azure Functions

## WebToTableFunction - Azure Function

This project is an Azure Function that reads data from an HTTP POST request and writes it to Azure Table Storage. It demonstrates how to handle HTTP requests and interact with Azure Table Storage.

## Steps to Generate This Project

This project was generated using the Azure Functions Core Tools (`func`). Below are the steps to create a similar project:

1. Install the Azure Functions Core Tools:

    ```bash
    npm install -g azure-functions-core-tools@4 --unsafe-perm true
    ```

2. Create a new Function App in the desired directory:

    ```bash
    func init WebToTableFunctionApp --dotnet
    ```

3. Create a new HTTP trigger function:

    ```bash
    func new --name WebToTableFunction --template "HttpTrigger" --authlevel "Function"
    ```

## Configuration Parameters/Variables

Ensure the following configurations are set up for the Azure Function to work:

- **AzureWebJobsStorage**: The connection string for the Azure Storage Account. This is retrieved from the environment variables.
  
  You can set it in your local environment by adding it to the `local.settings.json`:
  
    ```json
    {
      "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "YourAzureStorageConnectionString",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
      }
    }
    ```

## Usage

1. Deploy the function to Azure using Azure Functions Core Tools:

    ```bash
    func azure functionapp publish <your-function-app-name>
    ```

2. Send a POST request to the function's endpoint with a JSON payload. The function will extract `partitionKey`, `rowKey`, `name`, and `email` from the request body. If no `partitionKey` is provided, it will default to `defaultPartition`, and `rowKey` will be auto-generated.

   Example payload:

    ```json
    {
      "partitionKey": "userPartition",
      "rowKey": "userRow",
      "name": "John Doe",
      "email": "johndoe@example.com"
    }
    ```

3. The function will insert the received data into the `UserDetails` table in Azure Table Storage.

## Required Packages

- **Azure.Data.Tables**: For interacting with Azure Table Storage.
  
  Install the package:

    ```bash
    dotnet add package Azure.Data.Tables
    ```

- **Microsoft.Azure.WebJobs.Extensions.Http**: For handling HTTP requests. This package is part of the Azure Functions runtime.
  
- **Newtonsoft.Json**: For deserializing JSON request bodies.
  
  Install the package:

    ```bash
    dotnet add package Newtonsoft.Json
    ```

## Code Explanation

- The `WebToTableFunction` is triggered by an HTTP POST request.
- The function reads the request body and deserializes it using `JsonConvert`.
- If `partitionKey` and `rowKey` are provided, they are used; otherwise, defaults are set.
- It connects to Azure Table Storage using the `TableClient` and inserts the deserialized data into a table called `UserDetails`.
- The table entity is created with `Name` and `Email` fields, along with the `partitionKey` and `rowKey`.
- Finally, the function returns an HTTP `200 OK` response indicating that the data was successfully written.


