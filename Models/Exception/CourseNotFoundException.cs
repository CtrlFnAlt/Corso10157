using System;
namespace Corso10157.Models.Exception
{
    public class CourseNotFoundException : SystemException
    {
        public CourseNotFoundException(int id) : base($"Corso {id} non trovato.")
        {
            
        }
    }
}