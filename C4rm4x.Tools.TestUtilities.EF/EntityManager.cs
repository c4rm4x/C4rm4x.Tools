#region Using

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

#endregion

namespace C4rm4x.Tools.TestUtilities.EF
{
    /// <summary>
    /// Service responsible to manage the persistance of the entities
    /// </summary>
    /// <typeparam name="C">The type of the DbContext</typeparam>
    public class EntityManager<C> : IDisposable
        where C : DbContext
    {
        private readonly C _entities;
        private readonly ICollection<object> _addedEntities; // I need to keep the history of the entities added

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
            _addedEntities = new List<object>();
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
            _addedEntities.Add(entity);
        }

        /// <summary>
        /// Commits the changes into the database
        /// </summary>
        public void SaveAllChanges()
        {
            retry:
            try
            {
                _entities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                goto retry;
            }
        }

        /// <summary>
        /// Restores the database to its previous state and closes the db connection
        /// </summary>
        public void Restore()
        {
            foreach (var entity in _addedEntities)
                _entities.Set(entity.GetType()).Remove(entity);

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
