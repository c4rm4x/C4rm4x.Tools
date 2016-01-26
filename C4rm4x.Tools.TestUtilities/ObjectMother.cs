#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

#endregion

namespace C4rm4x.Tools.TestUtilities
{
    /// <summary>
    /// Random generator class
    /// </summary>
    public static class ObjectMother
    {
        private const int MaxStringLength = 100;
        private const int DaysLimit = 365;

        private static Random _random;

        /// <summary>
        /// Creates a random string for a given length
        /// </summary>
        /// <param name="length">The length of the random string</param>
        /// <returns>Random string generated</returns>
        public static string Create(int length)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < length; i++)
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * GetRandom().NextDouble() + 65))));

            return builder.ToString();
        }

        /// <summary>
        /// Creates a random object based on type
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <returns>Random object</returns>
        public static T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        /// <summary>
        /// Crates a random object based on type
        /// </summary>
        /// <param name="type">Type of the object</param>
        /// <returns>Random objet</returns>        
        public static object Create(Type type)
        {
            if (type == typeof(string))
                return Create(Next(MaxStringLength));
            else if (type == typeof(Guid))
                return Guid.NewGuid();
            else if (type == typeof(DateTime))
                return DateTime.Now.AddDays(GetRandom().Next(-DaysLimit, DaysLimit));
            else if (type == typeof(int))
                return Next();
            else if (type == typeof(decimal))
                return Convert.ToDecimal(Next());
            else if (type == typeof(double))
                return GetRandom().NextDouble();
            else if (type == typeof(long))
                return Convert.ToInt64(Next());
            else if (type == typeof(short))
                return Convert.ToInt16(Next(short.MaxValue));
            else if (type == typeof(bool))
                return GetRandom().Next(2) == 0;
            else if (type == typeof(byte))
                return Convert.ToByte(Next(byte.MaxValue));
            else if (type.IsClass)
                return CreateComplexObject(type);
            else
                return Default(type);
        }

        private static Random GetRandom()
        {
            if (_random == null)
                _random = new Random((int)DateTime.Now.Ticks);

            return _random;
        }

        private static int Next(int maxValue = int.MaxValue)
        {
            return GetRandom().Next(1, maxValue);
        }

        private static object CreateComplexObject(Type type)
        {
            try
            {
                var instance = Activator.CreateInstance(type, true);

                if (!typeof(IEnumerable).IsInstanceOfType(instance)) // Enumerable objects... only get initialized !
                {
                    foreach (var propertyInfo in GetPublicSetters(type))
                        propertyInfo.SetValue(instance, Create(propertyInfo.PropertyType));
                }

                return instance;
            }
            catch (Exception)
            {
                return null; // In case a new instance of given type cannot be created
            }
        }

        private static IEnumerable<PropertyInfo> GetPublicSetters(Type type)
        {
            return type.GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty)
                .Where(p => p.GetSetMethod() != null && !p.GetMethod.IsVirtual);
        }

        private static object Default(Type type)
        {
            return type.IsValueType
                ? Activator.CreateInstance(type)
                : null;
        }
    }
}
