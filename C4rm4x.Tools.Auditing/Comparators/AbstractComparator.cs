namespace C4rm4x.Tools.Auditing.Comparators
{
    /// <summary>
    /// Base implementation of IComparator
    /// </summary>
    /// <typeparam name="T">The type to compare</typeparam>
    public abstract class AbstractComparator<T> :
        IComparator<T>
    {
        /// <summary>
        /// Compares both instances
        /// </summary>
        /// <param name="original">The original</param>
        /// <param name="current">The current</param>
        /// <returns>True if both instances match; false, otherwise</returns>
        public bool Match(
            object original, 
            object current)
        {
            return Match((T)original, (T)current);
        }

        /// <summary>
        /// Compares both instances of type T
        /// </summary>
        /// <param name="original">The original</param>
        /// <param name="current">The current</param>
        /// <returns>True if both instances match; false, otherwise</returns>
        public abstract bool Match(
            T original,
            T current);
    }
}
