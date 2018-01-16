#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.Cryptography.Test
{
    public partial class CryptoExtensionsTest
    {
        [TestClass]
        public class CryptoExtensionsEncryptObjectTest :
            CryptoExtensionsFixture
        {
            [TestMethod, IntegrationTest]
            public void EncryptObject_Encryps_The_Object_Using_Given_Keys()
            {
                var obj = ObjectMother.Create<TestClass>();

                var encryptedData = obj.EncryptObject(PublicKey);

                var result = encryptedData.DecryptObject<TestClass>(PrivateKey);

                Assert.IsNotNull(result);
                Assert.IsTrue(obj.Equals(result));
            }
        }
    }
}
