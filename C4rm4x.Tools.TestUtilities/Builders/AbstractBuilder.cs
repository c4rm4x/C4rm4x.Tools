#region Using

using System;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace C4rm4x.Tools.TestUtilities.Builders
{
    /// <summary>
    /// Base class to build random instances of type T
    /// </summary>
    /// <typeparam name="T">The type of the instace to create</typeparam>
    public abstract class AbstractBuilder<T>
            where T : class
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

        /// <summary>
        /// Sets the value of the given private property
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="setter">The expression to get the property to be set</param>
        /// <param name="value">The new value</param>
        protected void SetPropertyValue<TProperty>(
            Expression<Func<T, TProperty>> setter, 
            TProperty value)
        {
            var propertyInfo = GetPropertyInfo(setter);

            propertyInfo.SetValue(_entity, value);
        }

        private PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<T, TProperty>> setter)
        {
            var type = typeof(T);

            var member = setter.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    setter.ToString()));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    setter.ToString()));

            return propInfo;
        }
    }
}
