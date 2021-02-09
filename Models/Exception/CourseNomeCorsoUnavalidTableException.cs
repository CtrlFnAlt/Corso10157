using System;

namespace Corso10157.Models.Exception
{
    public class CourseNomeCorsoUnavalidTableException : SystemException
    {
        public CourseNomeCorsoUnavalidTableException(string nomeCorso, SystemException innerException) : base($"Il corso {nomeCorso} è già stato creato!")
        {

        }
    }
}