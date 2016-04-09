#region Using

using C4rm4x.Tools.Auditing.Comparators;
using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.Auditing.Test.Comparators
{
    public partial class DateTimeComparatorTest
    {
        [TestClass]
        public class DateTimeComparatorMatchTest
        {
            [TestMethod, UnitTest]
            public void Match_Returns_True_When_Both_DateTime_Are_The_Same_Regardless_The_Format()
            {
                var value = ObjectMother.Create<DateTime>();

                Assert.IsTrue(
                    Match(value, value));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_False_When_Original_And_Current_Are_Different_With_The_Default_Format()
            {
                Assert.IsFalse(
                    Match(
                        ObjectMother.Create<DateTime>(),
                        ObjectMother.Create<DateTime>()));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_True_When_Both_Original_And_Current_DateTime_Only_Differs_In_Seconds_Component_With_Default_Format()
            {
                var original = ObjectMother.Create<DateTime>();
                var current = new DateTime(
                    original.Year,
                    original.Month,
                    original.Day,
                    original.Hour,
                    original.Minute,
                    ObjectMother.Create<int>() % 60);

                Assert.IsTrue(
                    Match(original, current));
            }

            [TestMethod, UnitTest]
            public void Match_Returns_True_When_Both_Original_And_Current_DateTime_Has_Same_String_Format()
            {
                var original = ObjectMother.Create<DateTime>();
                var current = new DateTime(
                    original.Year,
                    original.Month,
                    original.Day);

                Assert.IsTrue(
                    Match(original, current, "yyyy-MM-dd"));
            }

            private static bool Match(
                DateTime original, 
                DateTime current,
                string format = DateTimeComparator.DefaultFormat)
            {
                return new DateTimeComparator(format)
                    .Match(original, current);
            }
        }
    }
}
