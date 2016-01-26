#region Using

using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.TestUtilities.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class EnumExtensionsTest
    {
        [TestClass]
        public class EnumExtensionsParseTest
        {
            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void GetEnumValue_Throws_Exception_When_Type_Is_Not_A_Valid_System_Enum_Type()
            {
                ObjectMother.Create<string>().GetEnumValue<TestClass>();
            }

            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void GetEnumValue_Throws_Exception_When_Value_Is_Not_A_Valid_Enum_Value_Representation()
            {
                ObjectMother.Create<string>().GetEnumValue<TestEnum>();
            }

            [TestMethod, UnitTest]
            public void GetEnumValue_Returns_Equivalent_Value_When_Value_Is_A_Valid_Enum_Value_Representation()
            {
                Assert.AreEqual(
                    TestEnum.Value,
                    TestEnum.Value.ToString().GetEnumValue<TestEnum>());
            }

            #region Helper classes

            struct TestClass { }

            enum TestEnum
            {
                Value,
            }

            #endregion
        }
    }
}
