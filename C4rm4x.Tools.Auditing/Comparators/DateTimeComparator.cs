#region Using

using System;

#endregion

namespace C4rm4x.Tools.Auditing.Comparators
{
    /// <summary>
    /// Compare to instances of type DateTime based on format
    /// </summary>
    public class DateTimeComparator : 
        AbstractComparator<DateTime>
    {
        /// <summary>
        /// Default format if none is specified
        /// </summary>
        public const string DefaultFormat = "yyyyMMdd HH:mm";

        /// <summary>
        /// Gets the format to be applied to compare the two instances
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="format">The format</param>
        public DateTimeComparator(
            string format = DefaultFormat)
        {
            Format = format;
        }

        /// <summary>
        /// Compares the two instances of DateTime based on format
        /// </summary>
        /// <param name="original">The original value</param>
        /// <param name="current">The current value</param>
        /// <returns>True when both instances match based on format; false, otherwise</returns>
        public override bool Match(
            DateTime original, 
            DateTime current)
        {
            return original.ToString(Format).Equals(
                current.ToString(Format));
        }
    }
}
