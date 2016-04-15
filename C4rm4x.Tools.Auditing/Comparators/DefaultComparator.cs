#region Using

using C4rm4x.Tools.Utilities;

#endregion

namespace C4rm4x.Tools.Auditing.Comparators
{
    /// <summary>
    /// Compare two instances of type object
    /// </summary>
    public class DefaultComparator : 
        IComparator<object>
    {
        /// <summary>
        /// Gets whether or not null objects are considered equals
        /// </summary>
        public bool NullEquality { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nullEquality">Are null objects equals?</param>
        public DefaultComparator(bool nullEquality)
        {
            NullEquality = nullEquality;
        }

        /// <summary>
        /// Compares the two instances of object
        /// </summary>
        /// <param name="original">The original value</param>
        /// <param name="current">The current value</param>
        /// <returns>True when both instances match based on value; false, otherwise</returns>
        public bool Match(
            object original, 
            object current)
        {
            if (original.IsNull() && current.IsNull())
                return NullEquality;

            if (original.IsNull() || current.IsNull())
                return false;

            return original.Equals(current);
        }
    }
}
