#region Using

using System;
using System.Collections.Generic;

#endregion

namespace C4rm4x.Tools.TestUtilities
{
    /// <summary>
    /// Utilitiy to store information to be shared between bindings within the same test case
    /// </summary>
    public class TestContext
    {
        private readonly IDictionary<string, object> _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public TestContext()
        {
            _context = new Dictionary<string, object>();
        }

        /// <summary>
        /// Clears the context
        /// </summary>
        public void Cleanup()
        {
            _context.Clear();
        }

        /// <summary>
        /// Pushes the key of type T with the given value
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="key">The identifier</param>
        /// <param name="value">The value</param>
        /// <remarks>If a previous value with same key exists, regarless the type, gets overwritten</remarks>
        public void Push<T>(string key, T value)
        {
            _context[key] = value;
        }

        /// <summary>
        /// Retrieves the value of the given key
        /// </summary>
        /// <param name="key">The identifier</param>
        /// <returns>The value associated with the given key, if exists; null, otherwise</returns>
        public object Get(string key)
        {
            if (_context.ContainsKey(key))
                return _context[key];

            return null;
        }

        /// <summary>
        /// Retrieves the value of the given type as an object of type T
        /// </summary>
        /// <typeparam name="T">The type of the object retrieved (if any)</typeparam>
        /// <param name="key">The identifier</param>
        /// <returns>The value associated with the given key, if exists; null, otherwise</returns>
        /// <exception cref="InvalidCastException">If associated value cannot be cast as an object of type T</exception>
        public T Get<T>(string key)
        {
            return (T)Get(key);
        }
    }
}
