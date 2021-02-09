using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Corso10157.Models.Services.ADO.NET.InputModels
{
    public class CourseCreateInputModel
    {
        [Required(ErrorMessage = "Il Titolo è Obbligatorio!"),
        MinLength(10, ErrorMessage = "Il Titolo deve essere di almeno {1} caratteri."),
        MaxLength(100, ErrorMessage = "Il Titolo deve essere al più di {1} caratteri."),
        RegularExpression(@"^[\w\s\.]+$", ErrorMessage = "Titolo non valido. Puoi utilizzare solo lettere, spazi ed il punto."),
        Remote(action: "IsAvaibleNomecorso", controller: "Courses", ErrorMessage = "Il titolo esiste già")]
        public string NomeCorso { get; set; }
    }
}