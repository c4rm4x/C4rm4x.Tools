#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace C4rm4x.Tools.Utilities
{
    /// <summary>
    /// Utility methods Assembly searchs related
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Returns the list of all the assemblies that have been loaded
        /// into the execution context of appDomain that fulfill the predicate
        /// </summary>
        /// <param name="appDomain">The app domain</param>
        /// <param name="predicate">Predicate</param>
        /// <returns>The list of all assemblyes already loaded into the execution context of app domain that fulfill the predicate</returns>
        public static IEnumerable<Assembly> GetAssemblies(
            this AppDomain appDomain,
            Func<Assembly, bool> predicate)
        {
            appDomain.NotNull(nameof(appDomain));
            predicate.NotNull(nameof(predicate));

            return appDomain
                .GetAssemblies()
                .Where(predicate)
                .ToList();
        }

        /// <summary>
        /// Returns the assembly whose name is the specified one
        /// </summary>
        /// <param name="appDomain">The app domain</param>
        /// <param name="assemblyName">The assembly name</param>
        /// <returns>Returns the assembly with the specified name that has been loaded into the execution context of app domain</returns>
        /// <exception cref="InvalidOperationException">When no assembly with such name has been loaded</exception>
        public static Assembly GetAssemblyByName(
            this AppDomain appDomain,
            string assemblyName)
        {
            assemblyName.NotNullOrEmpty(nameof(assemblyName));

            return appDomain
                .GetAssemblies(a => a.GetName().Name.Equals(assemblyName))
                .First();
        }
    }
}
