using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class ClasseController : Controller
    {
        public IActionResult Index(string id){
            return Content($"Abbiamo ricevuto l'id: {id}");
        }
    }
}