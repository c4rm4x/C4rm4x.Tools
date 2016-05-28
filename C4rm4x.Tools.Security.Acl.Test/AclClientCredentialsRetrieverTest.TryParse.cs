#region Using

using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;

#endregion

namespace C4rm4x.Tools.Security.Acl.Test
{
    public class AclClientCredentialsRetrieverTest
    {
        [TestClass]
        public class AclClientCredentialsRetrieverTryParseTest
            : AutoMockFixture<AclClientCredentialsRetriever>
        {
            private const string ValidAuthorizationHeader = "U3Vic2NyaWJlcjpoZWxsbw=="; // Subscriber:hello

            [TestMethod, UnitTest]
            public void TryParse_Returns_False_When_Authorization_Header_Is_Not_A_Valid_Basic_One()
            {
                var request = GetHttpRequestMessasge(ObjectMother.Create<string>());
                AclClientCredentials credentials;

                Assert.IsFalse(_sut.TryParse(request, out credentials));
            }

            [TestMethod, UnitTest]
            public void TryParse_Returns_True_When_A_Valid_Authorization_Header_Is_Contained()
            {
                var request = GetHttpRequestMessasge(ValidAuthorizationHeader);
                AclClientCredentials credentials;

                Assert.IsTrue(_sut.TryParse(request, out credentials));
            }

            [TestMethod, UnitTest]
            public void TryParse_Sets_Credentials_With_A_New_Instance_Of_AclClientCredentials_When_A_Valid_Authorization_Header_Is_Contained()
            {
                var request = GetHttpRequestMessasge(ValidAuthorizationHeader);
                AclClientCredentials credentials;

                _sut.TryParse(request, out credentials);

                Assert.IsNotNull(credentials);
                Assert.AreEqual("Subscriber", credentials.Identifier);
                Assert.AreEqual("hello", credentials.Secret);
            }

            private static HttpRequestMessage GetHttpRequestMessasge(
                string authorization)
            {
                var result = new HttpRequestMessage();

                result.Headers.Authorization =
                    AuthenticationHeaderValue.Parse(
                        "Basic {0}".AsFormat(authorization));

                return result;
            }
        }
    }
}
