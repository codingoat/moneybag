using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZUtils
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = Random.Range(0, n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }

        public static IList<T> Shuffled<T>(this IList<T> list)
        {
            var newList = list.ToList(); // copy
            newList.Shuffle();
            return newList;
        }
    }
}