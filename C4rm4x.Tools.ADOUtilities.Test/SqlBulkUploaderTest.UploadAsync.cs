#region Using

using C4rm4x.Tools.ADOUtilities.Test.Infrastructure;
using C4rm4x.Tools.TestUtilities;
using C4rm4x.Tools.TestUtilities.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Tools.ADOUtilities.Test
{
    public partial class SqlBulkUploaderTest
    {
        [TestClass]
        public class SqlBulkUploaderUploadAsyncTest :
            SqlBulkUploaderFixture
        {
            [TestMethod, IntegrationTest]
            public async Task UploadAsync_Pushes_All_The_Entities_Into_Database()
            {
                await _sut.UploadAsync(BuilderCollection
                    .Generate(() => new TestEntityBuilder().Build(), 10000, 25000)
                    .ToArray());
            }
        }
    }
}
