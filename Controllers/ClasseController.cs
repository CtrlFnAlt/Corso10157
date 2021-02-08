using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class ClasseController : Controller
    {
        [HttpGet]
        public IActionResult Index(string id)
        {
            ViewData["Title"] = "Classi";
            return View();
        }
    }
}