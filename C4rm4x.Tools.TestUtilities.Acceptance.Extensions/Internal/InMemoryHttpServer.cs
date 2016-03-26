#region Using

using C4rm4x.Tools.HttpUtilities;
using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Web.Http;

#endregion

namespace C4rm4x.Tools.TestUtilities.Internal
{
    /// <summary>
    /// Service responsible to manage all the interaction between the acceptance tests and in-memory server
    /// </summary>
    internal class InMemoryHttpServer : IDisposable
    {
        private HttpServer _httpServer;

        /// <summary>
        /// Gets the url this in-memory http server in bound to
        /// </summary>
        public string BaseUrl { get; private set; }

        /// <summary>
        /// Gets whether or not this instance has already been disposed
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets the security token to be included with each request
        /// </summary>
        public string SecurityToken { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public InMemoryHttpServer()
        {
            BaseUrl = ConfigurationManager.AppSettings["Acceptance.Test.Server.BaseUrl"];
        }

        /// <summary>
        /// Configure the in-memory HttpServer configuration
        /// </summary>
        /// <param name="configurator">The HttpConfiguration configurator</param>
        public void Configure(Action<HttpConfiguration> configurator)
        {
            var config = new HttpConfiguration();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            configurator(config);

            _httpServer = new HttpServer(config);
        }

        /// <summary>
        /// Returns the Http response from a Get request
        /// </summary>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public HttpResponseMessage Get(
            string method,
            params KeyValuePair<string, object>[] parameters)
        {
            return RESTfulConsumer.Get(BaseUrl, method, SecurityToken, _httpServer, parameters);
        }

        /// <summary>
        /// Returns an instance of type T
        /// </summary>
        /// <typeparam name="T">The type of the instance</typeparam>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the instance of type T if exists</returns>
        public T Get<T>(
            string method,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.Get<T>(BaseUrl, method, SecurityToken, _httpServer, parameters);
        }

        /// <summary>
        /// Returns all instances of type T
        /// </summary>
        /// <typeparam name="T">The type of the instance</typeparam>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns all the instance of type T</returns>
        public IEnumerable<T> GetAll<T>(
            string method,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.GetAll<T>(BaseUrl, method, SecurityToken, _httpServer, parameters);
        }

        /// <summary>
        /// Sends a request of type T as a Post
        /// </summary>
        /// <typeparam name="T">The type of the instance</typeparam>
        /// <param name="objectToSend">Instance to send</param>
        /// <param name="method">Method to execute to send information</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public HttpResponseMessage Post<T>(
            T objectToSend,
            string method)
            where T : class
        {
            return RESTfulConsumer.Post(objectToSend, BaseUrl, method, SecurityToken, _httpServer);
        }

        /// <summary>
        /// Sends a request of type T as a Put
        /// </summary>
        /// <typeparam name="T">The type of the instance</typeparam>
        /// <param name="objectToSend">Instance to send</param>
        /// <param name="method">Method to execute to send information</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public HttpResponseMessage Put<T>(
            T objectToSend,
            string method,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return RESTfulConsumer.Put(objectToSend, BaseUrl, method, SecurityToken, _httpServer, parameters);
        }

        /// <summary>
        /// Sends a request as a Delete
        /// </summary>
        /// <param name="method">Method to execute to delete information</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        public HttpResponseMessage Delete(
            string method,
            params KeyValuePair<string, object>[] parameters)
        {
            return RESTfulConsumer.Delete(BaseUrl, method, SecurityToken, _httpServer, parameters);
        }

        /// <summary>
        /// Sets the security token to be used in each request
        /// </summary>
        /// <param name="securityToken"></param>
        public void SetSecurityToken(string securityToken)
        {
            securityToken.NotNullOrEmpty(nameof(securityToken));

            SecurityToken = securityToken;
        }

        /// <summary>
        /// Releases all the managed resources
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed) return;

            if (_httpServer.IsNotNull())
                _httpServer.Dispose();

            GC.SuppressFinalize(this);

            IsDisposed = true;
        }
    }
}
