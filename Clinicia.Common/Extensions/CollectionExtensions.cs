using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinicia.Common.Extensions
{
    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// </summary>
        /// <param name="source">Collection</param>
        /// <param name="item">Item to check and add</param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }

        public static bool ContainsSameValues(this ICollection<int> firstCollection, ICollection<int> secondCollection)
        {
            if (firstCollection.IsNullOrEmpty() && secondCollection.IsNullOrEmpty())
            {
                return true;
            }

            if (firstCollection != null && secondCollection != null && firstCollection.Count == secondCollection.Count)
            {
                return firstCollection.OrderBy(x => x).SequenceEqual(secondCollection.OrderBy(x => x));
            }

            return false;
        }
    }
}