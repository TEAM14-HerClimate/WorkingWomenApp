using Microsoft.AspNetCore.Mvc;

namespace WorkingWomenApp.Controllers
{
    public class HealthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
