#region Using

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Auditing.Internals
{
    internal static class TypeExtensions
    {
        public static bool IsGenericEnumerable(
            this Type thisType)
        {
            return !thisType.IsString() &&
                ((thisType.IsGenericType && thisType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                (thisType.GetInterfaces().Any(
                    i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))));
        }

        public static bool IsComplexType(
            this Type thisType)
        {
            return !thisType.IsString() &&
                thisType.IsClass;
        }

        private static bool IsString(
            this Type thisType)
        {
            return thisType == typeof(string);
        }
    }
}
