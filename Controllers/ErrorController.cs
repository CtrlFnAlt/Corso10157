using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Errore!";
            return View();
        }
    }
}