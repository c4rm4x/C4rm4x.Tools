#region Using

using System;

#endregion

namespace C4rm4x.Tools.ADOUtilities.Test.Infrastructure
{
    public class TestSqlBulkUploader : SqlBulkUploader<TestEntity>
    {
        public TestSqlBulkUploader() :
            base("TestEntities", "dbo.TestEntities", 100)
        {

        }
                
        protected override DataTableMapping GetDataTableMapping()
        {
            return new DataTableMapping
            {
                DataTableMappingItem.New<int>("IntProperty", "IntProperty"),
                DataTableMappingItem.New<string>("StringProperty", "StringProperty"),
                DataTableMappingItem.New<DateTime>("DateProperty", "DateProperty"),
            };
        }
    }
}
