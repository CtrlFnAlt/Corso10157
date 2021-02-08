namespace Corso10157.Models.Options
{
    public class CoursesOptions
    {
        public long PerPage { get; set; }
        public int InHome { get; set; }
        public CoursesOrderdOptions Orderd { get; set; }
    }

    public class CoursesOrderdOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}

// {
//   "Courses": {
//     "PerPage": 10,
//     "Orderd": {
//       "By": "NomeCorso",
//       "Ascending": true,
//       "Allow": [
//         "NomeCorso",
//         "Rating",
//         "Prezzo"
//       ]
//     }
//   }
// }