#region Using

using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.TestUtils.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System.Data.Entity;

#endregion

namespace C4rm4x.Tools.TestUtils
{
    /// <summary>
    /// Base test class for integration tests using Entity Framework for access to the database
    /// </summary>
    /// <typeparam name="T">Type of subject under test</typeparam>
    /// <typeparam name="C">Type of DbContext</typeparam>
    [TestClass]
    public abstract class IntegrationFixture<T, C> : IntegrationFixture<T>
        where T : class
        where C : DbContext
    {
        /// <summary>
        /// Initialise the test class
        /// </summary>
        [TestCleanup]
        public override void Cleanup()
        {
            EntityManager.Restore();

            base.Cleanup();
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

        /// <summary>
        /// Registers all the dependencies for this integration test
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="lifeStyle">The life style that specifies how the return instance will be cached</param>
        protected override void RegisterDependencies(
            Container container,
            Lifestyle lifeStyle)
        {
            base.RegisterDependencies(container, lifeStyle);

            container.Register<EntityManager<C>>(lifeStyle);
            container.Register<C>(lifeStyle);
        }

        private EntityManager<C> EntityManager
        {
            get { return GetInstance<EntityManager<C>>(); }
        }
    }
}
