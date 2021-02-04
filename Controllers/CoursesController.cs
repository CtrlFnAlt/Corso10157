using System.Collections.Generic;
using Corso10157.Models.Interfaces;
using Corso10157.Models.Services.Application;
using Corso10157.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "I Corsi";
            List<CourseViewModel> courses = courseService.GetCourses();
            return View(courses);
        }
        public IActionResult Detail(int id)
        {
            CourseDetailViewModel viewModel = courseService.GetCourse(id);
            ViewData["Title"] = $"Corso - {viewModel.NomeCorso}";
            return View(viewModel);
        }
    }
}