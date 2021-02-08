using System.Collections.Generic;
using System.Threading.Tasks;
using Corso10157.Models.Services.ADO.NET.Infrastructure;
using Corso10157.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(CacheProfileName = "Home")]
        public async Task<IActionResult> Index([FromServices] ICachedCourseService courseservice)
        {
            ViewData["Title"] = "Home Page";
            List<CourseViewModel> bestRatingCourses = await courseservice.GetBestRatingCoursesAsync();
            List<CourseViewModel> mostRecentCourses = await courseservice.GetMostRecentCoursesAsync();
            HomeViewModel homeViewModel = new HomeViewModel(){
                BestRatingCourses = bestRatingCourses,
                MostRecentCourses = mostRecentCourses
            };
            return View(homeViewModel);
        }
    }
}