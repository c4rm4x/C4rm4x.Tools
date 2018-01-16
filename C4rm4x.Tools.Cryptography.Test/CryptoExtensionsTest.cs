#region Using

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

#endregion

namespace C4rm4x.Tools.Cryptography.Test
{
    public partial class CryptoExtensionsTest
    {        
        [TestClass]
        public abstract class CryptoExtensionsFixture
        {
            public string PublicKey { get; private set; }

            public string PrivateKey { get; private set; }

            [TestInitialize]
            public void Setup()
            {
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.PersistKeyInCsp = false;

                    PublicKey = rsa.ToXmlString(false);
                    PrivateKey = rsa.ToXmlString(true);
                }
            }

            #region Helpers

            protected class TestClass
            {
                public string TestProperty { get; private set; }

                private TestClass()
                {

                }

                public TestClass(string testProperty)
                {
                    TestProperty = testProperty;
                }

                public override bool Equals(object obj)
                {
                    var thisObj = obj as TestClass;

                    return this.TestProperty.Equals(thisObj?.TestProperty);
                }

                public override int GetHashCode()
                {
                    return base.GetHashCode();
                }
            }

            #endregion
        }
    }
}
