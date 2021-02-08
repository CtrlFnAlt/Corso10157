namespace Corso10157.Models.ViewModel.Interfaces
{
    public interface IPaginationInfo
    {
        int CurrentPage { get; }
        long TotalResult { get; }
        int ResultPage { get; }

        string Search { get; }
        string OrderBy { get; }
        bool Ascending { get; }
    }
}