using System;
using System.Data;

namespace DAL.linq2db.Internal
{
    public interface IUnitOfWork : IDisposable
    {
        T CreateService<T>() where T : IService;

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }

    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IOrm _orm;

        public UnitOfWork(string database, bool isAutoStartTransaction = false)
        {
            _orm = new OrmLinqToDb(database);

            if (isAutoStartTransaction)
            {
                BeginTransaction();
            }
        }

        public T CreateService<T>() where T : IService
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { _orm });
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _orm.BeginTransaction(isolationLevel);
        }
        public void Commit()
        {
            _orm.CommitTransaction();
        }
        public void Rollback()
        {
            _orm.RollbackTransaction();
        }

        public void Dispose()
        {
            Rollback();
            _orm.Dispose();
        }
    }
}