# AirportCode API Dockerfile for Azure Container Registry (ACR)

## Description
This Dockerfile is used to build a container image for the AirportCode API application built with .NET, specifically for pushing to Azure Container Registry (ACR).

## Prerequisites
- Docker installed on your machine
- Azure CLI installed and configured
- An Azure subscription
- An Azure Container Registry created in your subscription

## Dockerfile Overview

The Dockerfile uses a multi-stage build process to create an optimized Docker image:

1. `base`: Sets up the base image with ASP.NET Core runtime
2. `build`: Builds the application
3. `publish`: Publishes the application
4. `final`: Creates the final, optimized image

## Building and Pushing to ACR

1. Log in to Azure:
   ```
   az login
   ```

2. Log in to your Azure Container Registry:
   ```
   az acr login --name <your-acr-name>
   ```
   Replace `<your-acr-name>` with your ACR name.

3. Build the Docker image with an ACR-compatible tag:
   ```
   docker build -t <your-acr-name>.azurecr.io/airport-code-api:v1 .
   ```
   Replace `<your-acr-name>` with your ACR name and `v1` with your desired version tag.

4. Push the image to ACR:
   ```
   docker push <your-acr-name>.azurecr.io/airport-code-api:v1
   ```

5. Verify the push in ACR:
   ```
   az acr repository show-tags --name <your-acr-name> --repository airport-code-api
   ```

## Using the Image from ACR

To use this image in Azure services (like Azure App Service or Azure Kubernetes Service):

1. In your Azure service configuration, set the image source to Azure Container Registry.
2. Select your ACR and choose the `airport-code-api` repository and the tag you pushed (e.g., `v1`).
3. Configure any necessary environment variables or settings specific to your application.

## Customization

- If your application requires additional setup (like environment variables), add them using the `ENV` instruction in the final stage of the Dockerfile.
- For applications requiring HTTPS, you'll need to expose port 443 and configure SSL in your application and container.

## Troubleshooting

- If you encounter authentication issues, ensure you're logged in to both Azure and your ACR.
- If the push fails, check your ACR permissions and network connectivity.
- For issues with the built image, you can pull it locally and run it to debug:
  ```
  docker run -p 8080:80 <your-acr-name>.azurecr.io/airport-code-api:v1
  ```

## Additional Resources

- [Azure Container Registry documentation](https://docs.microsoft.com/en-us/azure/container-registry/)
- [Deploy to Azure App Service using ACR](https://docs.microsoft.com/en-us/azure/app-service/deploy-container-github-action?tabs=publish-profile)
- [Use ACR with Azure Kubernetes Service](https://docs.microsoft.com/en-us/azure/aks/cluster-container-registry-integration)

