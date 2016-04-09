#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Auditing.Test
{
    public partial class AuditTrailsTest
    {
        [TestClass]
        public class AuditTrailsLogEnumerablesTest
        {
            #region Helper classes

            private class ComplexClass
            {
                public string Property { get; private set; }

                public ComplexClass(string property)
                {
                    Property = property;
                }
            }

            private class TestClass
            {
                public IEnumerable<ComplexClass> EnumerableProperty { get; private set; }

                public TestClass(params ComplexClass[] complexes)
                {
                    EnumerableProperty = complexes;
                }
            }

            #endregion

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_EnumerableProperty_Value_Is_Longer_Than_Current_EnumerableProperty_Value()
            {
                var numberOfItems = GetRand(2, 10);
                var original = GetObject(GetComplexes(numberOfItems).ToArray());
                var current = GetObject(GetComplexes(numberOfItems - 1).ToArray());

                var traces = Log(original, current);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(IEnumerable<ComplexClass>), trace.PropertyType);
                Assert.AreEqual("EnumerableProperty", trace.PropertyName);
                Assert.AreEqual(original.EnumerableProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_EnumerableProperty_Value_Is_Shorter_Than_Current_EnumerableProperty_Value()
            {
                var numberOfItems = GetRand(2, 10);
                var original = GetObject(GetComplexes(numberOfItems - 1).ToArray());
                var current = GetObject(GetComplexes(numberOfItems).ToArray());

                var traces = Log(original, current);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(IEnumerable<ComplexClass>), trace.PropertyType);
                Assert.AreEqual("EnumerableProperty", trace.PropertyName);
                Assert.AreEqual(original.EnumerableProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_EnumerableProperty_Value_Length_Is_Equal_Current_EnumerableProperty_Value_Length_But_There_Are_Elements_Present_In_Origiginal_That_They_Are_Not_In_Current()
            {
                var numberOfItems = GetRand(2, 10);
                var original = GetObject(GetComplexes(numberOfItems).ToArray());
                var current = GetObject(GetComplexes(numberOfItems).ToArray());

                var traces = Log(original, current);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(IEnumerable<ComplexClass>), trace.PropertyType);
                Assert.AreEqual("EnumerableProperty", trace.PropertyName);
                Assert.AreEqual(original.EnumerableProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_0_Trace_When_Original_EnumerableProperty_Value_Length_Is_Equal_Current_EnumerableProperty_Value_Length_And_All_Elements_Are_Present_In_Both()
            {
                var complexes = GetComplexes(GetRand(1, 10)).ToArray();
                var original = GetObject(complexes);
                var current = GetObject(complexes);

                Assert.IsFalse(Log(original, current).Any());
            }

            private static TestClass GetObject(params ComplexClass[] complexes)
            {
                return new TestClass(complexes);
            }

            private static IEnumerable<ComplexClass> GetComplexes(
                int numberOfComplexes)
            {
                for (var i = 0; i < numberOfComplexes; i++)
                    yield return new ComplexClass(ObjectMother.Create<string>());
            }

            private static int GetRand(
                int min,
                int max)
            {
                return new Random().Next(min, max);
            }

            private static IEnumerable<Trace> Log(
                TestClass original,
                TestClass current)
            {
                return new AuditTrails()
                    .Log(original, current);
            }
        }
    }
}
