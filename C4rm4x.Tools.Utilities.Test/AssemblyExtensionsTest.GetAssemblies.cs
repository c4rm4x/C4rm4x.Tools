#region Using

using C4rm4x.Tools.TestUtilities.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class AssemblyExtensionsTest
    {
        [TestClass]
        public class AssemblyExtensionsGetAssembliesTest
        {
            [TestMethod, UnitTest]
            public void GetAssemblies_Returns_All_The_Assemblies_From_AppDomain_When_Predicate_Is_Always_True()
            {
                var result = AppDomain.CurrentDomain.GetAssemblies(a => true);

                Assert.IsNotNull(result);
                Assert.IsTrue(result.Any());
                Assert.AreEqual(
                    AppDomain.CurrentDomain.GetAssemblies().Length,
                    result.Count());
            }

            [TestMethod, UnitTest]
            public void GetAssemblies_Returns_No_Assemblies_From_AppDomain_When_Predicate_Is_Always_False()
            {
                var result = AppDomain.CurrentDomain.GetAssemblies(a => false);

                Assert.IsNotNull(result);
                Assert.IsFalse(result.Any());
            }
        }
    }
}
