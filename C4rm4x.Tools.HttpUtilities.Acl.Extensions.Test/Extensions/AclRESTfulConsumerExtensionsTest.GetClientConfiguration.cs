#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl.Test
{
    public partial class AclRESTfulConsumerExtensionsTest
    {
        [TestClass]
        public class AclRESTfulConsumerExtensionsGetClientConfigurationTest :
            AclRESTfulConsumerExtensionsFixture
        {
            private const string BaseApiUrl = "http://www.google.com";
            private const string SubscriberIdentifier = "TestSubscriber";
            private const string SharedSecret = "aGVsbG8=";


            [TestMethod, UnitTest]
            [ExpectedException(typeof(ArgumentException))]
            public void GetClientConfiguration_Throws_ArgumentException_When_SubscriptionName_Is_Not_Found_In_App_Config()
            {
                CreateSubjectUnderTest("Non-existing")
                    .GetClientConfiguration();
            }

            [TestMethod, UnitTest]
            public void GetClientConfiguration_Returns_AclRESTfulConfiguration_Instance_Where_ApiBaseUrl_Is_Retrieved_From_App_Config()
            {
                Assert.AreEqual(
                    BaseApiUrl,
                    CreateSubjectUnderTest()
                        .GetClientConfiguration()
                        .ApiBaseUrl);
            }

            [TestMethod, UnitTest]
            public void GetClientConfiguration_Returns_AclRESTfulConfiguration_Instance_Where_SubscriberIdentifier_Is_Retrieved_From_App_Config()
            {
                Assert.AreEqual(
                    SubscriberIdentifier,
                    CreateSubjectUnderTest()
                        .GetClientConfiguration()
                        .SubscriberIdentifier);
            }

            [TestMethod, UnitTest]
            public void GetClientConfiguration_Returns_AclRESTfulConfiguration_Instance_Where_Secret_Is_Retrieved_From_App_Config_And_Decoded_From_Base64()
            {
                Assert.AreEqual(
                    SharedSecret,
                    CreateSubjectUnderTest()
                        .GetClientConfiguration()
                        .SecretAsBase64);
            }
        }
    }
}
