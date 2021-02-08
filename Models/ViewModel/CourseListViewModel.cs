using System.Collections.Generic;
using Corso10157.Models.Services.ADO.NET.InputModels;

namespace Corso10157.Models.ViewModel
{
    public class CourseListViewModel
    {
        public ListViewModel<CourseViewModel> Courses { get; set; }
        public CourseListInputModel Input { get; set; }
    }
}