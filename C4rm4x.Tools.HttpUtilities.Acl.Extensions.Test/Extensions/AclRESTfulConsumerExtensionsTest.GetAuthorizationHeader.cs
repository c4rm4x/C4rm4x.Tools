#region Using

using C4rm4x.Tools.HttpUtilities.Acl.Configuration;
using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl.Test
{
    public partial class AclRESTfulConsumerExtensionsTest
    {
        [TestClass]
        public class AclRESTfulConsumerExtensionsGetAuthorizationHeaderTest :
            AclRESTfulConsumerExtensionsFixture
        {
            [TestMethod, UnitTest]
            public void GetAuthorizationHeader_Returns_Authorization_Header_As_A_Basic_Encoded_As_Base64()
            {
                var config = GetConfig();

                var authorization = CreateSubjectUnderTest()
                    .GetAuthorizationHeader(config);

                Assert.IsFalse(authorization.IsNullOrEmpty());
                Assert.AreEqual(GetAuthorizationHeader(config), authorization);
            }

            private static AclRESTfulConsumerConfiguration GetConfig()
            {
                return new AclRESTfulConsumerConfiguration(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>().AsBase64());
            }

            private static string GetAuthorizationHeader(AclRESTfulConsumerConfiguration config)
            {
                return "Basic {0}".AsFormat(
                    "{0}:{1}".AsFormat(
                        config.SubscriberIdentifier,
                        config.Secret).AsBase64());
            }
        }
    }
}
