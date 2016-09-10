#region Using

using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;

#endregion

namespace C4rm4x.Tools.ServiceBus.Test
{
    public partial class BrokeredMessagesExtensionsTest
    {
        [TestClass]
        public abstract class BrokeredMessagesExtensionsFixture
        {
            [DataContract]
            public class TestMessage
            {
                [DataMember(IsRequired = true)]
                public string Value { get; private set; }

                public TestMessage()
                {

                }

                public TestMessage(string value)
                {
                    Value = value;
                }

                public override bool Equals(object obj)
                {
                    var objAsTestMessage = obj as TestMessage;

                    return Value == objAsTestMessage.Value;
                }

                public override int GetHashCode()
                {
                    return base.GetHashCode();
                }
            }

            protected static TestMessage GetTestMessage()
            {
                return new TestMessage(ObjectMother.Create<string>());
            }
        }
    }
}
