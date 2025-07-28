using Microsoft.AspNetCore.Mvc;

namespace WorkingWomenApp.Controllers
{
    public class ClimateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
