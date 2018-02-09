using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using DAL.linq2db.StoredProcedure;
using LinqToDB;
using LinqToDB.Data;

namespace DAL.linq2db.Internal
{
    public interface IOrm : IDisposable
    {
        IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        void RollbackTransaction();

        object InsertEntity<T>(T entity) where T : class;
        int UpdateEntity<T>(T entity) where T : class;
        int DeleteEntity<T>(T entity) where T : class;

        T GetByPk<T>(object pk) where T : class;
        IList<T> GetAll<T>() where T : class;
        IList<T> GetByParameters<T>(Func<T, bool> where) where T : class;
        IList<T> GetByPropertyValue<T, D>(string propertyName, D valueForFilter) where T : class;

        Func<T, bool> SimpleComparison<T>(string property, object value) where T : class;

        IQueryable<T> Query<T>() where T : class;
        IEnumerable<T> Query<T>(string sql);
        IEnumerable<T> Query<T>(string sql, OrmParameter parameter);
        IEnumerable<T> Query<T>(string sql, OrmParameter[] parameters);

        int Execute(string sql);
        int Execute(string sql, OrmParameter[] parameters);
        int Execute(OrmProcedure sp);

        T Execute<T>(string sql);
        T Execute<T>(string sql, OrmParameter parameter);
        T Execute<T>(string sql, OrmParameter[] parameters);
        T Execute<T>(OrmProcedure sp);

        int ExecuteProc(string sql, OrmParameter[] parameters);
        int ExecuteProc(OrmProcedure sp);

        T ExecuteProc<T>(string sql, OrmParameter[] parameters);
        T ExecuteProc<T>(OrmProcedure sp);

        DataReader ExecuteReader(string sql);
        DataReader ExecuteReader(string sql, OrmParameter[] parameters);
    }

    internal class OrmLinqToDb : OrmLinqToDbBase, IOrm
    {
        public OrmLinqToDb(string database) : base(database)
        {
        }

        public new IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return base.BeginTransaction(isolationLevel);
        }

        public object InsertEntity<T>(T entity) where T : class
        {
            return this.InsertWithIdentity(entity);
        }
        public int UpdateEntity<T>(T entity) where T : class
        {
            return this.Update(entity);
        }
        public int DeleteEntity<T>(T entity) where T : class
        {
            return this.Delete(entity);
        }

        public T GetByPk<T>(object pk) where T : class
        {
            var pkName = typeof(T).GetProperties()
                .First(
                    prop =>
                        prop.GetCustomAttributes(typeof(LinqToDB.Mapping.PrimaryKeyAttribute), false).Any());

            var expression = SimpleComparison<T>(pkName.Name, pk);

            return GetTable<T>().Where(expression).FirstOrDefault();
        }
        public IList<T> GetAll<T>() where T : class
        {
            return GetTable<T>().ToList();
        }
        public IList<T> GetByParameters<T>(Func<T, bool> where) where T : class
        {
            return GetTable<T>().Where(where).ToList();
        }
        public IList<T> GetByPropertyValue<T, D>(string propertyName, D valueForFilter) where T : class
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return GetAll<T>();
            }

            var expression = SimpleComparison<T>(propertyName, valueForFilter);

            var data = GetTable<T>().Where(expression).ToList();
            return data;
        }

        public Func<T, bool> SimpleComparison<T>(string property, object value) where T : class
        {
            var type = typeof(T);
            var pe = Expression.Parameter(type, "p");
            var propertyReference = Expression.Property(pe, property);
            var constantReference = Expression.Constant(value);

            return Expression.Lambda<Func<T, bool>>(
                Expression.Equal(propertyReference, constantReference), pe).Compile();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return from obj in GetTable<T>() select obj;
        }
        IEnumerable<T> IOrm.Query<T>(string sql)
        {
            return this.Query<T>(sql);
        }
        IEnumerable<T> IOrm.Query<T>(string sql, OrmParameter parameter)
        {
            var func = new Func<string, DataParameter[], IEnumerable<T>>(this.Query<T>);
            return ExecuteWithParams(func, sql, new[] { parameter });
        }
        IEnumerable<T> IOrm.Query<T>(string sql, OrmParameter[] parameters)
        {
            var func = new Func<string, DataParameter[], IEnumerable<T>>(this.Query<T>);
            return ExecuteWithParams(func, sql, parameters);
        }

        int IOrm.Execute(string sql)
        {
            return this.Execute(sql);
        }
        int IOrm.Execute(string sql, OrmParameter[] parameters)
        {
            var func = new Func<string, DataParameter[], int>(this.Execute);
            return ExecuteWithParams(func, sql, parameters);
        }
        int IOrm.Execute(OrmProcedure sp)
        {
            var func = new Func<string, DataParameter[], int>(this.Execute);
            return ExecuteWithParams(func, sp.Sql, sp.Parameters);
        }

        T IOrm.Execute<T>(string sql)
        {
            return this.Execute<T>(sql);
        }
        T IOrm.Execute<T>(string sql, OrmParameter parameter)
        {
            var func = new Func<string, DataParameter[], T>(this.Execute<T>);
            return ExecuteWithParams(func, sql, new[] { parameter });
        }
        T IOrm.Execute<T>(string sql, OrmParameter[] parameters)
        {
            var func = new Func<string, DataParameter[], T>(this.Execute<T>);
            return ExecuteWithParams(func, sql, parameters);
        }
        T IOrm.Execute<T>(OrmProcedure sp)
        {
            var func = new Func<string, DataParameter[], T>(this.Execute<T>);
            return ExecuteWithParams(func, sp.Sql, sp.Parameters);
        }

        int IOrm.ExecuteProc(string sql, OrmParameter[] parameters)
        {
            var func = new Func<string, DataParameter[], int>(this.ExecuteProc);
            return ExecuteWithParams(func, sql, parameters);
        }
        int IOrm.ExecuteProc(OrmProcedure sp)
        {
            var func = new Func<string, DataParameter[], int>(this.ExecuteProc);
            return ExecuteWithParams(func, sp.Sql, sp.Parameters);
        }

        T IOrm.ExecuteProc<T>(string sql, OrmParameter[] parameters)
        {
            var func = new Func<string, DataParameter[], T>(this.ExecuteProc<T>);
            return ExecuteWithParams(func, sql, parameters);
        }
        T IOrm.ExecuteProc<T>(OrmProcedure sp)
        {
            var func = new Func<string, DataParameter[], T>(this.ExecuteProc<T>);
            return ExecuteWithParams(func, sp.Sql, sp.Parameters);
        }

        DataReader IOrm.ExecuteReader(string sql)
        {
            return this.ExecuteReader(sql);
        }
        DataReader IOrm.ExecuteReader(string sql, OrmParameter[] parameters)
        {
            var func = new Func<string, DataParameter[], DataReader>(this.ExecuteReader);
            return ExecuteWithParams(func, sql, parameters);
        }

        // private methods ======================================================================================
        private T ExecuteWithParams<T>(Func<string, DataParameter[], T> execFunction, string sql, OrmParameter[] parameters)
        {
            var dataParams = DbHelper.GetDataParameter(parameters);
            var result = execFunction(sql, dataParams);

            dataParams.Where(dp => dp.Direction == ParameterDirection.Output).ToList().ForEach(dp =>
                {
                    var ormParam = parameters.FirstOrDefault(p => p.Name == dp.Name);
                    if (ormParam != null)
                    {
                        ormParam.Value = dp.Value;
                    }
                });

            return result;
        }
    }
}