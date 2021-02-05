using System;
using System.Data;

namespace Corso10157.Models.ViewModel
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public int IdCourse { get; set; }
        public string Titolo { get; set; }
        public TimeSpan Durata { get; set; }

         public static LessonViewModel FromDataRow(DataRow dataRow)
        {
            var lessonViewModel = new LessonViewModel {
                Id = Convert.ToInt32(dataRow["Id"]),
                IdCourse = Convert.ToInt32(dataRow["IdCourse"]),
                Titolo = Convert.ToString(dataRow["Titolo"]),
                Durata = TimeSpan.Parse(Convert.ToString(dataRow["Durata"])),
            };
            return lessonViewModel;
        }
    }
}