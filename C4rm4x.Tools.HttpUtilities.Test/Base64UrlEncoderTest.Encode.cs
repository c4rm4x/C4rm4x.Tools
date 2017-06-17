#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Test
{
    public partial class Base64UrlEncoderTest
    {
        [TestClass]
        public class Base64UrlEncoderDecodeTest
        {
            [TestMethod, UnitTest]
            public void Decode_Decodes_The_Given_String()
            {
                var result = Base64UrlEncoder.Decode("aGVsbG8gcGV0ZXI");

                Assert.AreEqual("hello peter", result);
            }
        }
    }
}
