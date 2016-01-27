#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class StringExtensionsTest
    {
        [TestClass]
        public class StringExtensionsIsNullOrEmptyTest
        {
            [TestMethod, UnitTest]
            public void IsNullOrEmpty_Returns_True_When_Argument_Is_Null()
            {
                Assert.IsTrue((null as string)
                    .IsNullOrEmpty());
            }

            [TestMethod, UnitTest]
            public void IsNullOrEmpty_Returns_True_When_Argument_Is_Empty_String()
            {
                Assert.IsTrue(string.Empty.IsNullOrEmpty());
            }

            [TestMethod, UnitTest]
            public void IsNullOrEmpty_Returns_False_When_Argument_Is_Neither_Null_Nor_Empty_String()
            {
                Assert.IsFalse(ObjectMother.Create<string>()
                    .IsNullOrEmpty());
            }
        }
    }
}
