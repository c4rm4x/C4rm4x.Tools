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
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static HttpResponseMessage Get(
            string domain,
            string method = "",
            params KeyValuePair<string, object>[] parameters)
        {
            return InvokeGet(domain, method, parameters, enforceSuccess: false);
        }

        /// <summary>
        /// Returns an instance of type T
        /// </summary>
        /// <typeparam name="T">Type of the instance to retrieve</typeparam>
        /// <param name="domain">Server domain to retrieve information from</param>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns an instance of type T if exists</returns>
        public static T Get<T>(
            string domain,
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return GetBy<T>(domain, method, parameters);
        }

        /// <summary>
        /// Returns all the instances of type T
        /// </summary>
        /// <typeparam name="T">Type of the instance to retrieve</typeparam>
        /// <param name="domain">Server domain to retrieve information from</param>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the list of all the instances of type T if any</returns>
        public static IEnumerable<T> GetAll<T>(
            string domain,
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return GetBy<List<T>>(domain, method, parameters);
        }

        private static TResult GetBy<TResult>(
            string domain,
            string method,
            params KeyValuePair<string, object>[] parameters)
        {
            return InvokeGet(domain, method, parameters)
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
        /// <param name="parameters">Parameters to include as part of query string</param>
        public static void Post<T>(
            T objectToSend,
            string domain,
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            InvokeMethod(domain, client =>
            {
                return client
                    .PostAsJsonAsync(BuildMethodName(method, parameters), objectToSend)
                    .Result;
            });
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
            KeyValuePair<string, object>[] parameters,
            bool enforceSuccess = true)
        {
            return InvokeMethod(domain, client =>
            {
                return client
                    .GetAsync(BuildMethodName(method, parameters))
                    .Result;
            }, enforceSuccess: enforceSuccess);
        }

        private static HttpResponseMessage InvokeMethod(
            string domain,
            Func<HttpClient, HttpResponseMessage> method,
            bool addApplicationJsonHeader = true,
            bool enforceSuccess = true)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domain);

                SetsHeaders(client, addApplicationJsonHeader);

                var response = method(client);

                return enforceSuccess
                    ? response.EnsureSuccessStatusCode()
                    : response;
            }
        }

        private static void SetsHeaders(
            HttpClient client,
            bool addApplicationJsonHeader)
        {
            const string ApplicationJson = "application/json";

            client.DefaultRequestHeaders.Accept.Clear();

            if (!addApplicationJsonHeader) return;

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(ApplicationJson));
        }
    }
}
