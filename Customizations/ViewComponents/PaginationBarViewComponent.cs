using Corso10157.Models.ViewModel;
using Corso10157.Models.ViewModel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Customizations.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPaginationInfo model)
        {
            return View(model);
        }
    }
}