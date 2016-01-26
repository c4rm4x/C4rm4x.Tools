#region Using

using System;

#endregion

namespace C4rm4x.Tools.Utilities
{
    /// <summary>
    /// Utility methods related to System.Enum 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts the string representation of the name of one or
        ///  more enumerated constants to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">Type of enum</typeparam>
        /// <param name="value">Value to be parsed</param>
        /// <param name="ignoreCase">Indicates whether or not to ignore case while parsing</param>
        /// <returns>Equivalent enum value represented by the string argument</returns>
        /// <exception cref="ArgumentException">TEnum is not an System.Enum</exception>
        /// <exception cref="OverflowException">Value is outsite the range of the underlying type</exception>       
        public static TEnum GetEnumValue<TEnum>(
            this string value,
            bool ignoreCase = false)
            where TEnum : struct
        {
            typeof(TEnum).IsEnum();
            value.NotNullOrEmpty(nameof(value));

            return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
        }

        /// <summary>
        /// Converts the numeric value of one or more enumerated constants 
        /// to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="TEnum">Type of enum</typeparam>
        /// <param name="value">Value to be parsed</param>
        /// <param name="ignoreCase">Indicates whether or not to ignore case while parsing</param>
        /// <returns>Equivalent enum value represented by the int argument</returns>
        /// <exception cref="ArgumentException">TEnum is not an System.Enum</exception>
        /// <exception cref="OverflowException">Value is outsite the range of the underlying type</exception>       
        public static TEnum GetEnumValue<TEnum>(
            this int value,
            bool ignoreCase = false)
            where TEnum : struct
        {
            return value.ToString().GetEnumValue<TEnum>(ignoreCase);
        }
    }
}
