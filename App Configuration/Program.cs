using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Connect to Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect("Endpoint="")
    .UseFeatureFlags()
           .ConfigureRefresh(refresh =>
           {
               refresh.Register("LandingPageMessage", refreshAll: true)
                      .Register("LandingPageBackgroundColor", refreshAll: true)
                      .Register("LandingPageFontSize", refreshAll: true)
                      .SetCacheExpiration(TimeSpan.FromSeconds(5));
           });
});

builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseAzureAppConfiguration();  
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
