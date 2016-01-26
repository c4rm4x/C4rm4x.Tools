#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Utilities
{
    /// <summary>
    /// Utility methods related to enumerables
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Filters the elements of an System.Collections.IEnumerable based on the specified type
        /// and returns an array of such type
        /// </summary>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">The System.Collections.IEnumerable whose elements to filter</param>
        /// <returns>An array that contains the elements of type TDestination from the input sequence</returns>
        public static TDestination[] ToArray<TDestination>(
            this IEnumerable source)
        {
            source.NotNull(nameof(source));

            return source.OfType<TDestination>().ToArray();
        }

        /// <summary>
        /// Checks whether the enumerable is either null or empty
        /// </summary>
        /// <typeparam name="TElement">Type of the elements of the enumerable</typeparam>
        /// <param name="enumerable">The enumerable</param>
        /// <returns>True whether the enumerable is either null or empty. False otherwise</returns>
        public static bool IsNullOrEmpty<TElement>(
            this IEnumerable<TElement> enumerable)
        {
            return enumerable.IsNull() || !enumerable.Any();
        }

        /// <summary>
        /// Checks whether the value is included in the enumerable
        /// </summary>
        /// <param name="enumerable">The enumerable</param>
        /// <param name="value">The value</param>
        /// <param name="stringComparison">One of the enumeration values that determines how this string and value are compared</param>
        /// <returns>True when value is included in the enumerable; false, otherwise</returns>
        /// <exception cref="ArgumentException">Throws if value is either null or empty</exception>
        public static bool Contains(
            this IEnumerable<string> enumerable,
            string value,
            StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            value.NotNullOrEmpty(nameof(value));

            if (enumerable.IsNullOrEmpty())
                return false;

            return enumerable.Any(x => value.Equals(x, stringComparison));
        }
    }
}
