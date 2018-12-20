#region Using

using C4rm4x.Tools.HttpUtilities.Acl.Configuration;
using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl
{
    /// <summary>
    /// Base implementation of an API subscriber using ACL based security
    /// </summary>
    public abstract class AclRESTfulConsumer
    {
        private readonly Lazy<AclRESTfulConsumerConfiguration> _config;

        private AclRESTfulConsumerConfiguration Config => _config.Value;

        /// <summary>
        /// Gets the subscription name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The consumer name</param>
        public AclRESTfulConsumer(string name)
        {
            name.NotNullOrEmpty(nameof(name));

            Name = name;

            _config = new Lazy<AclRESTfulConsumerConfiguration>(() =>
                this.GetClientConfiguration());
        }

        /// <summary>
        /// Returns the Http response from a GET request
        /// </summary>
        /// <param name="method">Method to execute to retrieve response</param>
        /// <param name="parameters">Parameters to include as part of queryString</param>
        /// <returns>The HttpResponseMessage</returns>
        protected Task<HttpResponseMessage> GetAsync(
            string method = "",
            params KeyValuePair<string, object>[] parameters)
        {
            return RESTfulConsumer.GetAsync(
                Config.ApiBaseUrl,
                method,
                this.GetAuthorizationHeader(Config),
                parameters);
        }

        /// <summary>
        /// Returns an instance of type T
        /// </summary>
        /// <typeparam name="T">The type of the instance to return</typeparam>
        /// <param name="method">Method to execute to retrieve response</param>
        /// <param name="parameters">Parameters to include as part of queryString</param>
        /// <returns>An instance of type T if exists</returns>
        protected Task<T> GetAsync<T>(
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.GetAsync<T>(
                Config.ApiBaseUrl,
                method,
                this.GetAuthorizationHeader(Config),
                parameters);
        }

        /// <summary>
        /// Returns all the instances of type T
        /// </summary>
        /// <typeparam name="T">The type of the instance to return</typeparam>
        /// <param name="method">Method to execute to retrieve response</param>
        /// <param name="parameters">Parameters to include as part of queryString</param>
        /// <returns>The list of all the instances of type T if any</returns>
        protected Task<IEnumerable<T>> GetAllAsync<T>(
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.GetAllAsync<T>(
                Config.ApiBaseUrl,
                method,
                this.GetAuthorizationHeader(Config),
                parameters);
        }

        /// <summary>
        /// Sends a request of type T as a POST
        /// </summary>
        /// <typeparam name="T">Type of the request</typeparam>
        /// <param name="objectToSend">Object to send</param>
        /// <param name="method">Method to execute to send data</param>
        /// <param name="parameters">Parameters to include as part of queryString</param>
        /// <returns>The HttpResponseMessage</returns>
        protected Task<HttpResponseMessage> PostAsync<T>(
            T objectToSend,
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.PostAsync<T>(
                objectToSend,
                Config.ApiBaseUrl,
                method,
                this.GetAllHeaders(Config, objectToSend),
                parameters);
        }

        /// <summary>
        /// Sends a request of type T as a PUT
        /// </summary>
        /// <typeparam name="T">Type of the request</typeparam>
        /// <param name="objectToSend">Object to send</param>
        /// <param name="method">Method to execute to send data</param>
        /// <param name="parameters">Parameters to include as part of queryString</param>
        /// <returns>The HttpResponseMessage</returns>
        protected Task<HttpResponseMessage> PutAsync<T>(
            T objectToSend,
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.PutAsync<T>(
                objectToSend,
                Config.ApiBaseUrl,
                method,
                this.GetAllHeaders(Config, objectToSend),
                parameters);
        }

        /// <summary>
        /// Returns the response from a DELETE request
        /// </summary>
        /// <param name="method">Method to execute to delete data</param>
        /// <param name="parameters">Parameters to include as part of queryString</param>
        /// <returns>The HttpResponseMessage</returns>
        protected Task<HttpResponseMessage> DeleteAsync(
            string method = "",
            params KeyValuePair<string, object>[] parameters)
        {
            return RESTfulConsumer.DeleteAsync(
                Config.ApiBaseUrl,
                method,
                this.GetAuthorizationHeader(Config),
                parameters);
        }
    }
}
