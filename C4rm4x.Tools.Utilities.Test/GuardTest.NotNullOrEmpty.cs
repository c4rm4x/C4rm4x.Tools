#region Using

using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.TestUtilities.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class GuardTest
    {
        [TestClass]
        public class GuardNotNullOrEmptyTest
        {
            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void NotNullOrEmpty_Throws_Exception_When_Argument_Is_Null()
            {
                (null as string).NotNullOrEmpty("argument");
            }

            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void NotNullOrEmpty_Throws_Exception_When_Argument_Is_Empty_String()
            {
                string.Empty.NotNullOrEmpty("argument");
            }

            [TestMethod, UnitTest]
            public void NotNullOrEmpty_Does_Not_Throw_Exception_When_Argument_Is_Neither_Null_Nor_Empty_String()
            {
                ObjectMother.Create<string>()
                    .NotNullOrEmpty("argument");
            }

            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void NotNullOrEmpty_Throws_Exception_When_Enumerable_Is_Null()
            {
                (null as IEnumerable<object>).NotNullOrEmpty("argument");
            }

            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void NotNullOrEmpty_Throws_Exception_When_Enumerable_Is_An_Empty_Collection()
            {
                new Object[] { }.NotNullOrEmpty("argument");
            }

            [TestMethod, UnitTest]
            public void NotNullOrEmpty_Does_Not_Throw_Exception_When_Enumerable_Is_Not_An_Empty_Collection()
            {
                new Object[] { new Object() }
                    .NotNullOrEmpty("argument");
            }
        }
    }
}
