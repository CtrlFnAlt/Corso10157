using System.Collections.Generic;
using System.Threading.Tasks;
using Corso10157.Models.Services.ADO.NET.InputModels;
using Corso10157.Models.ViewModel;

namespace Corso10157.Models.Services.ADO.NET.Infrastructure
{
    public interface ICourseServiceAsync
    {
        Task<ListViewModel<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending, int limit, int offset);
        Task<CourseDetailViewModel> GetCourseAsync(int id);
        Task<List<CourseViewModel>> GetBestRatingCoursesAsync();
        Task<List<CourseViewModel>> GetMostRecentCoursesAsync();
        Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel inputModel);
    }
}