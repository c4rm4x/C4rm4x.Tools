#region Using

using C4rm4x.Tools.ADOUtilities.Test.Infrastructure;
using C4rm4x.Tools.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace C4rm4x.Tools.ADOUtilities.Test
{
    public partial class SqlBulkUploaderTest
    {
        [TestClass]
        public abstract class SqlBulkUploaderFixture :
            IntegrationFixture<TestSqlBulkUploader, TestEntities>
        {
            [TestInitialize]
            public override void Setup()
            {
                base.Setup();

                GetInstance<TestEntities>()
                    .Database
                    .ExecuteSqlCommand("CREATE TABLE TestEntities (IntProperty INT, StringProperty NVARCHAR(MAX), DateProperty DATETIME)");
            }

            [TestCleanup]
            public override void Cleanup()
            {
                GetInstance<TestEntities>()
                    .Database
                    .ExecuteSqlCommand("DROP TABLE TestEntities");

                base.Cleanup();
            }
        }
    }
}
