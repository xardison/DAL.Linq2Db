namespace DAL.linq2db.Filter
{
    public abstract class FilterBase : Pager, IFilter
    {
        protected FilterBase(int? page = 1, int pageSize = DalParams.DefaultPageSize) : base(page, pageSize)
        {
        }
    }
}
