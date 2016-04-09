#region Using

using C4rm4x.Tools.Auditing.Comparators;
using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Auditing.Test
{
    public partial class AuditTrailsTest
    {
        [TestClass]
        public class AuditTrailsLogComparatorsTest
        {
            #region Helper classes

            private class TestClass
            {
                public string PublicProperty { get; private set; }

                public TestClass(string publicProperty)
                {
                    PublicProperty = publicProperty;
                }
            }

            private class TestComparator : 
                AbstractComparator<string>
            {
                private readonly bool _match;

                public TestComparator(bool match)
                {
                    _match = match;
                }

                public override bool Match(
                    string original, 
                    string current)
                {
                    return _match;
                }
            }

            #endregion

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_PublicProperty_Value_Differs_From_Current_PublicProperty_Value_Base_On_Comparator_Used()
            {
                var original = GetObject(ObjectMother.Create<string>());
                var current = GetObject(ObjectMother.Create<string>());

                var traces = Log(original, current, match: false);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(string), trace.PropertyType);
                Assert.AreEqual("PublicProperty", trace.PropertyName);
                Assert.AreEqual(original.PublicProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_0_Trace_When_Original_PublicProperty_Value_Does_Not_Differ_From_Current_PublicProperty_Value_Base_On_Comparator_Used()
            {
                var original = GetObject(ObjectMother.Create<string>());
                var current = GetObject(ObjectMother.Create<string>());

                Assert.IsFalse(Log(original, current, match: true).Any());
            }

            private static TestClass GetObject(string publicProperty)
            {
                return new TestClass(publicProperty);
            }

            private static IEnumerable<Trace> Log(
                TestClass original,
                TestClass current,
                bool match)
            {
                return new AuditTrails(
                    new ComparisonConfiguration(
                        comparators: () => new TestComparator(match)))
                    .Log(original, current);
            }
        }
    }
}
