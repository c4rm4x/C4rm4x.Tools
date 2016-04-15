#region Using

using C4rm4x.Tools.Auditing.Comparators;
using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Auditing
{
    /// <summary>
    /// Configure your comparision rules
    /// </summary>
    public class ComparisonConfiguration
    {
        /// <summary>
        /// Gets whether or not should ignore inherited properties
        /// </summary>
        public bool IgnoreInheritedProperties { get; private set; }

        /// <summary>
        /// Gets whether or not should ignore private and protected properties
        /// </summary>
        public bool IgnorePrivateProperties { get; private set; }

        /// <summary>
        /// Gets whether or not null objects are considered equal
        /// </summary>
        public bool NullObjectsAreEquals { get; private set; }

        /// <summary>
        /// Gets the type that should be compared using their custom comparators
        /// </summary>
        public IEnumerable<Func<IComparator>> Comparators { get; private set; } =
            new List<Func<IComparator>>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ignoreInheritedProperties">Compare only declared properties (true by default)</param>
        /// <param name="ignorePrivateProperties">Compare only public properties (true by default)</param>
        /// <param name="nullObjectsAreEquals">Determine whether or not null objects are equals</param>
        /// <param name="comparators">Custom comparators</param>
        public ComparisonConfiguration(
            bool ignoreInheritedProperties = true,
            bool ignorePrivateProperties = true,
            bool nullObjectsAreEquals = true,
            params Func<IComparator>[] comparators)
        {
            IgnoreInheritedProperties = ignoreInheritedProperties;
            IgnorePrivateProperties = ignorePrivateProperties;
            NullObjectsAreEquals = nullObjectsAreEquals;

            SetComparators(comparators);
        }

        private void SetComparators(params Func<IComparator>[] comparators)
        {
            if (comparators.IsNullOrEmpty()) return;

            Comparators = comparators.ToList();
        }
    }
}
