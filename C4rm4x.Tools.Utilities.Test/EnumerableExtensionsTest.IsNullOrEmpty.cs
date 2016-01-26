#region Using

using C4rm4x.Tools.TestUtilities.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class EnumerableExtensionsTest
    {
        [TestClass]
        public class EnumerableExtensionsIsNullOrEmptyTest
        {
            [TestMethod, UnitTest]
            public void IsNullOrEmpty_Returns_True_When_Enumerable_Is_Null()
            {
                Assert.IsTrue((null as IEnumerable<object>)
                    .IsNullOrEmpty());
            }

            [TestMethod, UnitTest]
            public void IsNullOEmpty_Returns_True_When_Enumerable_Has_No_Elements()
            {
                Assert.IsTrue(new Object[] { }.IsNullOrEmpty());
            }

            [TestMethod, UnitTest]
            public void IsNullOrEmpty_Returns_False_When_Enumerable_Has_At_Least_One_Element()
            {
                Assert.IsFalse(new Object[] { new Object() }
                    .IsNullOrEmpty());
            }
        }
    }
}
