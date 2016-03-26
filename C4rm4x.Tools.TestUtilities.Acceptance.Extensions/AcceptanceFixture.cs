#region Using

using C4rm4x.Tools.TestUtilities.Bdd;
using C4rm4x.Tools.TestUtilities.Internal;
using C4rm4x.Tools.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;

#endregion

namespace C4rm4x.Tools.TestUtilities
{
    /// <summary>
    /// Base test class for acceptance tests
    /// </summary>
    [TestClass]
    public abstract class AcceptanceFixture
    {
        private Container _container;
        private Scope _scope;
        private GivenWhenThen _givenWhenThen;

        private readonly Action<HttpConfiguration> _configurator;

        /// <summary>
        /// The test context
        /// </summary>
        protected TestContext Context { get; private set; } 
            = new TestContext();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configurator">Http configurator</param>
        public AcceptanceFixture(Action<HttpConfiguration> configurator)
        {
            configurator.NotNull(nameof(configurator));

            _configurator = configurator;
        }

        /// <summary>
        /// Initialises the test class
        /// </summary>
        [TestInitialize]
        public virtual void Setup()
        {
            SetupContainer();
            SetupHttpServer();
        }

        private void SetupContainer()
        {
            _container = new Container();

            RegisterDependencies(_container, new LifetimeScopeLifestyle()); // Registers the rest

            _container.Verify();

            _scope = _container.BeginLifetimeScope(); // Starts container life time scope
        }

        private void SetupHttpServer()
        {
            HttpServer.Configure(_configurator);
        }

        /// <summary>
        /// Finalises the test class
        /// </summary>
        [TestCleanup]
        public virtual void Cleanup()
        {
            _givenWhenThen = null;

            _scope.Dispose(); // Enforce to dispose all the components

            Context.Cleanup();
        }

        /// <summary>
        /// Registers all the dependencies for this acceptance test
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="lifeStyle">The life style that specifies how the return instance will be cached</param>
        protected virtual void RegisterDependencies(
            Container container,
            Lifestyle lifeStyle)
        {
            container.Register<InMemoryHttpServer>(lifeStyle);
        }

        /// <summary>
        /// Returns the Http response from a Get request
        /// </summary>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        protected HttpResponseMessage Get(
            string method,
            params KeyValuePair<string, object>[] parameters)
        {
            return HttpServer.Get(method, parameters);
        }

        /// <summary>
        /// Returns an instance of type T
        /// </summary>
        /// <typeparam name="T">The type of the instance</typeparam>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the instance of type T if exists</returns>
        protected T Get<T>(
            string method,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return HttpServer.Get<T>(method, parameters);
        }

        /// <summary>
        /// Returns all instances of type T
        /// </summary>
        /// <typeparam name="T">The type of the instance</typeparam>
        /// <param name="method">Method to execute to retrieve instance</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns all the instance of type T</returns>
        protected IEnumerable<T> GetAll<T>(
            string method,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return HttpServer.GetAll<T>(method, parameters);
        }

        /// <summary>
        /// Sends a request of type T as a Post
        /// </summary>
        /// <typeparam name="T">The type of the instance</typeparam>
        /// <param name="objectToSend">Instance to send</param>
        /// <param name="method">Method to execute to send information</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        protected HttpResponseMessage Post<T>(
            T objectToSend,
            string method)
            where T : class
        {
            return HttpServer.Post(objectToSend, method);
        }

        /// <summary>
        /// Sends a request of type T as a Put
        /// </summary>
        /// <typeparam name="T">The type of the instance</typeparam>
        /// <param name="objectToSend">Instance to send</param>
        /// <param name="method">Method to execute to send information</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        protected HttpResponseMessage Put<T>(
            T objectToSend,
            string method,
            params KeyValuePair<string, object>[] parameters)
            where T : class
        {
            return HttpServer.Put(objectToSend, method, parameters);
        }

        /// <summary>
        /// Sends a request as a Delete
        /// </summary>
        /// <param name="method">Method to execute to delete the information</param>
        /// <param name="parameters">Parameters to include as part of query string</param>
        /// <returns>Returns the HttpResponseMessage</returns>
        protected HttpResponseMessage Delete(
            string method,
            params KeyValuePair<string, object>[] parameters)
        {
            return HttpServer.Delete(method, parameters);
        }

        /// <summary>
        /// Starts a new test plan with the initial given step
        /// </summary>
        /// <param name="step">The initial given step</param>
        protected IGivenDefinition Given(GivenHandler step)
        {
            _givenWhenThen = GivenWhenThen.StartWith(step);

            return _givenWhenThen;
        }

        /// <summary>
        /// Starts a new test plan with the initial when step
        /// </summary>
        /// <param name="step">Then initial when step</param>
        protected IWhenDefinition When(WhenHandler step)
        {
            _givenWhenThen = GivenWhenThen.StartWith(step);

            return _givenWhenThen;
        }

        /// <summary>
        /// Gets an instance of the specified type
        /// </summary>
        /// <typeparam name="TService">Type of instance</typeparam>
        /// <returns>An instance of specified type</returns>
        /// <exception cref="SimpleInjector.ActivationException">Thrown when there are errors resolving the service instance</exception>
        protected TService GetInstance<TService>()
            where TService : class
        {
            return _container.GetInstance<TService>();
        }

        private InMemoryHttpServer HttpServer
        {
            get { return GetInstance<InMemoryHttpServer>(); }
        }
    }

    /// <summary>
    /// Base test class for acceptance tests using Entity Framework for access to the database
    /// </summary>
    /// <typeparam name="TContext">Type of DbContext</typeparam>
    [TestClass]
    public abstract class AcceptanceFixture<TContext> :
        AcceptanceFixture
        where TContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configurator">Http configurator</param>
        public AcceptanceFixture(Action<HttpConfiguration> configurator)
            : base(configurator)
        { }        

        /// <summary>
        /// Finalises the test class
        /// </summary>
        [TestCleanup]
        public override void Cleanup()
        {
            EntityManager.Restore();

            base.Cleanup();
        }        

        /// <summary>
        /// Registers all the dependencies for this acceptance test
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="lifeStyle">The life style that specifies how the return instance will be cached</param>
        protected override void RegisterDependencies(
            Container container,
            Lifestyle lifeStyle)
        {
            container.Register<EntityManager<TContext>>(lifeStyle);
            container.Register<TContext>(lifeStyle);

            base.RegisterDependencies(container, lifeStyle);
        }

        /// <summary>
        /// Adds a new entity to the db context
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity</typeparam>
        /// <param name="entity">Entity to add</param>
        /// <param name="saveContext">Indicates whether the entity must be saved in the context</param>
        protected void AddEntityToContext<TEntity>(
            TEntity entity,
            bool saveContext = false)
            where TEntity : class
        {
            EntityManager.Add(entity);

            if (saveContext)
                SaveContext();
        }

        /// <summary>
        /// Saves all the entities within the context into the database
        /// </summary>
        protected void SaveContext()
        {
            EntityManager.SaveAllChanges();
        }
        
        private EntityManager<TContext> EntityManager
        {
            get { return GetInstance<EntityManager<TContext>>(); }
        }
    }
}
