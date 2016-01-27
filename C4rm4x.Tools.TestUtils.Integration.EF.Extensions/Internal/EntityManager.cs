#region Using

using System;
using System.Data.Entity;
using System.Linq;

#endregion

namespace C4rm4x.Tools.TestUtils.Internal
{
    /// <summary>
    /// Service responsible to manage the persistante of the entities
    /// </summary>
    /// <typeparam name="C">The type of the DbContext</typeparam>
    internal class EntityManager<C> : IDisposable
        where C : DbContext
    {
        private readonly C _entities;

        /// <summary>
        /// Gets whether or not this instance has already been disposed
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entities">The db context</param>
        public EntityManager(C entities)
        {
            _entities = entities;
        }

        /// <summary>
        /// Adds an entity into the context
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity</typeparam>
        /// <param name="entity">Entity to add</param>
        public void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            _entities.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Commits the changes into the database
        /// </summary>
        public void SaveAllChanges()
        {
            _entities.SaveChanges();
        }

        /// <summary>
        /// Restores the database to its previous state and closes the db connection
        /// </summary>
        public void Restore()
        {
            foreach (var entity in _entities.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Deleted))
                entity.State = EntityState.Deleted;

            SaveAllChanges(); // Enforces the entities to be delated
        }

        /// <summary>
        /// Releases all the managed resources
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed) return;

            _entities.Dispose();

            GC.SuppressFinalize(this);

            IsDisposed = true;
        }
    }
}
