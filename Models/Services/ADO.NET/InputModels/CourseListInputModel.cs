using System;
using System.Linq;
using Corso10157.Customizations.ModelBinders;
using Corso10157.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Corso10157.Models.Services.ADO.NET.InputModels
{
    [ModelBinder(BinderType = typeof(CourseListInputModelBinder))]
    public class CourseListInputModel
    {
        public string Search { get; }
        public int Page { get; }
        public string OrderBy { get; }
        public bool Ascending { get; }
        public int Limit { get; }
        public int Offset { get; }
//IOptionsMonitor<CoursesOptions> coursesOptions
        public CourseListInputModel(string search, int page, string orderby, bool ascending, CoursesOptions coursesOptions)
        {
            if (!coursesOptions.Orderd.Allow.Contains(orderby))
            {
                orderby = coursesOptions.Orderd.By;
                ascending = coursesOptions.Orderd.Ascending;
            }
            page = Math.Max(1, page); //Prende il valore massimo tra i due passati
            
            string direction = ascending ? "ASC" : "DESC";

            Search = search;
            Page = page;
            OrderBy = orderby;
            Ascending = ascending;
            Limit = (int)coursesOptions.PerPage;
            Offset = (page - 1) * Limit;  
        }
    }
}