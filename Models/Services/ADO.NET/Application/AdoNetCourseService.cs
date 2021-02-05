using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Corso10157.Models.Options;
using Corso10157.Models.Services.ADO.NET.Infrastructure;
using Corso10157.Models.ViewModel;
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

            FormattableString query =$@"
            SELECT * FROM Courses WHERE Id={id};
            SELECT * FROM Lessons WHERE IdCourse={id}";
            DataSet dataSet = await db.QueryAsync(query);
            var courseTable = dataSet.Tables[0];
            if(courseTable.Rows.Count != 1)
            {
                logger.LogWarning($"Corso {id} non trovato!");
                throw new InvalidOperationException($"Non riesce a ritornare esattamente 1 colonna da Courses {id}");
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

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            /*Per registrare i Log dell'applicazione*/
            logger.LogInformation("Recupero catalogo dei corsi");
            /*Per la Paginazione*/
            long perPage = coursesOptions.CurrentValue.PerPage;
            string orderBy = coursesOptions.CurrentValue.Orderd.By;
            bool ascending = coursesOptions.CurrentValue.Orderd.Ascending;
            string[] allow = coursesOptions.CurrentValue.Orderd.Allow;

            FormattableString query = $"SELECT * FROM Courses;";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var courseList = new List<CourseViewModel>();
            foreach(DataRow courseRow in dataTable.Rows)
            {
                CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }
            return courseList;
        }
    }
}