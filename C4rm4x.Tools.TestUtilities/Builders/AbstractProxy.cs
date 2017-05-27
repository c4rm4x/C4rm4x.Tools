#region Using

using Moq;
using System;
using System.Linq.Expressions;

#endregion

namespace C4rm4x.Tools.TestUtilities.Builders
{
    /// <summary>
    /// Base class to build random proxies of type T
    /// </summary>
    /// <typeparam name="T">The type of the proxy to create</typeparam>
    public abstract class AbstractProxy<T>
        where T : class
    {
        /// <summary>
        /// The proxy created
        /// </summary>
        protected readonly T _entity;

        /// <summary>
        /// Constructor
        /// </summary>
        public AbstractProxy()
        {
            _entity = Mock.Of<T>();
        }

        /// <summary>
        /// Returns the proxy created
        /// </summary>
        /// <returns></returns>
        public T Build()
        {
            return _entity;
        }

        /// <summary>
        /// Sets property getter
        /// </summary>
        /// <typeparam name="TProperty">Type of the property</typeparam>
        /// <param name="expression">Lambda expression that specifies the property getter</param>
        /// <param name="value">The value</param>
        protected void SetProperty<TProperty>(
            Expression<Func<T, TProperty>> expression,
            TProperty value)
        {
            Mock.Get(_entity).SetupGet(expression).Returns(value);
        }
    }
}
