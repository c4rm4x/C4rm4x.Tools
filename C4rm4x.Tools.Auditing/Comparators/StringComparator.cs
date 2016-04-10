#region Using

using C4rm4x.Tools.Utilities;
using System;

#endregion

namespace C4rm4x.Tools.Auditing.Comparators
{
    /// <summary>
    /// Compare two instances of type string based on comparison options
    /// </summary>
    public class StringComparator :
        AbstractComparator<string>
    {
        /// <summary>
        /// Gets the string comparison to use to compare both instances
        /// </summary>
        public StringComparison StringComparison { get; private set; }

        /// <summary>
        /// Gets whether or not null strings are considered equals
        /// </summary>
        public bool NullEquality { get; private set; }

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="stringComparison">The options</param>
        /// <param name="nullEquality">Are null strings equals?</param>
        public StringComparator(
            StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase,
            bool nullEquality = true)
        {
            StringComparison = stringComparison;
            NullEquality = nullEquality;
        }

        /// <summary>
        /// Compares the two instances of string based on StringComparison
        /// </summary>
        /// <param name="original">The original value</param>
        /// <param name="current">The current value</param>
        /// <returns>True when both instances match based on StringComparison; false, otherwise</returns>
        public override bool Match(
            string original, 
            string current)
        {
            if (original.IsNull() && current.IsNull())
                return NullEquality;

            if (original.IsNull() || current.IsNull())
                return false;

            return original.Equals(current, StringComparison);
        }
    }
}
