namespace DAL.linq2db.Internal
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(string database, bool openTransaction = false);
    }

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create(string database, bool openTransaction = false)
        {
            return new UnitOfWork(database, openTransaction);
        }
    }
}