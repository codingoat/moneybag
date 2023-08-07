using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace ZUtils
{
    public static class EnumerableExtensions
    {
        /// <returns>The index of the first matching element, or -1 if no matches are found.</returns>
        public static int FirstIndex<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> condition)
        {
            var index = 0;
            foreach (var item in items)
            {
                if (condition.Invoke(item)) return index;
                index++;
            }

            return -1;
        }

        public static T PickRandom<T>(this IEnumerable<T> list) => list.ElementAt(Random.Range(0, list.Count()));

        public static T MinElement<T>(this IEnumerable<T> list, Func<T, float> orderBy) =>
            list.Aggregate((aggr, next) => orderBy(aggr) < orderBy(next) ? aggr : next);

        public static T MaxElement<T>(this IEnumerable<T> list, Func<T, float> orderBy) =>
            list.Aggregate((aggr, next) => orderBy(aggr) > orderBy(next) ? aggr : next);

        public static int MinIndex<T>(this IEnumerable<T> list, Func<T, float> orderBy)
        {
            int result = 0;
            var arr = list.ToArray();
            for (int i = 0; i < arr.Length; i++) result = orderBy(arr[i]) < orderBy(arr[result]) ? i : result;
            return result;
        }

        public static int MaxIndex<T>(this IEnumerable<T> list, Func<T, float> orderBy)
        {
            int result = 0;
            var arr = list.ToArray();
            for (int i = 0; i < arr.Length; i++) result = orderBy(arr[i]) > orderBy(arr[result]) ? i : result;
            return result;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> function)
        {
            foreach (T element in array) function(element);
            return array;
        }

        ///<summary>Does <paramref name="source"/> contain any of the values found in <paramref name="values"/>?</summary>
        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> values) =>
            values.Any(source.Contains);

        ///<summary>Returns a sorted copy of the enumerable</summary>
        public static List<T> Sorted<T>(this IEnumerable<T> array)
        {
            List<T> sorted = new List<T>(array);
            sorted.Sort();
            return sorted;
        }
    }
}