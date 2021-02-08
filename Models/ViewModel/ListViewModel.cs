using System.Collections.Generic;

namespace Corso10157.Models.ViewModel
{
    public class ListViewModel<T>
    {
        public List<T> Result { get; set; }
        public long TotalCount { get; set; }

    }
    
}