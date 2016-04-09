#region Using

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
        public class AuditTrailsLogBasicTest
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

            #endregion

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_PublicProperty_Value_Differs_From_Current_PublicProperty_Value()
            {
                var original = GetObject(ObjectMother.Create<string>());
                var current = GetObject(ObjectMother.Create<string>());

                var traces = Log(original, current);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(string), trace.PropertyType);
                Assert.AreEqual("PublicProperty", trace.PropertyName);
                Assert.AreEqual(original.PublicProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_0_Trace_When_Both_Original_And_Current_PublicProperty_Values_Are_Equal()
            {
                var value = ObjectMother.Create<string>();
                var original = GetObject(value);
                var current = GetObject(value);

                Assert.IsFalse(Log(original, current).Any());
            }

            private static TestClass GetObject(string value)
            {
                return new TestClass(value);
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
