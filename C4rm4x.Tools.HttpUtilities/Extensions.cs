#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Linq;
using System.Web;

#endregion

namespace C4rm4x.Tools.HttpUtilities
{
    /// <summary>
    /// Extensions methods HttpRequestBase related
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets Visitor's IP Address
        /// </summary>
        /// <param name="request">The current request</param>
        /// <returns>The IP Address</returns>
        public static string GetIPAddress(this HttpRequestBase request)
        {
            request.NotNull("request");

            var httpXForwarderFor = request.GetServerVariable("HTTP_X_FORWARDED_FOR");

            return httpXForwarderFor.IsNullOrEmpty()
                ? request.GetServerVariable("REMOTE_ADDR")
                : httpXForwarderFor
                    .Split(new[] { ",", ":" }, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Gets Visitor's Origin (if any)
        /// </summary>
        /// <param name="request">The current request</param>
        /// <returns>The origin</returns>
        public static string GetOrigin(this HttpRequestBase request)
        {
            request.NotNull("request");

            return request.GetServerVariable("ORIGIN");
        }

        /// <summary>
        /// Gets the server variable (if any)
        /// </summary>
        /// <param name="request">The current request</param>
        /// <param name="variableName">The variable name</param>
        /// <returns></returns>
        public static string GetServerVariable(
            this HttpRequestBase request, string variableName)
        {
            return request.ServerVariables[variableName];
        }
    }
}
