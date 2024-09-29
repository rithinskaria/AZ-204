using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IFeatureManager _featureManager;

    public HomeController(IConfiguration configuration, IFeatureManager featureManager)
    {
        _configuration = configuration;
        _featureManager = featureManager;
    }

    public async Task<IActionResult> Index()
    {
        // Default settings
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
}
