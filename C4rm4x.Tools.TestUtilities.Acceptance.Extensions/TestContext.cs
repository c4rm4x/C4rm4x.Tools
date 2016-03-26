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
        /// Gets or sets the value with the given key
        /// </summary>
        /// <param name="key">The identifier</param>
        /// <returns>The element with the specified key</returns>
        /// <exception cref="ArgumentNullException">If the key is null</exception>
        /// <exception cref="KeyNotFoundException">Key cannot be found</exception>
        public object this[string key]
        {
            get
            {
                return _context[key];
            }
            set
            {
                _context[key] = value;
            }
        }

        /// <summary>
        /// Retrieves the value of the given type as an object of type T
        /// </summary>
        /// <typeparam name="T">The type of the object retrieved (if any)</typeparam>
        /// <param name="key">The identifier</param>
        /// <returns>The value associated with the given key (ifany)</returns>
        /// <exception cref="ArgumentNullException">If the key is null</exception>
        /// <exception cref="KeyNotFoundException">Key cannot be found</exception>
        /// <exception cref="InvalidCastException">If associated value cannot be cast as an object of type T</exception>
        public T Get<T>(string key)
        {
            return (T)this[key];
        }
    }
}
