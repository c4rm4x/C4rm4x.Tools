#region Using

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

#endregion

namespace C4rm4x.Tools.ADOUtilities.Test.Infrastructure
{
    public class TestEntities : DbContext
    {
        public TestEntities()
            : base("name=TestEntities")
        {
            Database.SetInitializer<TestEntities>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
