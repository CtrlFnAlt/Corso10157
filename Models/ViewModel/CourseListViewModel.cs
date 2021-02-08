using System.Collections.Generic;
using Corso10157.Models.Services.ADO.NET.InputModels;
using Corso10157.Models.ViewModel.Interfaces;

namespace Corso10157.Models.ViewModel
{
    public class CourseListViewModel : IPaginationInfo
    {
        public ListViewModel<CourseViewModel> Courses { get; set; }
        public CourseListInputModel Input { get; set; }

        int IPaginationInfo.CurrentPage => Input.Page;

        long IPaginationInfo.TotalResult => Courses.TotalCount;

        int IPaginationInfo.ResultPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;

        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
    }
}