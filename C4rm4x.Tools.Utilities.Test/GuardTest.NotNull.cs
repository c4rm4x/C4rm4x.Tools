#region Using

using C4rm4x.Tools.TestUtilities.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class GuardTest
    {
        [TestClass]
        public class GuardNotNullTest
        {
            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void NotNull_Throws_Exception_When_Argument_Is_Null()
            {
                (null as object).NotNull("argument");
            }

            [TestMethod, UnitTest]
            public void NotNull_Does_Not_Throw_Exception_When_Argument_Is_Not_Null()
            {
                new Object().NotNull("argument");
            }
        }
    }
}
