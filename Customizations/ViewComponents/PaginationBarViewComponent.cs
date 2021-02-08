using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Customizations.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}