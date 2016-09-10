#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.ServiceBus.Test
{
    public partial class BrokeredMessagesExtensionsTest
    {
        [TestClass]
        public class BrokeredMessagesExtensionsExtractContentTest
            : BrokeredMessagesExtensionsFixture
        {
            [TestMethod, UnitTest]
            public void ExtractContent_Returns_The_Original_Content_Out_Of_BrokeredMessage_Body()
            {
                var original = GetTestMessage();
                var brokeredMessage = original.BuildBrokeredMessage();
                var result = brokeredMessage.ExtractContent();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(TestMessage));
                Assert.IsTrue(original.Equals(result));
            }
        }
    }
}
