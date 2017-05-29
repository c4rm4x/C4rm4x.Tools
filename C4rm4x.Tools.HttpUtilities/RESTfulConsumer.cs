#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
        /// <param name="headers">Include headers to request</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> GetAsync(
            string domain,
            string method = "",
            Action<HttpRequestHeaders> headers = null,
            params KeyValuePair<string, object>[] parameters)
        {
            return await InvokeGetAsync(domain, method, headers, parameters);
        }

        /// <summary>
        /// Returns an instance of type T
        /// </summary>
        /// <typeparam name="T">Type of the instance to retrieve</typeparam>
        /// <param name="domain">Server domain to retrieve information from</param>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="headers">Include headers to request</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns an instance of type T if exists</returns>
        public static async Task<T> GetAsync<T>(
            string domain,
            string method = "",
            Action<HttpRequestHeaders> headers = null,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return await GetByAsync<T>(domain, method, headers, parameters);
        }

        /// <summary>
        /// Returns all the instances of type T
        /// </summary>
        /// <typeparam name="T">Type of the instance to retrieve</typeparam>
        /// <param name="domain">Server domain to retrieve information from</param>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="headers">Include headers to request</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the list of all the instances of type T if any</returns>
        public static async Task<IEnumerable<T>> GetAllAsync<T>(
            string domain,
            string method = "",
            Action<HttpRequestHeaders> headers = null,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return await GetByAsync<List<T>>(domain, method, headers, parameters);
        }

        private static async Task<TResult> GetByAsync<TResult>(
            string domain,
            string method,
            Action<HttpRequestHeaders> headers = null,
            params KeyValuePair<string, object>[] parameters)
        {
            var result = await InvokeGetAsync(domain, method, headers, parameters);

            return await result
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsAsync<TResult>();
        }

        /// <summary>
        /// Sends a request of type T as a Post
        /// </summary>
        /// <typeparam name="T">Type of the request</typeparam>
        /// <param name="objectToSend">Object of type T to be sent</param>
        /// <param name="domain">Server domain to send information to</param>
        /// <param name="method">Method to execute to send information</param>
        /// <param name="headers">Include headers to request</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PostAsync<T>(
            T objectToSend,
            string domain,
            string method = "",
            Action<HttpRequestHeaders> headers = null,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return await InvokeMethodAsync(
                domain, 
                client => client.PostAsJsonAsync(BuildMethodName(method, parameters), objectToSend), 
                headers);
        }

        /// <summary>
        /// Sends a request of type T as a Put
        /// </summary>
        /// <typeparam name="T">Type of the request</typeparam>
        /// <param name="objectToSend">Object of type T to be sent</param>
        /// <param name="domain">Server domain to send information to</param>
        /// <param name="method">Method to execute to send information</param>
        /// <param name="headers">Include headers to request</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> PutAsync<T>(
            T objectToSend,
            string domain,
            string method = "",
            Action<HttpRequestHeaders> headers = null,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return await InvokeMethodAsync(
                domain, 
                client => client.PutAsJsonAsync(BuildMethodName(method, parameters), objectToSend),
                headers);
        }

        /// <summary>
        /// Returns the Http response from a Delete request
        /// </summary>
        /// <param name="domain">Server domain to delete information from</param>
        /// <param name="method">Method to execute to delete the info</param>
        /// <param name="headers">Include headers to request</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public static async Task<HttpResponseMessage> DeleteAsync(
            string domain,
            string method = "",
            Action<HttpRequestHeaders> headers = null,
            params KeyValuePair<string, object>[] parameters)
        {
            return await InvokeMethodAsync(
                domain, 
                client => client .DeleteAsync(BuildMethodName(method, parameters)),
                headers);
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

        private static async Task<HttpResponseMessage> InvokeGetAsync(
            string domain,
            string method,
            Action<HttpRequestHeaders> headers = null,
            params KeyValuePair<string, object>[] parameters)
        {
            return await InvokeMethodAsync(
                domain, 
                client => client.GetAsync(BuildMethodName(method, parameters)), 
                headers);
        }

        private static async Task<HttpResponseMessage> InvokeMethodAsync(
            string domain,
            Func<HttpClient, Task<HttpResponseMessage>> method,
            Action<HttpRequestHeaders> headers = null,
            bool addApplicationJsonHeader = true)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domain);

                SetsHeaders(client, addApplicationJsonHeader, headers);

                return await method(client);
            }
        }

        private static void SetsHeaders(
            HttpClient client,
            bool addApplicationJsonHeader,
            Action<HttpRequestHeaders> headers = null)
        {
            SetAcceptHeader(client, addApplicationJsonHeader);

            if (headers.IsNotNull())
                headers(client.DefaultRequestHeaders);
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
    }
}
