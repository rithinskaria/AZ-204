# Hosting a Static Website on Azure Storage

## Overview
This project demonstrates how to host a static website using Azure Storage. The `index.html` file in this folder serves as the main page of the website.

## Prerequisites
- An Azure account with an active subscription
- Azure CLI installed on your local machine
- A storage account in Azure (StorageV2 (general purpose v2) or Blob storage account)

## Setup Steps

1. **Enable static website hosting on your storage account**

   Use the Azure CLI to enable static website hosting:

   ```bash
   az storage blob service-properties update --account-name <storage-account-name> --static-website --index-document index.html
   ```

2. **Upload your index.html file**

   Upload the `index.html` file to the `$web` container in your storage account:

   ```bash
   az storage blob upload --account-name <storage-account-name> --container-name '$web' --file index.html --name index.html
   ```

3. **Get the website URL**

   Retrieve the URL for your static website:

   ```bash
   az storage account show --name <storage-account-name> --query "primaryEndpoints.web" --output tsv
   ```

## File Structure

```
your-project-folder/
│
├── index.html          # Main page of your static website
└── README.md           # This file
```

## Customizing Your Website

Edit the `index.html` file to customize your website. After making changes, re-upload the file using the Azure CLI command from step 2.

## Troubleshooting

- Ensure that your storage account is a StorageV2 (general purpose v2) or Blob storage account.
- Verify that static website hosting is enabled on your storage account.
- Check that the `index.html` file is uploaded to the `$web` container.
- Clear your browser cache if changes are not immediately visible.

## Additional Resources

- [Static website hosting in Azure Storage](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-static-website)
- [Azure Storage CLI documentation](https://docs.microsoft.com/en-us/cli/azure/storage?view=azure-cli-latest)
- [Tutorial: Host a static website on Blob Storage](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-static-website-how-to?tabs=azure-cli)

## Security Considerations

- Enable HTTPS for your static website.
- Use Azure CDN for better performance and additional security features.
- Implement proper access controls on your storage account.

## Cost Considerations

Hosting a static website on Azure Storage is generally cost-effective, but be aware of the following:

- Storage costs for the files in the `$web` container.
- Transaction costs for requests to your website.
- Data transfer costs for outbound traffic.

Review the [Azure Storage pricing](https://azure.microsoft.com/en-us/pricing/details/storage/) for more details.
