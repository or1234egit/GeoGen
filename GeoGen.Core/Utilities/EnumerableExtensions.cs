﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeoGen.Core.Configurations;

namespace GeoGen.Core.Utilities
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks if the enumerable has no element.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>true, if the enumerable is empty; false otherwise</returns>
        public static bool Empty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return !enumerable.Any();
        }

        /// <summary>
        /// Creates a single-element enumerable containing the given item.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="item">The item</param>
        /// <returns>The enumerable with count 1.</returns>
        public static IEnumerable<T> SingleItemAsEnumerable<T>(this T item)
        {
            yield return item;
        }

        /// <summary>
        /// Converts an enumerable to <see cref="HashSet{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>The hash set of the enumerable's items.</returns>
        public static HashSet<T> ToSet<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return new HashSet<T>(enumerable);
        }

        public static T Min<T>(this IEnumerable<T> enumerable, IComparer<T> comparer)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            var y = default(T);

            if (y == null)
            {
                foreach (var x in enumerable)
                {
                    if (x != null && (y == null || comparer.Compare(x, y) < 0))
                    {
                        y = x;
                    }
                }

                return y;
            }

            var flag = false;

            foreach (var x in enumerable)
            {
                if (flag)
                {
                    if (comparer.Compare(x, y) < 0)
                        y = x;
                }
                else
                {
                    y = x;
                    flag = true;
                }
            }

            if (flag)
                return y;

            throw new InvalidOperationException("No elements.");
        }

        public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        public static ConfigurationObjectsMap ToObjectsMap(this IEnumerable<ConfigurationObject> objects)
        {
            return new ConfigurationObjectsMap(objects);
        }
    }
}