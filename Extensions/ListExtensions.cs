using System.Collections.Generic;
using System.Text;

namespace CsBenchmark.Extensions
{

    public static class ListExtensions
    {

        public static string ToStringList<T>(this IEnumerable<T> vals, string separator = ", ")
        {
            var sb = new StringBuilder();
            bool first = false;
            foreach (T t in vals)
            {
                if (first)
                {
                    sb.Append(separator);
                }
                sb.Append(t != null ? t.ToString() : "null");
                first = true;
            }
            return sb.ToString();
        }

    }

}
