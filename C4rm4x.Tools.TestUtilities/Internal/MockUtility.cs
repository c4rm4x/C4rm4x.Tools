#region Using

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace C4rm4x.Tools.TestUtilities.Internal
{
    internal static class MockUtility
    {
        public static ConstructorInfo BuildContainer<T>(
            IDictionary<Type, Mock> container,
            params KeyValuePair<Type, Mock>[] mocks)
        {
            var constructor = GetMostSpecificConstructor<T>();

            CreateMocks(container, mocks, constructor.GetParameters());

            return constructor;
        }

        private static ConstructorInfo GetMostSpecificConstructor<T>()
        {
            var constructors = typeof(T)
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            if (!constructors.Any())
                throw new ArgumentException(
                    string.Format("Type {0} does not have any public constructor", typeof(T).Name));

            var constructor = constructors.First();

            if (constructors.Count() > 1)
                constructor = constructors
                    .OrderBy(c => c.GetParameters().Count())
                    .LastOrDefault(); // Take always the most restrictive one

            return constructor;
        }

        private static void CreateMocks(
            IDictionary<Type, Mock> container,
            KeyValuePair<Type, Mock>[] mocks,
            ParameterInfo[] parameters)
        {
            foreach (var parameter in parameters)
            {
                var mock = GetMock(parameter.ParameterType, mocks);
                container.Add(parameter.ParameterType, mock);
            }
        }

        private static Mock GetMock(
            Type parameterType,
            KeyValuePair<Type, Mock>[] mocks)
        {
            return mocks.Any(m => m.Key == parameterType)
                ? mocks.First(m => m.Key == parameterType).Value
                : CreateMock(parameterType);
        }

        private static Mock CreateMock(Type parameterType)
        {
            var mockType = typeof(Mock<>);
            var typeArgs = new { parameterType };
            var makeMe = mockType.MakeGenericType(parameterType);

            return (Mock)Activator.CreateInstance(makeMe);
        }
    }
}
