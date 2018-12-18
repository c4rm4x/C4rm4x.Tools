using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace C4rm4x.Tools.Security.Acl.Test
{
    public partial class AclClientRequestSignerTest
    {
        [TestClass]
        public class AclClientRequestSignerSignTest :
            AutoMockFixture<AclClientRequestSigner>
        {
            [TestMethod, UnitTest]
            public void Sign_Signs_The_Object_After_Serialization_As_Json()
            {
                var objectToSign = ObjectMother.Create<TestObject>();

                var signature = _sut.Sign(objectToSign, Secret);

                Assert.AreEqual(
                    Sign(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(objectToSign))),
                    signature);
            }

            [TestMethod, UnitTest]
            public void Sign_Signs_The_Body_With_The_Secret()
            {
                var body = new byte[1024];

                var signature = _sut.Sign(body, Secret);

                Assert.AreEqual(Sign(body), signature);
            }                        

            private static string Sign(byte[] body)
            {
                using (var hmac = new HMACSHA256(Convert.FromBase64String(Secret)))
                {
                    var hash = hmac.ComputeHash(body);

                    return string
                        .Join(string.Empty, hash.Select(b => b.ToString("x2")))
                        .AsBase64();
                }
            }

            private static string Secret => Resource.SecretBase64;

            #region Helper classes

            public class TestObject
            {
                public string Property { get; private set; }
            }

            #endregion
        }
    }
}
