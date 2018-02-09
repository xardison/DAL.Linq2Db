using System.Diagnostics;
using LinqToDB.Data;

namespace DAL.linq2db.Internal
{
    internal abstract class OrmLinqToDbBase : DataConnection
    {
#if DEBUG
        private static long _sqlCount;

        static OrmLinqToDbBase()
        {
            DataConnection.WriteTraceLine = (msg, info) =>
            {
                Trace.WriteLine(msg.StartsWith("Execution")
                    ? msg
                    : string.Format(
                        "{0}\n[SQL:{1}] {2}\n{0}",
                        string.Empty.PadLeft(30, '-'),
                        (++_sqlCount).ToString().PadLeft(6, '0'),
                        msg));
            };

            DataConnection.TurnTraceSwitchOn();
        }
#endif

        protected OrmLinqToDbBase(string database)
            : base(DbHelper.GetProvider(), DbHelper.GetConnectionString(database))
        {
            DbHelper.UpdateConnectionInfo(Connection);
        }
    }
}