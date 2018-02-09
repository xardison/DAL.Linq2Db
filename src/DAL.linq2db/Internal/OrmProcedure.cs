namespace DAL.linq2db.Internal
{
    public abstract class OrmProcedure
    {
        protected OrmProcedure()
        {
        }
        protected OrmProcedure(OrmParameter[] parameters)
        {
            Parameters = parameters;
        }

        /// "SELECT :p FROM DUAL;"
        public abstract string Sql { get; set; }

        /// new [] { new OrmParameter("p", "USER")}
        public OrmParameter[] Parameters { get; set; }
    }
}