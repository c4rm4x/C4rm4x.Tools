#region Using

using C4rm4x.Tools.HttpUtilities.Acl.Configuration;
using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;

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
        /// Constructor
        /// </summary>
        /// <param name="name">The consumer name</param>
        public AclRESTfulConsumer(string name)
        {
            name.NotNullOrEmpty(nameof(name));

            _config = new Lazy<AclRESTfulConsumerConfiguration>(() =>
                this.GetClientConfiguration(name));
        }

        /// <summary>
        /// Returns the Http response from a GET request
        /// </summary>
        /// <param name="method">Method to execute to retrieve response</param>
        /// <param name="parameters">Parameters to include as part of queryString</param>
        /// <returns>The HttpResponseMessage</returns>
        protected HttpResponseMessage Get(
            string method = "",
            params KeyValuePair<string, object>[] parameters)
        {
            return RESTfulConsumer.Get(
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
        protected T Get<T>(
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.Get<T>(
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
        protected IEnumerable<T> GetAll<T>(
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.GetAll<T>(
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
        protected HttpResponseMessage Post<T>(
            T objectToSend,
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.Post<T>(
                objectToSend,
                Config.ApiBaseUrl,
                method,
                this.GetAuthorizationHeader(Config),
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
        protected HttpResponseMessage Put<T>(
            T objectToSend,
            string method = "",
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.Put<T>(
                objectToSend,
                Config.ApiBaseUrl,
                method,
                this.GetAuthorizationHeader(Config),
                parameters);
        }

        /// <summary>
        /// Returns the response from a DELETE request
        /// </summary>
        /// <param name="method">Method to execute to delete data</param>
        /// <param name="parameters">Parameters to include as part of queryString</param>
        /// <returns>The HttpResponseMessage</returns>
        protected HttpResponseMessage Delete(
            string method = "",
            params KeyValuePair<string, object>[] parameters)
        {
            return RESTfulConsumer.Delete(
                Config.ApiBaseUrl,
                method,
                this.GetAuthorizationHeader(Config),
                parameters);
        }
    }
}
