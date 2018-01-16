#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.Cryptography.Test
{
    public partial class CryptoExtensionsTest
    {
        [TestClass]
        public class CryptoExtensionsEncryptTest :
            CryptoExtensionsFixture
        {
            [TestMethod, IntegrationTest]
            public void Encrypt_Encryps_The_String_Using_Given_Keys()
            {
                var str = ObjectMother.Create<string>();

                var encryptedData = str.Encrypt(PublicKey);

                var result = encryptedData.Decrypt(PrivateKey);

                Assert.IsNotNull(result);
                Assert.IsTrue(str.Equals(result));
            }
        }
    }
}
