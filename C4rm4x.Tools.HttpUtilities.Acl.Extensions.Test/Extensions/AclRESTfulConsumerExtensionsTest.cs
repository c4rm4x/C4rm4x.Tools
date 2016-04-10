#region Using

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.HttpUtilities.Acl.Test
{
    public partial class AclRESTfulConsumerExtensionsTest
    {
        [TestClass]
        public abstract class AclRESTfulConsumerExtensionsFixture
        {
            protected const string Name = "Test";

            #region Helper classes

            protected class TestAclRESTfulConsumer : AclRESTfulConsumer
            {
                public TestAclRESTfulConsumer(string name)
                    : base(name)
                { }
            }

            #endregion

            protected static AclRESTfulConsumer CreateSubjectUnderTest(
                string name = Name)
            {
                return new TestAclRESTfulConsumer(name);
            }
        }
    }
}
