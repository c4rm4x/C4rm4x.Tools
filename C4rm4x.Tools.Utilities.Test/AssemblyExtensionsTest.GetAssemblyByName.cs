#region Using

using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.TestUtilities.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class AssemblyExtensionsTest
    {
        [TestClass]
        public class AssemblyExtensionsGetAssemblyByNameTest
        {
            [TestMethod, UnitTest]
            [ExpectedException(typeof(InvalidOperationException))]
            public void GetAssemblyByName_Throws_Exception_When_The_Assembly_With_The_Specified_Name_Has_Not_Been_Yet_Loaded()
            {
                AppDomain.CurrentDomain.GetAssemblyByName(ObjectMother.Create<string>());
            }

            [TestMethod, UnitTest]
            public void GetAssemblyByName_Returns_Assembly_Whose_Name_Is_The_Same_Than_The_Specified_One()
            {
                const string System = "System";

                var result = AppDomain.CurrentDomain.GetAssemblyByName(System);

                Assert.IsNotNull(result);
                Assert.AreEqual(System, result.GetName().Name);
            }
        }
    }
}
