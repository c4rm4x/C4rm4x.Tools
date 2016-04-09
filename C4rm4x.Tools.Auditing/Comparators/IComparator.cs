namespace C4rm4x.Tools.Auditing.Comparators
{
    /// <summary>
    /// Determines whether or not both instances
    /// </summary>
    public interface IComparator
    {
        /// <summary>
        /// Compares both instances
        /// </summary>
        /// <param name="original">The original</param>
        /// <param name="current">The current</param>
        /// <returns>True if both instances match; false, otherwise</returns>
        bool Match(
            object original,
            object current);
    }

    /// <summary>
    /// Determines whether or not both instances of type T match
    /// </summary>
    /// <typeparam name="T">The type to compare</typeparam>
    public interface IComparator<T> : IComparator
    {
        /// <summary>
        /// Compares both instances of type T
        /// </summary>
        /// <param name="original">The original</param>
        /// <param name="current">The current</param>
        /// <returns>True if both instances match; false, otherwise</returns>
        bool Match(
            T original, 
            T current);
    }
}
