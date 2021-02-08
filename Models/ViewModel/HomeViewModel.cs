using System.Collections.Generic;

namespace Corso10157.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<CourseViewModel> BestRatingCourses { get; set; }
        public List<CourseViewModel> MostRecentCourses { get; set; }

    }
}