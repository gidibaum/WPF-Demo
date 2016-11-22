using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base
{
    public static class ListExtensions
    {

        public class MatchRange
        {
            public int Index { get; set; }
            public int Length { get; set; }
        }

        public class ReplaceItem<T>
        {
            public int Index { get; set; }
            public int Length { get; set; }
            public T Item { get; set; }
        }

        public static TSource Min<TSource>(this IEnumerable<TSource> v, int inx, int n) => v.Skip(inx).Take(n).Min();


        public static TSource Max<TSource>(this IEnumerable<TSource> v, int inx, int n) => v.Skip(inx).Take(n).Max();


        public static IEnumerable<TSource> TakeEnd<TSource>(this IEnumerable<TSource> source, int count)
        {
            var enumerable = source as TSource[] ?? source.ToArray();
            return enumerable.Skip(enumerable.Length - count);
        }


        public static IEnumerable<TSource> SkipEnd<TSource>(this IEnumerable<TSource> source, int count)
        {
            var enumerable = source as TSource[] ?? source.ToArray();
            return enumerable.Take(enumerable.Length - count);
        }


        public static TSource MinItem<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> f)
        {
            decimal minValue;
            return MinItem(source, f, out minValue);
        }


        public static TSource MinItem<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> f, out decimal min)
        {
            var res = default(TSource);
            min = decimal.MaxValue;

            foreach (var item in source)
            {
                var v = f(item);
                if (v < min)
                {
                    min = v;
                    res = item;
                }
            }

            return res;
        }


        public static TSource MaxItem<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> f)
        {
            decimal maxValue;
            return MaxItem(source, f, out maxValue);
        }


        public static TSource MaxItem<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> f, out decimal maxValue)
        {
            var res = default(TSource);
            maxValue = decimal.MinValue;

            foreach (var item in source)
            {
                var v = f(item);
                if (v > maxValue)
                {
                    maxValue = v;
                    res = item;
                }
            }

            return res;
        }


        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> f)
        {
            foreach (var item in source)
            {
                f(item);
            }
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> f)
        {
            var i = 0;
            foreach (var item in source)
            {
                f(item, i++);
            }
        }


        public static IEnumerable<T> Transform<T>(this IEnumerable<T> source, Func<T, T> f) => source.Select(f);

        public static IEnumerable<T> Transform<T>(this IEnumerable<T> source, Func<T, bool> predicate, Func<T, T> f)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return f(item);
                else
                    yield return item;
            }
        }


        public static IEnumerable<TSource> Iterate<TSource>(this IEnumerable<TSource> source, Action<TSource, TSource> f)
        {
            var enumerable = source as TSource[] ?? source.ToArray();
            var a0 = enumerable.First();

            foreach (var item in enumerable.Skip(1))
            {
                f(a0, item);
                a0 = item;
            }

            return enumerable;
        }


        public static IEnumerable<TSource> Clone<TSource>(this IEnumerable<TSource> source) => source.Select(i => i);


        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source) => source == null || !source.Any();


        public static bool IsNotEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source != null && source.Any();
        }


        public static bool OneOrMore<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).IsNotEmpty();
        }


        public static string ToCsv<T>(this IEnumerable<T> source, string delimiter = ", ")
        {
            if (source == null) return "";
            var enumerable = source as T[] ?? source.ToArray();
            if (enumerable.IsEmpty()) return "";

            var str = new StringBuilder(enumerable.First().ToString());
            enumerable.Skip(1).ForEach(s => str.AppendFormat("{0}{1}", delimiter, s));

            return str.ToString();
        }

        public static IEnumerable<T> ToSingleItemList<T>(this T item) => new[] { item };

        public static bool Contains<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) return false;

            var enumerable = source as T[] ?? source.ToArray();
            if (enumerable.IsEmpty()) return false;

            foreach (var item in enumerable)
            {
                if (predicate(item))
                    return true;
            }

            return false;
        }


        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T item)
        {
            return source.Concat(item.ToSingleItemList());
        }


        public static float Product<T>(this IEnumerable<T> source, Func<T, float> f)
        {
            var p = 1f;
            source.ForEach(i => p *= f(i));
            return p;
        }


        public static double Product<T>(this IEnumerable<T> source, Func<T, double> f)
        {
            double p = 1;
            source.ForEach(i => p *= f(i));
            return p;
        }


        public static int Product<T>(this IEnumerable<T> source, Func<T, int> f)
        {
            var p = 1;
            source.ForEach(i => p *= f(i));
            return p;
        }


        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> f)
        {
            var list = new List<T>();

            foreach (var item in source)
            {
                if (f(item))
                {
                    if (!list.IsNotEmpty()) continue;

                    yield return list;
                    list = new List<T>();
                }
                else
                {
                    list.Add(item);
                }
            }

            if (list.IsNotEmpty())
                yield return list;
        }


        public static void SetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }


        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }

            var value = new TValue();

            dict.Add(key, value);
            return value;
        }

        public static void Set<T>(this List<T> list, params T[] items)
        {
            list.Clear();
            list.AddRange(items);
        }

        public static void Set<T>(this List<T> list, IEnumerable<T> items)
        {
            list.Clear();
            list.AddRange(items);
        }

        public static IEnumerable<MatchRange> Match<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            var listA = a.ToList();
            var listB = b.ToList();

            var na = listA.Count;
            var nb = listB.Count;

            for (var i = 0; i < na; i++)
            {
                var start = i;
                bool ok = true;

                for (var j = 0; j < nb; j++)
                {
                    var inx = i + j;
                    if (inx >= na || !EqualityComparer<T>.Default.Equals(listA[inx], listB[j]))
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    yield return new MatchRange { Index = start, Length = nb };
                }
            }
        }

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, IEnumerable<ReplaceItem<T>> replaceList)
        {
            var replaceItems = replaceList as ReplaceItem<T>[] ?? replaceList.ToArray();
            if (replaceItems.IsEmpty())
            {
                foreach (var item in source)
                    yield return item;

                yield break;
            }

            var rlist = replaceItems.OrderBy(i => i.Index).ToList();

            using (var rPtr = rlist.GetEnumerator())
            {
                rPtr.MoveNext();
                var r = rPtr.Current;

                var list = source.ToList();

                var n = list.Count;

                for (var i = 0; i < n; i++)
                {
                    if (r != null && i == r.Index)
                    {
                        yield return r.Item;
                        i += r.Length - 1;
                        rPtr.MoveNext();
                        r = rPtr.Current;
                    }
                    else
                    {
                        yield return list[i];
                    }
                }
            }
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();

            foreach (var item in source)
            {
                if (seenKeys.Add(keySelector(item)))
                    yield return item;
            }
        }
    }
}
