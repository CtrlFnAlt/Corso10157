using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(CacheProfileName = "Home")]
        public IActionResult Index(){
            ViewData["Title"] = "Home Page";
            return View();
        }
    }
}