using System;
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
            FormattableString query =$@"
            SELECT * FROM Courses WHERE Id={id};
            SELECT * FROM Lessons WHERE IdCourse={id}";
            DataSet dataSet = db.Query(query);
            var courseTable = dataSet.Tables[0];
            if(courseTable.Rows.Count != 1)
            {
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

        public List<CourseViewModel> GetCourses()
        {
            FormattableString query = $"SELECT * FROM Courses";
            DataSet dataSet = db.Query(query);
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