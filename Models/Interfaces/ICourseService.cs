using System.Collections.Generic;
using Corso10157.Models.ViewModel;

namespace Corso10157.Models.Interfaces
{
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses();
        CourseDetailViewModel GetCourse(int id);
    }
}