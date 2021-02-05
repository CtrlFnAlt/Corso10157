using System;
using System.Data;

namespace Corso10157.Models.ViewModel
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string NomeCorso { get; set; }
        public string Autore { get; set; }
        public string Image { get; set; }
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public double Rating { get; set; }

        public static CourseViewModel FromDataRow(DataRow courseRow)
        {
            var courseViewModel = new CourseViewModel()
            {
                NomeCorso = Convert.ToString(courseRow["NomeCorso"]),
                Image = Convert.ToString(courseRow["Image"]),
                Autore = Convert.ToString(courseRow["Autore"]),
                Descrizione = Convert.ToString(courseRow["Descrizione"]),
                Prezzo = Convert.ToDecimal(courseRow["Prezzo"]),
                Rating = Convert.ToDouble(courseRow["Rating"]),
                Id = Convert.ToInt32(courseRow["Id"])
            };
            return courseViewModel;
        }
    }
}