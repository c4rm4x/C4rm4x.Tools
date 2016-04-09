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
        public class AuditTrailsLogBindingsTest
        {
            #region Helper classes

            private class BaseClass
            {
                public string BaseProperty { get; private set; }

                public BaseClass(string baseProperty)
                {
                    BaseProperty = baseProperty;
                }
            }

            private class TestClass : BaseClass
            {
                internal string NonPublicProperty { get; set; }

                public string PublicProperty { get; private set; }

                public TestClass(
                    string publicProperty,
                    string nonPublicProperty,
                    string baseProperty)
                    : base(baseProperty)
                {
                    PublicProperty = publicProperty;
                    NonPublicProperty = nonPublicProperty;
                }
            }

            #endregion

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_NonPublicProperty_Value_Differs_From_Current_NonPublicProperty_Value_And_Configuration_IgnorePrivateProperties_Is_False()
            {
                var original = GetObject(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>());
                var current = GetObject(
                    original.PublicProperty,
                    ObjectMother.Create<string>(),
                    original.BaseProperty);

                var traces = Log(original, current, ignorePrivateProperties: false);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(string), trace.PropertyType);
                Assert.AreEqual("NonPublicProperty", trace.PropertyName);
                Assert.AreEqual(original.NonPublicProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_0_Trace_When_Original_NonPublicProperty_Value_Differs_From_Current_NonPublicProperty_Value_And_Configuration_IgnorePrivateProperties_Is_True()
            {
                var original = GetObject(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>());
                var current = GetObject(
                    original.PublicProperty,
                    ObjectMother.Create<string>(),
                    original.BaseProperty);

                Assert.IsFalse(
                    Log(original, current, ignorePrivateProperties: true).Any());
            }

            [TestMethod, UnitTest]
            public void Log_Returns_1_Trace_When_Original_BaseProperty_Value_Differs_From_Current_BaseProperty_Value_And_Configuration_IgnoreInheritedProperties_Is_False()
            {
                var original = GetObject(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>());
                var current = GetObject(
                    original.PublicProperty,
                    original.NonPublicProperty,
                    ObjectMother.Create<string>());

                var traces = Log(original, current, ignoreInheritedProperties: false);

                Assert.IsTrue(traces.Any());

                var trace = traces.First();
                Assert.IsNotNull(trace);
                Assert.AreEqual(typeof(string), trace.PropertyType);
                Assert.AreEqual("BaseProperty", trace.PropertyName);
                Assert.AreEqual(original.BaseProperty, trace.OriginalValue);
            }

            [TestMethod, UnitTest]
            public void Log_Returns_0_Trace_When_Original_BaseProperty_Value_Differs_From_Current_BaseProperty_Value_And_Configuration_IgnoreInheritedProperties_Is_True()
            {
                var original = GetObject(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>());
                var current = GetObject(
                    original.PublicProperty,
                    original.NonPublicProperty,
                    ObjectMother.Create<string>());

                Assert.IsFalse(
                    Log(original, current, ignoreInheritedProperties: true).Any());
            }

            private static IEnumerable<Trace> Log(
                TestClass original,
                TestClass current,
                bool ignorePrivateProperties = true,
                bool ignoreInheritedProperties = true)
            {
                return new AuditTrails(
                    new ComparisonConfiguration(
                        ignoreInheritedProperties, ignorePrivateProperties))
                    .Log(original, current);
            }

            private static TestClass GetObject(
                string publicProperty,
                string nonPublicProperty,
                string baseProperty)
            {
                return new TestClass(publicProperty, nonPublicProperty, baseProperty);
            }
        }
    }
}
