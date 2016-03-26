#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

#endregion

namespace C4rm4x.Tools.HttpUtilities
{
    /// <summary>
    /// Utility class to consume RESTful services
    /// </summary>
    public static class RESTfulConsumer
    {
        /// <summary>
        /// Returns the Http response from a Get request
        /// </summary>
        /// <param name="domain">Server domain to retrieve information from</param>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="authorizationHeader">The authorization header</param>
        /// <param name="httpMessageHandler">The http message handler (if any)</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static HttpResponseMessage Get(
            string domain,
            string method = "",
            string authorizationHeader = null,
            HttpMessageHandler httpMessageHandler = null,
            params KeyValuePair<string, object>[] parameters)
        {
            return InvokeGet(domain, method, authorizationHeader, httpMessageHandler, parameters);
        }

        /// <summary>
        /// Returns an instance of type T
        /// </summary>
        /// <typeparam name="T">Type of the instance to retrieve</typeparam>
        /// <param name="domain">Server domain to retrieve information from</param>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="authorizationHeader">The authorization header</param>
        /// <param name="httpMessageHandler">The http message handler (if any)</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns an instance of type T if exists</returns>
        public static T Get<T>(
            string domain,
            string method = "",
            string authorizationHeader = null,
            HttpMessageHandler httpMessageHandler = null,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return GetBy<T>(domain, method, authorizationHeader, httpMessageHandler, parameters);
        }

        /// <summary>
        /// Returns all the instances of type T
        /// </summary>
        /// <typeparam name="T">Type of the instance to retrieve</typeparam>
        /// <param name="domain">Server domain to retrieve information from</param>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="authorizationHeader">The authorization header</param>
        /// <param name="httpMessageHandler">The http message handler (if any)</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the list of all the instances of type T if any</returns>
        public static IEnumerable<T> GetAll<T>(
            string domain,
            string method = "",
            string authorizationHeader = null,
            HttpMessageHandler httpMessageHandler = null,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return GetBy<List<T>>(domain, method, authorizationHeader, httpMessageHandler, parameters);
        }

        private static TResult GetBy<TResult>(
            string domain,
            string method,
            string authorizationHeader,
            HttpMessageHandler httpMessageHandler,
            params KeyValuePair<string, object>[] parameters)
        {
            return InvokeGet(domain, method, authorizationHeader, httpMessageHandler, parameters)
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsAsync<TResult>()
                .Result;
        }

        /// <summary>
        /// Sends a request of type T as a Post
        /// </summary>
        /// <typeparam name="T">Type of the request</typeparam>
        /// <param name="objectToSend">Object of type T to be sent</param>
        /// <param name="domain">Server domain to send information to</param>
        /// <param name="method">Method to execute to send information</param>
        /// <param name="authorizationHeader">The authorization header</param>
        /// <param name="httpMessageHandler">The http message handler (if any)</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static HttpResponseMessage Post<T>(
            T objectToSend,
            string domain,
            string method = "",
            string authorizationHeader = null,
            HttpMessageHandler httpMessageHandler = null,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return InvokeMethod(domain, client =>
            {
                return client
                    .PostAsJsonAsync(BuildMethodName(method, parameters), objectToSend)
                    .Result;
            }, authorizationHeader, httpMessageHandler);
        }

        /// <summary>
        /// Sends a request of type T as a Put
        /// </summary>
        /// <typeparam name="T">Type of the request</typeparam>
        /// <param name="objectToSend">Object of type T to be sent</param>
        /// <param name="domain">Server domain to send information to</param>
        /// <param name="method">Method to execute to send information</param>
        /// <param name="authorizationHeader">The authorization header</param>
        /// <param name="httpMessageHandler">The http message handler (if any)</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static HttpResponseMessage Put<T>(
            T objectToSend,
            string domain,
            string method = "",
            string authorizationHeader = null,
            HttpMessageHandler httpMessageHandler = null,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return InvokeMethod(domain, client =>
            {
                return client
                    .PutAsJsonAsync(BuildMethodName(method, parameters), objectToSend)
                    .Result;
            }, authorizationHeader, httpMessageHandler);
        }

        /// <summary>
        /// Returns the Http response from a Delete request
        /// </summary>
        /// <param name="domain">Server domain to delete information from</param>
        /// <param name="method">Method to execute to delete the info</param>
        /// <param name="authorizationHeader">The authorization header</param>
        /// <param name="httpMessageHandler">The http message handler (if any)</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static HttpResponseMessage Delete(
            string domain,
            string method = "",
            string authorizationHeader = null,
            HttpMessageHandler httpMessageHandler = null,
            params KeyValuePair<string, object>[] parameters)
        {
            return InvokeMethod(domain, client =>
            {
                return client
                    .DeleteAsync(BuildMethodName(method, parameters))
                    .Result;
            }, authorizationHeader, httpMessageHandler);
        }

        private static string BuildMethodName(
            string method,
            KeyValuePair<string, object>[] parameters)
        {
            var queryString = string
                .Join("&", parameters.Select(p => "{0}={1}".AsFormat(p.Key, p.Value)));

            if (queryString.IsNullOrEmpty())
                return method;

            return "{0}?{1}".AsFormat(method, queryString);
        }

        private static HttpResponseMessage InvokeGet(
            string domain,
            string method,
            string authorizationHeader,
            HttpMessageHandler httpMessageHandler,
            KeyValuePair<string, object>[] parameters)
        {
            return InvokeMethod(domain, client =>
            {
                return client
                    .GetAsync(BuildMethodName(method, parameters))
                    .Result;
            }, authorizationHeader, httpMessageHandler);
        }

        private static HttpResponseMessage InvokeMethod(
            string domain,
            Func<HttpClient, HttpResponseMessage> method,
            string authorizationHeader,
            HttpMessageHandler httpMessageHandler,
            bool addApplicationJsonHeader = true)
        {
            using (var client = GetHttpClient(httpMessageHandler))
            {
                client.BaseAddress = new Uri(domain);

                SetsHeaders(client, addApplicationJsonHeader, authorizationHeader);

                return method(client);
            }
        }

        private static HttpClient GetHttpClient(
            HttpMessageHandler httpMessageHandler)
        {
            return httpMessageHandler.IsNull()
                ? new HttpClient()
                : new HttpClient(httpMessageHandler);
        }

        private static void SetsHeaders(
            HttpClient client,
            bool addApplicationJsonHeader,
            string jsonWebToken = null)
        {
            SetAcceptHeader(client, addApplicationJsonHeader);
            SetAuthorizationHeader(client, jsonWebToken);
        }

        private static void SetAcceptHeader(
            HttpClient client, 
            bool addApplicationJsonHeader)
        {
            const string ApplicationJson = "application/json";

            client.DefaultRequestHeaders.Accept.Clear();

            if (!addApplicationJsonHeader) return;

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(ApplicationJson));
        }

        private static void SetAuthorizationHeader(
            HttpClient client, 
            string authorizationHeader)
        {
            if (authorizationHeader.IsNullOrEmpty()) return;

            client.DefaultRequestHeaders.Authorization =
                AuthenticationHeaderValue.Parse(authorizationHeader);
        }
    }
}
