#region Using

using C4rm4x.Tools.Auditing.Comparators;
using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace C4rm4x.Tools.Auditing.Internals
{
    internal static class ComparisonConfigurationExtensions
    {
        public static IEnumerable<PropertyInfo> GetPropertiesFor(
            this ComparisonConfiguration config,
            Type type)
        {
            return type.GetProperties(config.GetPropertyBindings());
        }

        private static BindingFlags GetPropertyBindings(
            this ComparisonConfiguration config)
        {
            var bindings = BindingFlags.Instance | BindingFlags.Public;

            if (!config.IgnorePrivateProperties)
                bindings |= BindingFlags.NonPublic;

            if (config.IgnoreInheritedProperties)
                bindings |= BindingFlags.DeclaredOnly;

            return bindings;
        }

        internal static IComparator GetComparatorFor(
            this ComparisonConfiguration config,
            Type type)
        {
            return (from c in config.Comparators
                   let comparator = c()
                   let comparatorType = comparator.GetType()
                   where IsGenericComparator(comparatorType, type) 
                   select comparator)
                   .FirstOrDefault();
        }

        private static bool IsGenericComparator(
            Type comparatorType, 
            Type type)
        {
            var @interface = comparatorType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType 
                && i.GetGenericTypeDefinition() == typeof(IComparator<>));

            if (@interface.IsNull()) return false;

            return @interface.GetGenericArguments()[0] == type;
        }
    }
}
