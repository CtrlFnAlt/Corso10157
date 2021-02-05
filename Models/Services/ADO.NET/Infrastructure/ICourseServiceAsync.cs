using System.Collections.Generic;
using System.Threading.Tasks;
using Corso10157.Models.ViewModel;

namespace Corso10157.Models.Services.ADO.NET.Infrastructure
{
    public interface ICourseServiceAsync
    {
        Task<List<CourseViewModel>> GetCoursesAsync();
        Task<CourseDetailViewModel> GetCourseAsync(int id);
    }
}