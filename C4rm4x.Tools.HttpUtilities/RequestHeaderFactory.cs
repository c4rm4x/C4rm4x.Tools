#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Net.Http.Headers;

#endregion

namespace C4rm4x.Tools.HttpUtilities
{
    /// <summary>
    /// Utiltities to add headers into request headers collection
    /// </summary>
    public static class RequestHeaderFactory
    {
        /// <summary>
        /// Adds authorization header as part of request headers
        /// </summary>
        /// <param name="authorizationHeader">The authorization header value</param>
        /// <returns>The action to add the authorization header as part of the request headers</returns>
        public static Action<HttpRequestHeaders> AddAuthorization(
            string authorizationHeader)
        {
            authorizationHeader.NotNullOrEmpty(nameof(authorizationHeader));

            return headers =>
                headers.Authorization = AuthenticationHeaderValue.Parse(authorizationHeader);
        }
    }
}
