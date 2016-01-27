#region Using

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;

#endregion

namespace C4rm4x.Tools.TestUtilities
{
    /// <summary>
    /// Base test class for integration tests
    /// </summary>
    /// <typeparam name="T">Type of subject under test</typeparam>
    [TestClass]
    public abstract class IntegrationFixture<T>
        where T : class
    {
        private Container _container;
        private Scope _scope;

        /// <summary>
        /// The instance of subject under test
        /// </summary>
        protected T _sut;

        /// <summary>
        /// Initialise the test class
        /// </summary>
        [TestInitialize]
        public virtual void Setup()
        {
            _container = new Container();

            RegisterDependencies(_container, new LifetimeScopeLifestyle()); // Registers the rest

            _container.Verify();

            _scope = _container.BeginLifetimeScope(); // Starts container life time scope
            _sut = GetInstance<T>();
        }

        /// <summary>
        /// Clean up resources after each test is run
        /// </summary>
        [TestCleanup]
        public virtual void Cleanup()
        {
            _scope.Dispose(); // Enforce to dispose all the components
        }

        /// <summary>
        /// Registers the rest of dependencies within the container
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="lifeStyle">The life style that specifies how the return instance will be cached</param>
        /// <remarks>When override, do ALWAYS base.RegisterDependencies(container, lifeStyle)</remarks>
        protected virtual void RegisterDependencies(
            Container container,
            Lifestyle lifeStyle)
        {
            _container.RegisterLifetimeScope<T>(); // Registers the class as itself
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
    }
}
