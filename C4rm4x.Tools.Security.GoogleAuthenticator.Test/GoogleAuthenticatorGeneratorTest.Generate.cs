using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;

namespace C4rm4x.Tools.Security.GoogleAuthenticator.Test
{
    public partial class GoogleAuthenticatorGeneratorTest
    {
        [TestClass]
        public class GoogleAuthenticatorGeneratorGenerateTest
        {
            [TestMethod, UnitTest]
            public void Generate_Generates_Qr_Chart_Url()
            {
                var result = GoogleAuthenticatorGenerator.Generate(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>());

                Assert.IsFalse(result.QrCodeImageUrl.IsNullOrEmpty());
            }

            [TestMethod, UnitTest]
            public void Generate_Sets_Width_And_Height_With_Given_Values()
            {
                var qrCodeWidth = ObjectMother.Create<byte>();
                var qrCodeHeight = ObjectMother.Create<byte>();

                var result = GoogleAuthenticatorGenerator.Generate(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    qrCodeWidth,
                    qrCodeHeight);

                var chs = HttpUtility.ParseQueryString(result.QrCodeImageUrl).Get("chs");

                Assert.AreEqual($"{qrCodeWidth}x{qrCodeHeight}", chs);
            }

            [TestMethod, UnitTest]
            public void Generate_Generates_Alternative_Code()
            {
                var result = GoogleAuthenticatorGenerator.Generate(
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>(),
                    ObjectMother.Create<string>());

                Assert.IsFalse(result.ManualEntryKey.IsNullOrEmpty());
            }            
        }
    }
}
