#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class GuardTest
    {
        [TestClass]
        public class GuardIsEnumTest
        {
            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void IsEnum_Throws_Exception_When_Type_Is_Not_A_Valid_System_Enum_Type()
            {
                typeof(TestClass).IsEnum();
            }

            [TestMethod, UnitTest]
            public void IsEnum_Does_Not_Throw_Exception_When_Type_Is_A_Valid_System_Enum_Type()
            {
                typeof(EnumTest).IsEnum();
            }

            #region Helper classes

            class TestClass
            { }

            enum EnumTest
            { }

            #endregion
        }
    }
}
