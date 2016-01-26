#region Using

using C4rm4x.Tools.TestUtilities.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class ObjectExtensionsTest
    {
        [TestClass]
        public class ObjectExtensionsIsNotNullTest
        {
            [TestMethod, UnitTest]
            public void IsNotNull_Returns_False_When_Object_Is_Null()
            {
                Assert.IsFalse((null as object).IsNotNull());
            }

            [TestMethod, UnitTest]
            public void IsNotNull_Returns_True_When_Object_Is_Not_Null()
            {
                Assert.IsTrue(new Object().IsNotNull());
            }
        }
    }
}
