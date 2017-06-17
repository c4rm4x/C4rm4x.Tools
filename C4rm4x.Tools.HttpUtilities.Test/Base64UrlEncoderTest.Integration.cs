#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Test
{
    public partial class Base64UrlEncoderTest
    {
        [TestClass]
        public class Base64UrlEncoderIntegrationTest
        {
            [TestMethod, IntegrationTest]
            public void Encode_And_Decode_Work_Together()
            {
                var oneWay = Base64UrlEncoder.Encode(Resources.Text);
                var twoWay = Base64UrlEncoder.Decode(oneWay);

                Assert.AreEqual(Resources.Text, twoWay);
            }
        }
    }
}
