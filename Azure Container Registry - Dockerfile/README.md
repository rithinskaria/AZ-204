# AirportCode API Dockerfile for Azure Container Registry (ACR)

## Description
This Dockerfile is used to build a container image for the AirportCode API application built with .NET, specifically for Azure Container Registry (ACR) using the `az acr build` command.

## Prerequisites
- Azure CLI installed and configured
- An Azure subscription
- An Azure Container Registry created in your subscription

## Dockerfile Overview

The Dockerfile uses a multi-stage build process to create an optimized Docker image:

1. `base`: Sets up the base image with ASP.NET Core runtime
2. `build`: Builds the application
3. `publish`: Publishes the application
4. `final`: Creates the final, optimized image

## Building and Pushing to ACR using az acr build

1. Log in to Azure:
   ```
   az login
   ```

2. Set your current subscription (if you have multiple):
   ```
   az account set --subscription <your-subscription-id>
   ```

3. Build and push the image to ACR using a single command:
   ```
   az acr build --registry <your-acr-name> --image airport-code-api:v1 .
   ```
   Replace `<your-acr-name>` with your ACR name and `v1` with your desired version tag.

   This command does the following:
   - Sends the build context (including the Dockerfile) to ACR
   - Builds the image in ACR
   - Pushes the resulting image to your ACR repository

4. Verify the push in ACR:
   ```
   az acr repository show-tags --name <your-acr-name> --repository airport-code-api
   ```

## Advantages of using az acr build

- No need for a local Docker installation
- Builds happen in Azure, saving local resources and bandwidth
- Automatically pushes the image to ACR after building
- Leverages Azure's infrastructure for potentially faster builds

## Using the Image from ACR

To use this image in Azure services (like Azure App Service or Azure Kubernetes Service):

1. In your Azure service configuration, set the image source to Azure Container Registry.
2. Select your ACR and choose the `airport-code-api` repository and the tag you built (e.g., `v1`).
3. Configure any necessary environment variables or settings specific to your application.

## Customization

- If your application requires additional setup (like environment variables), add them using the `ENV` instruction in the final stage of the Dockerfile.
- For applications requiring HTTPS, you'll need to expose port 443 and configure SSL in your application and container.

## Troubleshooting

- If you encounter authentication issues, ensure you're logged in to Azure and have the necessary permissions for your ACR.
- If the build fails, check the output for any error messages. Common issues include missing files in the build context or syntax errors in the Dockerfile.
- You can view build logs in the Azure portal or use the Azure CLI:
  ```
  az acr task logs --registry <your-acr-name>
  ```

## Additional Resources

- [Azure Container Registry documentation](https://docs.microsoft.com/en-us/azure/container-registry/)
- [az acr build command reference](https://docs.microsoft.com/en-us/cli/azure/acr?view=azure-cli-latest#az_acr_build)
- [Deploy to Azure App Service using ACR](https://docs.microsoft.com/en-us/azure/app-service/deploy-container-github-action?tabs=publish-profile)
- [Use ACR with Azure Kubernetes Service](https://docs.microsoft.com/en-us/azure/aks/cluster-container-registry-integration)
