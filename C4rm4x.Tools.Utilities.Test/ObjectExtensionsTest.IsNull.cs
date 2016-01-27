#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class ObjectExtensionsTest
    {
        [TestClass]
        public class ObjectExtensionsIsNullTest
        {
            [TestMethod, UnitTest]
            public void IsNull_Returns_True_When_Object_Is_Null()
            {
                Assert.IsTrue((null as object).IsNull());
            }

            [TestMethod, UnitTest]
            public void IsNull_Returns_False_When_Object_Is_Not_Null()
            {
                Assert.IsFalse(new Object().IsNull());
            }
        }
    }
}
