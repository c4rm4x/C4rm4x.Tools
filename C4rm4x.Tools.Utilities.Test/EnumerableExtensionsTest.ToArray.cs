#region Using

using C4rm4x.Tools.TestUtilities.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class EnumerableExtensionsTest
    {
        [TestClass]
        public class EnumerableExtensionsToArrayTest
        {
            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void ToArray_Throws_Exception_When_Source_Is_Null()
            {
                (null as IEnumerable).ToArray<Object>();
            }

            [TestMethod, UnitTest]
            public void ToArray_Returns_Empty_Array_When_No_Item_In_The_Collection_Is_Type_Of_TDestination()
            {
                var source = GetSource<string>(GetRand(10));

                Assert.IsFalse(source.ToArray<int>().Any());
            }

            [TestMethod, UnitTest]
            public void ToArray_Returns_An_Array_With_As_Many_Items_Of_Of_TDestination_Are_In_Source()
            {
                var n = GetRand(10);
                var source = GetSource<string>(n);

                Assert.AreEqual(n, source.ToArray<string>().Length);
            }
        }
    }
}
