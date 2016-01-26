#region Using

using System;

#endregion

namespace C4rm4x.Tools.TestUtilities.Builders
{
    /// <summary>
    /// Base class to build random instances of type T
    /// </summary>
    /// <typeparam name="T">The type of the instace to create</typeparam>
    public abstract class AbstractBuilder<T>
            where T : class, new()
    {
        /// <summary>
        /// The instace created
        /// </summary>
        protected readonly T _entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public AbstractBuilder()
        {
            _entity = ObjectMother.Create<T>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customGenerator">Apply custom generation rules</param>
        public AbstractBuilder(Action<T> customGenerator)
            : this()
        {
            customGenerator(_entity);
        }

        /// <summary>
        /// Returns the instance created
        /// </summary>
        /// <returns></returns>
        public T Build()
        {
            return _entity;
        }
    }
}
