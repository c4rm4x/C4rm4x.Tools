#region Using

using C4rm4x.Tools.Auditing.Comparators;
using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;

#endregion

namespace C4rm4x.Tools.Auditing.Test
{
    public partial class AuditTrailsTest
    {
        [TestClass]
        public class AudiTrailsLogComplexPropertiesTest
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
                public ComplexClass ComplexProperty { get; private set; }

                public TestClass(ComplexClass complexProperty)
                {
                    ComplexProperty = complexProperty;
                }
            }

            private class ComplexClassComparator: 
                AbstractComparator<ComplexClass>
            {
                private readonly bool _match;

                public ComplexClassComparator(bool match)
                {
                    _match = match;
                }

                public override bool Match(
                    ComplexClass original, 
                    ComplexClass current)
                {
                    return _match;
                }
            }

            #endregion

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_ComplexProperty_Value_Differs_From_Current_ComplexProperty_Value()
            {
                var original = GetObject(ObjectMother.Create<string>());
                var current = GetObject(ObjectMother.Create<string>());

                var traces = Log(original, current);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(ComplexClass), trace.PropertyType);
                Assert.AreEqual("ComplexProperty", trace.PropertyName);
                Assert.AreEqual(original.ComplexProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_0_Trace_When_Original_ComplexProperty_Value_Does_Not_Differ_From_Current_ComplexProperty_Value()
            {
                var value = ObjectMother.Create<string>();
                var original = GetObject(value);
                var current = GetObject(value);

                Assert.IsFalse(Log(original, current).Any());
            }

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_ComplexProperty_Value_Differs_From_Current_ComplexProperty_Value_Based_On_Comparator_Used()
            {
                var original = GetObject(ObjectMother.Create<string>());
                var current = GetObject(ObjectMother.Create<string>());

                var traces = Log(original, current, false);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(ComplexClass), trace.PropertyType);
                Assert.AreEqual("ComplexProperty", trace.PropertyName);
                Assert.AreEqual(original.ComplexProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_0_Trace_When_Original_ComplexProperty_Value_Does_Not_Differ_From_Current_ComplexProperty_Value_Based_On_Comparator_Used()
            {
                var value = ObjectMother.Create<string>();
                var original = GetObject(value);
                var current = GetObject(value);

                Assert.IsFalse(Log(original, current, true).Any());
            }

            private static TestClass GetObject(string value)
            {
                return new TestClass(new ComplexClass(value));
            }

            private static IEnumerable<Trace> Log(
                TestClass original,
                TestClass current,
                params Func<IComparator>[] comparators)
            {
                return new AuditTrails(
                        new ComparisonConfiguration(comparators: comparators))
                    .Log(original, current);
            }

            private static IEnumerable<Trace> Log(
                TestClass original,
                TestClass current,
                bool match)
            {
                return Log(original, current, () => new ComplexClassComparator(match));
            }
        }
    }
}
