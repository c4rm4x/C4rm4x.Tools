#region Using

using C4rm4x.Tools.TestUtilities.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace C4rm4x.Tools.TestUtilities
{
    /// <summary>
    /// Base test class that auto mock all the depedencies of the subject under test
    /// </summary>
    /// <typeparam name="T">Type of the subject under test</typeparam>
    [TestClass]
    public abstract class AutoMockFixture<T>
        where T : class
    {
        /// <summary>
        /// The instance of subject under test
        /// </summary>
        protected T _sut;

        private IDictionary<Type, Mock> _container;

        /// <summary>
        /// Initialises the subject under test mocking all its depedencies
        /// </summary>
        [TestInitialize]
        public virtual void Setup()
        {
            BuildContainer();
        }

        /// <summary>
        /// Build container overwriting the specified mocks
        /// </summary>
        /// <param name="mocks">Specific mocks in case more specific behavior is required</param>
        protected void BuildContainer(params KeyValuePair<Type, Mock>[] mocks)
        {
            _container = new Dictionary<Type, Mock>();

            var constructor = MockUtility
                .BuildContainer<T>(_container, mocks);

            _sut = (T)constructor.Invoke(
                _container.Values.Select(m => m.Object).ToArray());
        }

        /// <summary>
        /// Gets a mock for the specific dependency
        /// </summary>
        /// <typeparam name="TMock">Type of the dependency</typeparam>
        /// <returns>A mock for the specific dependency</returns>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        /// <remark>Use this method in case you cannot use any of the other methods</remark>
        protected Mock<TMock> GetMock<TMock>()
            where TMock : class
        {
            if (!_container.ContainsKey(typeof(TMock)))
                throw new ArgumentException(
                    string.Format("There is not mock for type {0}", typeof(TMock).Name));

            return (Mock<TMock>)_container[typeof(TMock)];
        }

        /// <summary>
        /// Setups an expected behavior
        /// </summary>
        /// <typeparam name="TMock">Type of the dependency</typeparam>
        /// <param name="setup">Setup</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected ISetup<TMock> Setup<TMock>(Expression<Action<TMock>> setup)
            where TMock : class
        {
            return GetMock<TMock>()
                .Setup(setup);
        }

        /// <summary>
        /// Setups an expected behavior where a result is expected
        /// </summary>
        /// <typeparam name="TMock">Type of the dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="setup">Setup</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected ISetup<TMock, TResult> Setup<TMock, TResult>(
            Expression<Func<TMock, TResult>> setup)
            where TMock : class
        {
            return GetMock<TMock>()
                .Setup(setup);
        }

        /// <summary>
        /// Setups an expected behavior for a property getter
        /// </summary>
        /// <typeparam name="TMock">Type of the dependency</typeparam>
        /// <typeparam name="TProperty">Property to setup behavior when is accessed as a getter</typeparam>
        /// <param name="setup">Setup</param>
        protected ISetupGetter<TMock, TProperty> SetupGet<TMock, TProperty>(
            Expression<Func<TMock, TProperty>> setup)
            where TMock : class
        {
            return GetMock<TMock>()
                .SetupGet(setup);
        }

        /// <summary>
        /// Setups an expected behavior for a property setter
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TProperty">Property to setup behavior when is accessed as a setter</typeparam>
        /// <param name="setup">Setup</param>
        protected ISetupSetter<TMock, TProperty> SetupSet<TMock, TProperty>(
            Action<TMock> setup)
            where TMock : class
        {
            return GetMock<TMock>()
                .SetupSet<TProperty>(setup);
        }

        /// <summary>
        /// Returns an specified result for the expected behavior
        /// </summary>
        /// <typeparam name="TMock">Type of the dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="result">Result</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IReturnsResult<TMock> Returns<TMock, TResult>(
            Expression<Func<TMock, TResult>> setup,
            TResult result)
            where TMock : class
        {
            return Setup(setup)
                .Returns(result);
        }

        /// <summary>
        /// Verifies the expected behavior is invoked as many times as specified
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="times">Number of times of expected behavior happens</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected void Verify<TMock>(
            Expression<Action<TMock>> setup,
            Times times)
            where TMock : class
        {
            GetMock<TMock>()
                .Verify(setup, times);
        }

        /// <summary>
        /// Verifies the expected behavior is invoked as many times as specified
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="times">Number of times of expected behavior happens</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected void Verify<TMock, TResult>(
            Expression<Func<TMock, TResult>> setup,
            Times times)
            where TMock : class
        {
            GetMock<TMock>()
                .Verify(setup, times);
        }

        /// <summary>
        /// Throws an specific type of exception when setup is invoked
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="setup">Setup</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IThrowsResult Throws<TMock, TException>(
            Expression<Action<TMock>> setup)
            where TMock : class
            where TException : Exception, new()
        {
            return Setup(setup)
                .Throws<TException>();
        }

        /// <summary>
        /// Throws the exception when setup is inovked
        /// </summary>
        /// <typeparam name="TMock">Tpe of dependency</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="exceptionToThrow">Exception to throw</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IThrowsResult Throws<TMock>(
            Expression<Action<TMock>> setup,
            Exception exceptionToThrow)
            where TMock : class
        {
            return Setup(setup)
                .Throws(exceptionToThrow);
        }

        /// <summary>
        /// Throws an specific type of exception when setup is invoked
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="setup">Setup</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IThrowsResult Throws<TMock, TResult, TException>(
            Expression<Func<TMock, TResult>> setup)
            where TMock : class
            where TException : Exception, new()
        {
            return Setup(setup)
                .Throws<TException>();
        }

        /// <summary>
        /// Throws the exception when setup is invoked
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="exceptionToThrow">Exception to throw</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IThrowsResult Throws<TMock, TResult>(
            Expression<Func<TMock, TResult>> setup,
            Exception exceptionToThrow)
            where TMock : class
        {
            return Setup(setup)
                .Throws(exceptionToThrow);
        }

        /// <summary>
        /// Specifies a callback to be invoked when setup is called
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="callback">Callback</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected ICallbackResult Callback<TMock>(
            Expression<Action<TMock>> setup,
            Action callback)
            where TMock : class
        {
            return Setup(setup)
                .Callback(callback);
        }

        /// <summary>
        /// Specifies a callback with one argument to be invoked when setup is called
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="T1">Type of first argument</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="callback">Callback</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected ICallbackResult Callback<TMock, T1>(
            Expression<Action<TMock>> setup,
            Action<T1> callback)
            where TMock : class
        {
            return Setup(setup)
                .Callback(callback);
        }

        /// <summary>
        /// Specifies a callback with two arguments to be invoked when setup is called
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="T1">Type of first argument</typeparam>
        /// <typeparam name="T2">Type of second argument</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="callback">Callback</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected ICallbackResult Callback<TMock, T1, T2>(
            Expression<Action<TMock>> setup,
            Action<T1, T2> callback)
            where TMock : class
        {
            return Setup(setup)
                .Callback(callback);
        }

        /// <summary>
        /// Specifies a callback with three arguments to be invoked when setup is called
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="T1">Type of first argument</typeparam>
        /// <typeparam name="T2">Type of second argument</typeparam>
        /// <typeparam name="T3">Type of third argument</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="callback">Callback</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected ICallbackResult Callback<TMock, T1, T2, T3>(
            Expression<Action<TMock>> setup,
            Action<T1, T2, T3> callback)
            where TMock : class
        {
            return Setup(setup)
                .Callback(callback);
        }

        /// <summary>
        /// Specifies a callback that returns to be invoked when setup is called
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="callback">Callback</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IReturnsThrows<TMock, TResult> Callback<TMock, TResult>(
            Expression<Func<TMock, TResult>> setup,
            Action callback)
            where TMock : class
        {
            return Setup(setup)
                .Callback(callback);
        }

        /// <summary>
        /// Specifies a callback with one argument that returns to be invoked when setup is called
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <typeparam name="T1">Type of first argument</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="callback">Callback</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IReturnsThrows<TMock, TResult> Callback<TMock, TResult, T1>(
            Expression<Func<TMock, TResult>> setup,
            Action<T1> callback)
            where TMock : class
        {
            return Setup(setup)
                .Callback(callback);
        }

        /// <summary>
        /// Specifies a callback with two arguments that returns to be invoked when setup is called
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <typeparam name="T1">Type of first argument</typeparam>
        /// <typeparam name="T2">Type of second argument</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="callback">Callback</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IReturnsThrows<TMock, TResult> Callback<TMock, TResult, T1, T2>(
            Expression<Func<TMock, TResult>> setup,
            Action<T1, T2> callback)
            where TMock : class
        {
            return Setup(setup)
                .Callback(callback);
        }

        /// <summary>
        /// Specifies a callback with three arguments that returns to be invoked when setup is called
        /// </summary>
        /// <typeparam name="TMock">Type of dependency</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <typeparam name="T1">Type of first argument</typeparam>
        /// <typeparam name="T2">Type of second argument</typeparam>
        /// <typeparam name="T3">Type of third argument</typeparam>
        /// <param name="setup">Setup</param>
        /// <param name="callback">Callback</param>
        /// <exception cref="ArgumentException">In case subject under test does not have such dependency</exception>
        protected IReturnsThrows<TMock, TResult> Callback<TMock, TResult, T1, T2, T3>(
            Expression<Func<TMock, TResult>> setup,
            Action<T1, T2, T3> callback)
            where TMock : class
        {
            return Setup(setup)
                .Callback(callback);
        }
    }
}
