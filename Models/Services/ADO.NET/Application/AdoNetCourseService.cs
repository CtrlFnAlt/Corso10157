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
using Corso10157.Models.Services.ADO.NET.InputModels;
using Microsoft.Data.Sqlite;

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

        public async Task<List<CourseViewModel>> GetBestRatingCoursesAsync()
        {
            ListViewModel<CourseViewModel> result = await GetCoursesAsync("", 1, "Rating", false, coursesOptions.CurrentValue.InHome, 0);
            return result.Result;
        }

        public async Task<List<CourseViewModel>> GetMostRecentCoursesAsync()
        {
            ListViewModel<CourseViewModel> result = await GetCoursesAsync("", 1, "id", false, coursesOptions.CurrentValue.InHome, 0);
            return result.Result;
        }

        public async Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel inputModel)
        {
            string nomeCorso = inputModel.NomeCorso;
            string autore = "Guido Bianchi";
            try
            {
                var dataSet = await db.QueryAsync($@"INSERT INTO Courses 
            (NomeCorso,Autore,Image,Descrizione,Prezzo,Rating,DescrizioneDettagliata) 
            VALUES({nomeCorso},{autore},'default.png','',0,0,'');
            SELECT last_insert_rowid()");
                int idCorso = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                CourseDetailViewModel course = await GetCourseAsync(idCorso);
                return course;
            }
            catch (SqliteException exc) when (exc.SqliteErrorCode == 19)
            {
                throw new CourseNomeCorsoUnavalidTableException(nomeCorso, exc);
            }
        }

        public async Task<bool> IsAvaibleNomecorsoAsync(string nomeCorso)
        {
            DataSet result = await db.QueryAsync($"SELECT COUNT(*) FROM COURSES WHERE NomeCorso LIKE {nomeCorso}");
            bool nomeCorsoAvaible = Convert.ToInt32(result.Tables[0].Rows[0][0]) == 0;
            return nomeCorsoAvaible;
        }
    }
}