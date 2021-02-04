using System;
using System.Collections.Generic;

namespace Corso10157.Models.ViewModel
{
    public class CourseDetailViewModel : CourseViewModel
    {
        public string DescrizioneDettagliata { get; set; }
        public List<LessonViewModel> Lezioni { get; set; }
        public TimeSpan Totale
        {
            get
            {
                TimeSpan tempo = new TimeSpan();
                foreach(var item in Lezioni){
                    tempo += item.Durata; 
                }
                return tempo;
            }
        }
    }
}