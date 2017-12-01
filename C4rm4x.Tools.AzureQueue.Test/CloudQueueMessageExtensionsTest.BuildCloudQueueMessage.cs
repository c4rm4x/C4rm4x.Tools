#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Queue;

#endregion

namespace C4rm4x.Tools.AzureQueue.Test
{
    public partial class CloudQueueMessageExtensionsTest
    {
        [TestClass]
        public class CloudQueueMessageExtensionsBuildCloudQueueMessageTest
            : CloudQueueMessageExtensionsFixture
        {
            [TestMethod, UnitTest]
            public void BuildCloudQueueMessage_Returns_A_New_Instance_Of_CloudQueueMessage()
            {
                var message = GetTestMessage().BuildCloudQueueMessage();

                Assert.IsNotNull(message);
                Assert.IsInstanceOfType(message, typeof(CloudQueueMessage));
            }
        }
    }
}
