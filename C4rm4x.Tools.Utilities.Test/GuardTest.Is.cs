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
        public class GuardIsTest
        {
            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void Is_Throws_Exception_When_Type_Is_Not_Compatible_Whith_Given_Type()
            {
                typeof(string).Is<int>();
            }

            [TestMethod, UnitTest]
            public void Is_Does_Not_Throw_Exception_When_Type_And_Given_Type_Are_Compatible()
            {
                typeof(string).Is<object>();
            }
        }
    }
}
