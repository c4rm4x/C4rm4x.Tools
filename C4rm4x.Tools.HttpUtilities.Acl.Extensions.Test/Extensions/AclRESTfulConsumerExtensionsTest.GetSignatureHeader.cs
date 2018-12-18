using C4rm4x.Tools.Security.Acl;
using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net.Http;

namespace C4rm4x.Tools.HttpUtilities.Acl.Test
{
    public partial class AclRESTfulConsumerExtensionsTest
    {
        [TestClass]
        public class AclRESTfulConsumerExtensionsGetSignatureHeaderTest :
            AclRESTfulConsumerExtensionsFixture
        {
            private readonly AclRESTfulConsumer _sut = CreateSubjectUnderTest();

            [TestMethod, UnitTest]
            public void GetSignatureHeader_Sets_SignatureHeader_With_Object_Sign_Using_SharedSecret()
            {
                var config = _sut.GetClientConfiguration();
                var objectToSend = ObjectMother.Create<TestObject>();
                var headers = new HttpClient().DefaultRequestHeaders;
                var generator = _sut.GetSignatureHeader(config, objectToSend);

                generator(headers);

                var signatureHeader = headers.FirstOrDefault(_ => _.Key == config.SignatureHeader);

                Assert.IsNotNull(signatureHeader);
                Assert.AreEqual(
                    new AclClientRequestSigner().Sign(objectToSend, config.SecretAsBase64), 
                    signatureHeader.Value.FirstOrDefault());
            }

            #region Helper classes

            private class TestObject
            {
                public string Property { get; private set; }
            }

            #endregion
        }
    }
}
