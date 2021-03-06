using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace System
{
    public static class Debugging
    {
        public static IEnumerable<T> Where<T>(IEnumerable<T> collection, string property, object value)
        {
            return collection.Where(x =>
            {
                var val = x.GetType().GetProperty(property).GetValue(x, null);
                return val.Equals(value);
            });
        }

        public static IEnumerable<T> Where<T>(IEnumerable<T> collection, char comparer, object value) where T : IComparable
        {
            return collection.Where(x =>
            {
                switch (comparer)
                {
                    case '=':
                        return x.CompareTo(value) == 0;
                    case '<':
                        return x.CompareTo(value) == -1;
                    case '>':
                        return x.CompareTo(value) == 1;
                    case '!':
                        return x.CompareTo(value) != 0;
                    default:
                        throw new InvalidOperationException("Not supported comparer");
                }
            });
        }

        public static IEnumerable<object> Select<T>(IEnumerable<T> collection, string property)
        {
            return collection.Select(x =>
            {
                var val = x.GetType().GetProperty(property).GetValue(x, null);
                return val;
            });
        }




        public static IEnumerable<T> WhereDebug<T>(this IEnumerable<T> collection, string property, object value)
        {
            return collection.Where(x =>
            {
                var val = x.GetType().GetProperty(property).GetValue(x, null);
                return val.Equals(value);
            });
        }

        public static IEnumerable<T> WhereDebug<T>(this IEnumerable<T> collection, char comparer, object value) where T : IComparable
        {
            return collection.Where(x =>
            {
                switch (comparer)
                {
                    case '=':
                        return x.CompareTo(value) == 0;
                    case '<':
                        return x.CompareTo(value) == -1;
                    case '>':
                        return x.CompareTo(value) == 1;
                    case '!':
                        return x.CompareTo(value) != 0;
                    default:
                        throw new InvalidOperationException("Not supported comparer");
                }
            });
        }

        public static IEnumerable<object> SelectDebug<T>(this IEnumerable<T> collection, string property)
        {
            return collection.Select(x =>
            {
                var val = x.GetType().GetProperty(property).GetValue(x, null);
                return val;
            });
        }

        public static IEnumerable<object> SelectDebugProps<T>(this IEnumerable<T> collection, params string[] properties)
        {
            return collection.Select(x =>
            {
                var expando = new ExpandoObject();
                var expandoDict = expando as IDictionary<string, object>;

                foreach (var item in properties)
                {
                    var propParts = item.Split('.');
                    object val = x;
                    var pName = item.Replace(".", "_");

                    foreach (var pp in propParts)
                    {
                        val = val.GetType().GetProperty(pp).GetValue(val, null);
                    }

                    if (expandoDict.ContainsKey(pName))
                        expandoDict[pName] = val;
                    else
                        expandoDict.Add(pName, val);
                }

                return expando;
            });
        }

    }
}
