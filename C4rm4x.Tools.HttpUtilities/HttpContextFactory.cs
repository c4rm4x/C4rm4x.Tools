#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Web;

#endregion

namespace C4rm4x.Tools.HttpUtilities
{
    /// <summary>
    /// This class is only meant to be used to facilitate unit testing
    /// as HttpContext is a sealed class and no mocking framework can mock those
    /// </summary>
    public class HttpContextFactory
    {
        private static HttpContextBase _context;

        /// <summary>
        ///  Gets the System.Web.HttpContext object for the current HTTP request
        /// </summary>
        public static HttpContextBase Current
        {
            get
            {
                if (_context.IsNotNull())
                    return _context;

                if (HttpContext.Current.IsNull())
                    throw new InvalidOperationException("HttpContext not available");

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        /// <summary>
        /// Sets context for unit testing
        /// </summary>
        /// <param name="context">The context</param>
        public static void SetCurrentContext(HttpContextBase context)
        {
            _context = context;
        }
    }
}
