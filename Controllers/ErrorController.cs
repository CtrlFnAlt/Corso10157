using System;
using Corso10157.Models.Exception;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            switch (feature.Error)
            {
                case CourseNotFoundException exception:
                    ViewData["Title"] = "Corso non Trovato!";
                    Response.StatusCode = 404;
                    return View("CourseNotFound");
                default:
                    ViewData["Title"] = "Errore!";
                    return View();
            }
        }
    }
}