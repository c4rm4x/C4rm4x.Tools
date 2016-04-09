#region Using

using C4rm4x.Tools.Auditing.Comparators;
using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.Auditing.Test.Comparators
{
    public partial class StringComparatorTest
    {
        [TestClass]
        public class StringComparatorMatchTest
        {
            [TestMethod, UnitTest]
            public void Match_Returns_False_When_Both_Instances_Are_Null_And_NullEquality_Is_False()
            {
                Assert.IsFalse(
                    Match(null, null, nullEquality: false));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_True_When_Both_Instances_Are_Null_And_NullEquality_Is_True()
            {
                Assert.IsTrue(
                    Match(null, null, nullEquality: true));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_False_When_Original_Is_Null_But_Current_Not()
            {
                Assert.IsFalse(
                    Match(null, ObjectMother.Create<string>()));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_False_When_Original_Is_Not_Null_But_Current_Is()
            {
                Assert.IsFalse(
                    Match(ObjectMother.Create<string>(), null));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_True_When_Both_Original_And_Current_Are_Equal_Regardless_The_StringComparison()
            {
                var value = ObjectMother.Create<string>();

                Assert.IsTrue(Match(value, value));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_False_When_Original_And_Current_Are_Different_Regardless_The_StringComparison()
            {
                Assert.IsFalse(Match(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>()));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_True_When_Both_Original_And_Current_Only_Differ_In_Case_But_StringComparison_Is_InvariantCultureIgnoreCase()
            {
                var value = ObjectMother.Create<string>();

                Assert.IsTrue(
                    Match(value, value.ToLower(), StringComparison.InvariantCultureIgnoreCase));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_False_When_Both_Original_And_Current_Only_Differ_In_Case_And_StringComparison_Is_InvariantCulture()
            {
                var value = ObjectMother.Create<string>();

                Assert.IsFalse(
                    Match(value, value.ToLower(), StringComparison.InvariantCulture));
            }

            private static bool Match(
                string original,
                string current,
                StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase,
                bool nullEquality = true)
            {
                return new StringComparator(stringComparison, nullEquality)
                    .Match(original, current);
            } 
        }
    }
}
