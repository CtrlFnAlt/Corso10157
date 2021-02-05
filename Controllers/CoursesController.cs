using System.Collections.Generic;
using System.Threading.Tasks;
using Corso10157.Models.Interfaces;
using Corso10157.Models.Services.PlaceHolder.Infrastructure;
using Corso10157.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseServiceAsync courseService;
        public CoursesController(ICourseServiceAsync courseService)
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