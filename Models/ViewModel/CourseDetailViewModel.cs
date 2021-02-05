using System;
using System.Collections.Generic;
using System.Data;

namespace Corso10157.Models.ViewModel
{
    public class CourseDetailViewModel : CourseViewModel
    {
        public CourseDetailViewModel()
        {
            Lezioni = new List<LessonViewModel>();
        }
        public string DescrizioneDettagliata { get; set; }
        public List<LessonViewModel> Lezioni { get; set; }
        public TimeSpan Totale
        {
            get
            {
                TimeSpan tempo = new TimeSpan();
                foreach (var item in Lezioni)
                {
                    tempo += item.Durata;
                }
                return tempo;
            }
        }
        public static new CourseDetailViewModel FromDataRow(DataRow courseRow)
        {
            var courseDetailViewModel = new CourseDetailViewModel
            {
                NomeCorso = Convert.ToString(courseRow["NomeCorso"]),
                Descrizione = Convert.ToString(courseRow["Descrizione"]),
                Image = Convert.ToString(courseRow["Image"]),
                Autore = Convert.ToString(courseRow["Autore"]),
                Rating = Convert.ToDouble(courseRow["Rating"]),
                Prezzo = Convert.ToDecimal(courseRow["Prezzo"]),
                DescrizioneDettagliata = Convert.ToString(courseRow["DescrizioneDettagliata"]),
                Id = Convert.ToInt32(courseRow["Id"]),
                Lezioni = new List<LessonViewModel>()
            };
            return courseDetailViewModel;
        }
    }
}