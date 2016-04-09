#region Using

using C4rm4x.Tools.Auditing.Internals;
using C4rm4x.Tools.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace C4rm4x.Tools.Auditing
{
    /// <summary>
    /// Class responsible to calculate audit logs between two objects of the same type
    /// </summary>
    public class AuditTrails
    {
        /// <summary>
        /// Gets the comparison configuration used to check the what changes
        /// have been mande (if any) between the two instances
        /// </summary>
        public ComparisonConfiguration Configuration { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public AuditTrails(
            ComparisonConfiguration configuration = null)
        {
            Configuration = configuration ?? new ComparisonConfiguration();
        }

        /// <summary>
        /// Calculates all the changes between an original object
        /// and the current one
        /// </summary>
        /// <typeparam name="T">Type of the objects to compare</typeparam>
        /// <param name="original">The original object</param>
        /// <param name="current">The current object</param>
        /// <returns>The list of all traces represeting the original values that have changed</returns>
        /// <exception cref="ArgumentException">When either original or current are null</exception>
        /// <exception cref="ArgumentException">When type T is not complex</exception>
        public IEnumerable<Trace> Log<T>(
            T original,
            T current)
            where T : class
        {
            original.NotNull(nameof(original));
            current.NotNull(nameof(current));
            typeof(T).Must(t => t.IsComplexType(), "The object type must be complex");

            foreach (var property in Configuration.GetPropertiesFor(typeof(T)))
                if (!Match(original, current, property))
                    yield return new Trace(property, original);
        }

        private bool Match(
            object original,
            object current,
            PropertyInfo propertyInfo)
        {
            return Match(
                propertyInfo.GetValue(original, null),
                propertyInfo.GetValue(current, null),
                propertyInfo.PropertyType);
        }

        private bool Match(
            object original,
            object current,
            Type type)
        {
            if (type.IsGenericEnumerable())
                return EnumerableMatch(
                    original as IEnumerable,
                    current as IEnumerable,
                    type.GetGenericArguments()[0]);

            if (type.IsComplexType())
                return ComplexTypeMatch(original, current, type);

            return ValueMatch(original, current, type);
        }

        private bool EnumerableMatch(
            IEnumerable original,
            IEnumerable current,
            Type type)
        {
            original.NotNull(nameof(original));
            current.NotNull(nameof(current));

            var originalEnumerable = original.ToArray<object>();
            var currentEnumerable = current.ToArray<object>();

            if (originalEnumerable.Length != currentEnumerable.Length)
                return false;

            return originalEnumerable.All(o => currentEnumerable.Any(c => Match(o, c, type))) &&
                currentEnumerable.All(c => originalEnumerable.Any(o => Match(c, o, type)));
        }

        private bool ComplexTypeMatch(
            object original,
            object current,
            Type type)
        {
            var comparator = Configuration.GetComparatorFor(type);

            if (comparator.IsNotNull())
                return comparator.Match(original, current);

            foreach (var property in Configuration.GetPropertiesFor(type))
                if (!Match(original, current, property))
                    return false;

            return true;
        }

        private bool ValueMatch(
            object originalValue,
            object currentValue,
            Type type)
        {
            var comparator = Configuration.GetComparatorFor(type);

            if (comparator.IsNotNull())
                return comparator.Match(originalValue, currentValue);

            return originalValue == currentValue;
        }
    }
}
