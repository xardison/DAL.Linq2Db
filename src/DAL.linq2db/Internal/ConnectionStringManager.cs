namespace DAL.linq2db.Internal
{
    public class ConnectionStringManager
    {
        public const string DefaultConnectionString = @"Data Source={0};User ID=/;Validate Connection=true;Pooling=false;";
        private static string _connectionString;

        public static string GetConnectionString(string database)
        {
            return _connectionString == null
                ? string.Format(DefaultConnectionString, database)
                : (database == null ? _connectionString : string.Format(_connectionString, database));
        }

        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
