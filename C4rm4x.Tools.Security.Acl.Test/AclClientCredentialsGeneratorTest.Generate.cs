#region Using

using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.Security.Acl.Test
{
    public partial class AclClientCredentialsGeneratorTest
    {
        [TestClass]
        public class AclClientCredentialsGeneratorGenerateTest :
            AutoMockFixture<AclClientCredentialsGenerator>
        {
            [TestMethod, UnitTest]
            public void Generate_Returns_Authorization_Header_As_A_Basic_Encoded_As_Base64()
            {
                var Identifier = ObjectMother.Create<string>();

                var authorization = _sut
                    .Generate(Identifier, Resource.SecretBase64);

                Assert.IsFalse(authorization.IsNullOrEmpty());
                Assert.AreEqual(GetAuthorizationHeader(Identifier), authorization);
            }

            private static string GetAuthorizationHeader(
                string identifier)
            {
                return "Basic {0}".AsFormat(
                    "{0}:{1}".AsFormat(identifier, Resource.Secret)
                    .AsBase64());
            }
        }
    }
}
