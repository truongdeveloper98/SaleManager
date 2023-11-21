using Microsoft.AspNetCore.Mvc;

namespace SalesManagerSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
