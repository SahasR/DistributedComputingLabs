using Microsoft.AspNetCore.Mvc;

namespace WebApplicationGUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
    }
}
