#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Test
{
    public partial class Base64UrlEncoderTest
    {
        [TestClass]
        public class Base64UrlEncoderEncodeTest
        {
            [TestMethod, UnitTest]
            public void Encode_Encodes_The_Given_String()
            {
                var result = Base64UrlEncoder.Encode("hello peter");

                Assert.AreEqual("aGVsbG8gcGV0ZXI", result);
            }
        }
    }
}
