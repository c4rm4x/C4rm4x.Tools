#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.AzureQueue.Test
{
    public partial class CloudQueueMessageExtensionsTest
    {
        [TestClass]
        public class CloudQueueMessageExtensionsExtractContentTest
            : CloudQueueMessageExtensionsFixture
        {
            [TestMethod, UnitTest]
            public void ExtractContent_Returns_The_Original_Content_Out_Of_CloudQueueMessage_Body()
            {
                var original = GetTestMessage();
                var queueMessage = original.BuildCloudQueueMessage();
                var result = queueMessage.ExtractContent();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(TestMessage));
                Assert.IsTrue(original.Equals(result));
            }
        }
    }
}
