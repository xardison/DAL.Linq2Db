namespace DAL.linq2db.Filter
{
    public interface IPager
    {
        int TotalItems { get; }
        int PageCurrent { get; }
        int PageSize { get; }
        int PageTotal { get; }
        int PageStart { get; }
        int PageEnd { get; }

        void RecalcPages(int totalItems = -1);
    }
}