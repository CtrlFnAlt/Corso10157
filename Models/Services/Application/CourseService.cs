using System;
using System.Collections.Generic;
using Corso10157.Models.ViewModel;

namespace Corso10157.Models.Services.Application
{
    public class CourseService
    {
        public List<CourseViewModel> GetCourses()
        {
           var listaCorsi = new List<CourseViewModel>();
            Random rand = new Random();
            for (int i = 1; i <= 20; i++)
            {
                decimal prezzo = Convert.ToDecimal(rand.NextDouble() * 10 + 10);    
                CourseViewModel course = new CourseViewModel()
                {
                    Id = i,
                    NomeCorso = $"Corso {i}",
                    Autore = "Pinco Pallo",
                    Rating = rand.NextDouble() * 5.0,
                    Image = "index.png",
                    Prezzo = Convert.ToDecimal(prezzo),
                    Descrizione = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                };
                listaCorsi.Add(course);
            }
           return listaCorsi;
        }

        internal CourseDetailViewModel GetCourses(int id)
        {
            Random rand = new Random();
            CourseDetailViewModel course = new CourseDetailViewModel(){
                Id = id,
                NomeCorso = $"Corso {id}",
                Prezzo = Convert.ToDecimal(12.50),
                Autore = "Nome Cognome",
                Rating = rand.Next(10,50)/10,
                Image = "index.png",
                DescrizioneDettagliata = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.",
                Lezioni = new List<LessonViewModel>()
            };
            for(var i = 1; i<=5; i++){
                var lezione = new LessonViewModel{
                    Titolo = $"Lezione {i}",
                    Durata = TimeSpan.FromSeconds(rand.Next(40,90))
                };
                course.Lezioni.Add(lezione);
            }
            return course;
        }
    }
}