using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Corso10157.Models.Exception;
using Corso10157.Models.Options;
using Corso10157.Models.Services.ADO.NET.Infrastructure;
using Corso10157.Models.ViewModel;
using Corso10157.Models.Services.ADO.NET.Classes.ValueType;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Corso10157.Models.Services.ADO.NET.Application
{
    public class AdoNetCourseService : ICourseServiceAsync
    {
        private readonly IDatabaseAccessor db;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        private readonly ILogger<AdoNetCourseService> logger;

        public AdoNetCourseService(IDatabaseAccessor db, IOptionsMonitor<CoursesOptions> coursesOptions, ILogger<AdoNetCourseService> logger)
        {
            this.db = db;
            this.coursesOptions = coursesOptions;
            this.logger = logger;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            /*Per registrare i Log dell'applicazione*/
            logger.LogInformation($"Course {id} richiesto");

            FormattableString query = $@"
            SELECT * FROM Courses WHERE Id={id};
            SELECT * FROM Lessons WHERE IdCourse={id}";
            DataSet dataSet = await db.QueryAsync(query);
            var courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count != 1)
            {
                logger.LogWarning($"Corso {id} non trovato!");
                throw new CourseNotFoundException(id);
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);
            var lessonDataTable = dataSet.Tables[1];
            foreach (DataRow lessonRow in lessonDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lezioni.Add(lessonViewModel);
            }
            return courseDetailViewModel;
        }

        public async Task<ListViewModel<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending, int limit, int offset)
        {
            /*Per registrare i Log dell'applicazione*/
            logger.LogInformation("Recupero catalogo dei corsi");
            /*Per la Paginazione*/
            string direction = ascending ? "ASC" : "DESC";

            FormattableString query = $@"SELECT * FROM Courses WHERE NomeCorso 
            LIKE {"%" + search + "%"}
            ORDER BY {(Sql)orderby} {(Sql)direction}
            LIMIT {limit} OFFSET {offset};
            SELECT count(*) FROM Courses WHERE NomeCorso 
            LIKE {"%" + search + "%"}";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var courseList = new List<CourseViewModel>();
            foreach (DataRow courseRow in dataTable.Rows)
            {
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }
            ListViewModel<CourseViewModel> courseListViewModel = new ListViewModel<CourseViewModel>();
            courseListViewModel.Result = courseList;
            courseListViewModel.TotalCount = (long)dataSet.Tables[1].Rows[0][0];
            return courseListViewModel;
        }
    }
}