using System.Collections.Generic;
using System.Data;
using Corso10157.Models.Interfaces;
using Corso10157.Models.Services.ADO.NET.Infrastructure;
using Corso10157.Models.ViewModel;

namespace Corso10157.Models.Services.ADO.NET.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;
        public AdoNetCourseService(IDatabaseAccessor db)
        {
            this.db = db;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<CourseViewModel> GetCourses()
        {
            string query = "SELECT * FROM Courses";
            DataSet dataSet = db.Query(query);
            throw new System.NotImplementedException();
        }
    }
}