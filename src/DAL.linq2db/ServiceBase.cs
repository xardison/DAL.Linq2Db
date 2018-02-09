using DAL.linq2db.Internal;

namespace DAL.linq2db
{
    public interface IService
    {
    }

    public abstract class ServiceBase : IService
    {
        protected ServiceBase(IOrm orm)
        {
            Orm = orm;
        }

        protected IOrm Orm { get; private set; }
    }
}