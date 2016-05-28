﻿#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

#endregion

namespace C4rm4x.Tools.Security.Acl
{
    /// <summary>
    /// Acl credentials retriever is responsible to retrieve
    /// the pair identifier/secret from an base-64 authorization header
    /// </summary>
    public class AclClientCredentialsRetriever
    {
        /// <summary>
        /// Tries to convert the authorization header from the request (if any)
        /// into an instance of AclClientCredentials
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="credentials">The credentials</param>
        /// <returns>True when a valid base-64 basic authorization is found; false, otherwise</returns>
        public bool TryParse(
            HttpRequestMessage request,
            out AclClientCredentials credentials)
        {
            request.NotNull(nameof(request));

            credentials = null;

            var authorizationAsBase64 = GetAuthorization(request.Headers);

            var authorization = ExtractCredentials(authorizationAsBase64);

            if (authorization.Length != 2)
                return false;

            credentials = new AclClientCredentials(
                authorization[0],
                authorization[1]);

            return true;
        }

        private static string GetAuthorization(
            HttpRequestHeaders headers)
        {
            return headers.Authorization.ToString();
        }

        private static string[] ExtractCredentials(
            string authorizationAsBase64)
        {
            if (authorizationAsBase64.IsNullOrEmpty())
                return new string[] { };

            return authorizationAsBase64
                .Replace("Basic ", string.Empty)
                .FromBase64()
                .Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
