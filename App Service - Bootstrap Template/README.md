# Bootstrap Web Template for Azure App Service Deployment Demo

## Overview
This project contains a bootstrap web template downloaded from the internet, specifically structured to demonstrate deployment to Azure App Service. It provides a practical example of how a typical web application can be deployed and hosted on Azure.

## Project Structure
```
project-root/
│
├── css/            # CSS stylesheets
├── font/           # Font files
├── images/         # Image assets
├── js/             # JavaScript files
├── index.html      # Main HTML file
├── LICENSE.txt     # License information
└── README.md       # This file
```

## Purpose
The primary purpose of this template is to serve as a demo for deploying a static website to Azure App Service. It showcases:
1. The structure of a typical web application
2. How static assets are organized
3. The deployment process to Azure App Service

## Deploying to Azure App Service

### Prerequisites
- An Azure account with an active subscription
- Azure CLI installed on your local machine
- Git (optional, for deployment via Git)

### Deployment Steps

1. **Create an App Service Plan**
   ```
   az appservice plan create --name myAppServicePlan --resource-group myResourceGroup --sku F1
   ```

2. **Create a Web App**
   ```
   az webapp create --name myWebApp --resource-group myResourceGroup --plan myAppServicePlan
   ```

3. **Deploy the Web App**
   - Using Git:
     ```
     az webapp deployment source config --name myWebApp --resource-group myResourceGroup --repo-url <repository-url> --branch main --manual-integration
     ```
   - Using ZIP deploy:
     ```
     az webapp deployment source config-zip --resource-group myResourceGroup --name myWebApp --src myWebApp.zip
     ```

4. **Configure for Static HTML**
   ```
   az webapp config set --resource-group myResourceGroup --name myWebApp --web-sockets-enabled false
   ```

5. **Access Your Web App**
   Your web app will be available at `https://myWebApp.azurewebsites.net`

## Customization
- Modify `index.html` to change the content of the web page
- Update CSS in the `css` folder to alter the styling
- Add or modify JavaScript in the `js` folder for dynamic behavior
- Replace images in the `images` folder with your own assets

## Testing Locally
Before deployment, you can test the website locally by opening `index.html` in a web browser.

## Additional Resources
- [Deploy a static HTML web app to Azure](https://docs.microsoft.com/en-us/azure/app-service/quickstart-html)
- [Azure App Service documentation](https://docs.microsoft.com/en-us/azure/app-service/)
- [Bootstrap documentation](https://getbootstrap.com/docs/)

## License
Please refer to the `LICENSE.txt` file for licensing information.

## Contributing
This template is for demonstration purposes. However, if you have suggestions or improvements for the deployment process, please feel free to open an issue or submit a pull request.
