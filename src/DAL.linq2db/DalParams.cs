namespace DAL.linq2db
{
    public static class DalParams
    {
        public const int DefaultPageSize = 100;

        static DalParams()
        {
            LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
        }

        public static string DatabaseProviderName { get; set; }

        public static string ClientId { get; set; }
        public static string ClientInfo { get; set; }
        public static string ModuleName { get; set; }
        public static string ActionName { get; set; }

        public static void Init()
        {
        }
    }
}
