using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.Database.Models;

namespace WorkingWomenApp.Controllers
{


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherApiService _weatherApiService;
        public HomeController(ILogger<HomeController> logger, IWeatherApiService weatherApiService)
        {
            _logger = logger;
            _weatherApiService = weatherApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }


        [ResponseCache(VaryByHeader = "User-Agent", Duration = 10800, Location = ResponseCacheLocation.Any)]
        public async Task<JsonResult> GetWeatherData(double latitude, double longitude)
        {

           latitude  = Math.Round(latitude, 4);
           longitude = Math.Round(longitude, 4);

            var (success, errorMessage, weatherData) = await _weatherApiService.GetWeatherInfo(latitude, longitude);
            if (success)
            {
                if (weatherData == null)
                {
                    //Console.WriteLine("Company data was null");
                    return new JsonResult(new
                    {
                        code = false,
                        message = "Business not found"
                    });
                }
                return new JsonResult(new
                {
                    code = true,
                    name = weatherData.entity_name,
                    message = "Business Verified"
                });
            }



            return new JsonResult(new
            {
                code = true,
                message = errorMessage,

            });

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
