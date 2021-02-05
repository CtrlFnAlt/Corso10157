using System.Collections.Generic;
using System.Threading.Tasks;
using Corso10157.Models.Services.ADO.NET.Infrastructure;
using Corso10157.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICachedCourseService courseService;
        public CoursesController(ICachedCourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "I Corsi";
            List<CourseViewModel> courses = await courseService.GetCoursesAsync();
            return View(courses);
        }
        public async Task<IActionResult> Detail(int id)
        {
            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            ViewData["Title"] = $"Corso - {viewModel.NomeCorso}";
            return View(viewModel);
        }
    }
}