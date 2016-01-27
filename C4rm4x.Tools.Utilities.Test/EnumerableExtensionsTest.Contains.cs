#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class EnumerableExtensionsTest
    {
        [TestClass]
        public class EnumerableExtensionsContainsTest
        {
            [TestMethod, UnitTest]
            public void Contains_Returns_False_When_Enumerable_Is_Null()
            {
                Assert.IsFalse(
                    (null as IEnumerable<string>)
                        .Contains(ObjectMother.Create<string>()));
            }

            [TestMethod, UnitTest]
            public void Contains_Returns_False_When_Enumerable_Is_Empty_Collection()
            {
                Assert.IsFalse(
                    new string[] { }
                        .Contains(ObjectMother.Create<string>()));
            }

            [TestMethod, UnitTest]
            public void Contains_Returns_False_When_Enumerable_Does_Not_Contain_The_Value()
            {
                const string Value = "ValueToBeFound";

                Assert.IsFalse(GetSource().Contains(Value));
            }

            [TestMethod, UnitTest]
            public void Contains_Returns_True_When_Enumerable_Contains_The_Value()
            {
                const string Value = "ValueToBeFound";

                Assert.IsTrue(GetSource(Value).Contains(Value));
            }

            [TestMethod, UnitTest]
            public void Contains_Returns_False_When_Enumerable_Does_Not_Contain_The_Exact_Value_And_StringComparison_Is_Case_Sensitive()
            {
                const string Value = "ValueToBeFound";

                Assert.IsFalse(GetSource(Value.ToLower()).Contains(Value, StringComparison.CurrentCulture));
            }

            private static IEnumerable<string> GetSource(
                params string[] values)
            {
                var result = new List<string>(values);

                result.AddRange(GetSource<string>(GetRand(10)).ToArray());

                return result;
            }
        }
    }
}
