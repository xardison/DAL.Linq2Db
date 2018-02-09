using System;
using System.Linq;
using System.Text.RegularExpressions;
using DAL.linq2db.Filter;
using LinqToDB;
using LinqToDB.Linq;

namespace DAL.linq2db
{
    public static class OrmExtensions
    {
        private const string StrAny = "%";

        public static IQueryable<T> Paging<T>(this IQueryable<T> query, IPager pager) where T : class
        {
            if (pager == null || pager.PageSize <= 0)
            {
                return query;
            }

            pager.RecalcPages(query.Count());

            return query.Skip((pager.PageCurrent - 1) * pager.PageSize).Take(pager.PageSize);
        }

        [Sql.Expression("{0} between {1} and {2}", ServerSideOnly = true)]
        public static bool Between(this DateTime date, DateTime date1, DateTime date2)
        {
            throw new LinqException();
        }

        public static string MatchStart(this string prop)
        {
            return StrAny + prop;
        }
        public static string MatchEnd(this string prop)
        {
            return prop + StrAny;
        }
        public static string MatchAny(this string prop)
        {
            var pattern = Regex.Replace(StrAny + prop + StrAny, "[,*]+", StrAny);
            return Regex.Replace(pattern, "[" + StrAny + "]+", StrAny);
        }

        public static string ReplaceStar(this string prop)
        {
            return prop.Replace("*", StrAny);
        }
    }
}