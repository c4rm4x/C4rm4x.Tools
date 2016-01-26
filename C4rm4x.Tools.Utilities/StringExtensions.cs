namespace C4rm4x.Tools.Utilities
{
    /// <summary>
    /// Utility methods related to strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether the argument is null or empty
        /// </summary>
        /// <param name="str">The argument</param>
        /// <returns>True if argument is either null or empty. False otherwise</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Replaces the format item in a specified string with the string representation
        /// of a corresponding object in a specified array.
        /// </summary>
        /// <param name="format">The format</param>
        /// <param name="args">Optional arguments</param>
        /// <returns>
        /// A copy of format in which the format items have been replaced by the string
        ///  representation of the corresponding objects in args
        /// </returns>
        public static string AsFormat(
            this string format,
            params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
