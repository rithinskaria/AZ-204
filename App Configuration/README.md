# ASP.NET Core Web App with Azure App Configuration and Feature Flags

## Description
This ASP.NET Core web application demonstrates the use of Azure App Configuration for managing application settings and Feature Flags for controlling feature availability. The application dynamically updates its UI based on configuration settings and feature flags.

## Key Components

### Program.cs
- Configures the application to use Azure App Configuration
- Sets up Feature Management
- Configures dynamic refresh for specific configuration keys

### Models/ErrorViewModel.cs
- Defines a model for error handling

### Controllers/HomeController.cs
- Retrieves configuration values and checks feature flags
- Applies different UI settings based on configuration and feature state

### Views/Home/Index.cshtml
- Displays a dynamically configured message with customizable background color and font size

## How to Use

1. Ensure you have the necessary NuGet packages installed (see Required Packages section).
2. Replace the Azure App Configuration connection string in `Program.cs` with your own.
3. Set up the following configuration keys in your Azure App Configuration instance:
   - `LandingPageMessage`
   - `LandingPageBackgroundColor`
   - `LandingPageFontSize`
4. Create a feature flag named `ShowNewUI` in your Azure App Configuration instance.
5. Run the application.

## Configuration

In `Program.cs`, replace the following with your Azure App Configuration connection string:
```csharp
options.Connect("Your-Azure-App-Configuration-Connection-String")
```

## Required Packages
- Microsoft.Azure.AppConfiguration.AspNetCore
- Microsoft.FeatureManagement.AspNetCore

To install the required packages, run:
```
dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
dotnet add package Microsoft.FeatureManagement.AspNetCore
```

## Code Explanation

### Program.cs
```csharp
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("Your-Connection-String")
    .UseFeatureFlags()
    .ConfigureRefresh(refresh =>
    {
        refresh.Register("LandingPageMessage", refreshAll: true)
               .Register("LandingPageBackgroundColor", refreshAll: true)
               .Register("LandingPageFontSize", refreshAll: true)
               .SetCacheExpiration(TimeSpan.FromSeconds(5));
    });
});
```
This code configures the application to use Azure App Configuration, enables feature flags, and sets up dynamic refresh for specific configuration keys.

### HomeController.cs
```csharp
public async Task<IActionResult> Index()
{
    ViewBag.Message = _configuration["LandingPageMessage"];
    ViewBag.BackgroundColor = _configuration["LandingPageBackgroundColor"];
    ViewBag.FontSize = _configuration["LandingPageFontSize"];
    if (await _featureManager.IsEnabledAsync("ShowNewUI"))
    {
        ViewBag.Message = "Welcome to the New UI";
        ViewBag.BackgroundColor = "#fab691"; 
        ViewBag.FontSize = "18px";           
    }
    return View();
}
```
This code retrieves configuration values and checks if the `ShowNewUI` feature flag is enabled. If enabled, it applies different UI settings.

## Additional Notes
- Ensure your Azure App Configuration instance is properly set up with the required keys and feature flags.
- The application uses a 5-second cache expiration for configuration values. Adjust this as needed for your use case.
- In a production environment, secure your connection strings and consider using managed identities for authentication.
- This example uses ViewBag for simplicity. In a larger application, consider using strongly-typed models.

## Troubleshooting
- If the configuration doesn't seem to update, check the Azure App Configuration connection string and ensure the keys are correctly set up.
- Verify that the `ShowNewUI` feature flag is properly configured in Azure App Configuration.
- Check the application logs for any errors related to configuration retrieval or feature management.
