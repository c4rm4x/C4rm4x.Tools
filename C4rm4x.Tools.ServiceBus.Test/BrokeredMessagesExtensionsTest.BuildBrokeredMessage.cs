#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.ServiceBus.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

#endregion

namespace C4rm4x.Tools.ServiceBus.Test
{
    public partial class BrokeredMessagesExtensionsTest
    {
        [TestClass]
        public class BrokeredMessagesExtensionsBuildBrokeredMessageTest :
            BrokeredMessagesExtensionsFixture
        {
            [TestMethod, UnitTest]
            public void BuildBrokeredMessage_Returns_A_New_Instance_Of_BrokeredMessage()
            {
                var result = GetTestMessage().BuildBrokeredMessage();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(BrokeredMessage));
            }

            [TestMethod, UnitTest]
            public void BuildBrokeredMessage_Returns_A_New_Instance_Of_BrokeredMessage_With_ContentTye_As_AssemblyQualifyName()
            {
                var result = GetTestMessage().BuildBrokeredMessage();

                Assert.AreEqual(typeof(TestMessage).AssemblyQualifiedName, result.ContentType);
            }

            [TestMethod, UnitTest]
            public void BuildBrokeredMessage_Returns_A_New_Instance_Of_BrokeredMessage_With_New_Guid_As_MessageId()
            {
                Guid messageId;
                var result = GetTestMessage().BuildBrokeredMessage();

                Assert.IsTrue(Guid.TryParse(result.MessageId, out messageId));
                Assert.IsFalse(Guid.Empty.Equals(messageId));
            }
        }
    }
}
