#region Using

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Utilities
{
    /// <summary>
    /// Utility methods to check conditions that must be true
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Verifies the argument satifies the condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument">The argument</param>
        /// <param name="condition">Contidion to be satisfied</param>
        /// <param name="message">Message to include in exception if this must be thrown</param>
        /// <exception cref="ArgumentException">Throws if argument does not satisfy the condition</exception>
        public static void Must<T>(
            this T argument,
            Func<T, bool> condition,
            string message)
        {
            if (!condition(argument))
                throw new ArgumentException(message);
        }

        /// <summary>
        /// Verifies the argument is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument">The argument</param>
        /// <param name="argumentName">Argument name</param>
        /// <exception cref="ArgumentException">Throws if argument is null</exception>
        public static void NotNull<T>(
            this T argument,
            string argumentName)
        {
            argument.Must(x => x.IsNotNull(),
                "{0} cannot be null".AsFormat(argumentName));
        }

        /// <summary>
        /// Verifies the string is neither null nor empty
        /// </summary>
        /// <param name="argument">The string</param>
        /// <param name="argumentName">Argument name</param>
        /// <exception cref="ArgumentException">Throws if argument is either null or empty</exception>
        public static void NotNullOrEmpty(
            this string argument,
            string argumentName)
        {
            argument.Must(x => !x.IsNullOrEmpty(),
                "{0} cannot be either null or empty".AsFormat(argumentName));
        }

        /// <summary>
        /// Verifies the collection is neither null nor empty
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="argument">The collection</param>
        /// <param name="argumentName">ArgmentName</param>
        /// <exception cref="ArgumentException">Throws if argument is either null or empty</exception>
        public static void NotNullOrEmpty<T>(
            this IEnumerable<T> argument,
            string argumentName)
            where T : class
        {
            argument.NotNull(argumentName);
            argument.Must(x => x.Any(),
                "{0} cannot be empty".AsFormat(argumentName));
        }

        /// <summary>
        /// Verifies the type is a valid System.Enum type
        /// </summary>
        /// <param name="thisType">The type</param>
        public static void IsEnum(
            this Type thisType)
        {
            thisType.Must(t => t.IsEnum,
                "{0} is not a valid System.Enum type".AsFormat(thisType.FullName));
        }

        /// <summary>
        /// Verifies the type is compatible with TGiven
        /// </summary>
        /// <typeparam name="TGiven">Type to check compatiblity against</typeparam>
        /// <param name="thisType">The type</param>
        public static void Is<TGiven>(
            this Type thisType)
        {
            thisType.Must(t => typeof(TGiven).IsAssignableFrom(t),
                "{0} must be compatible with given type {1}".AsFormat(
                    thisType.FullName, typeof(TGiven).FullName));
        }
    }
}
