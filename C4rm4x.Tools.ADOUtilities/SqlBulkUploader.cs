#region Using

using C4rm4x.Tools.Utilities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SqlCopy = System.Data.SqlClient.SqlBulkCopy;

#endregion

namespace C4rm4x.Tools.ADOUtilities
{
    /// <summary>
    /// Abstract Bulk uploader
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity to upload</typeparam>
    public abstract class SqlBulkUploader<TEntity> where TEntity : class
    {
        private readonly BulkUploadToSql<TEntity> Uploader;

        /// <summary>
        /// Gets the connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="tableName">Table name</param>
        /// <param name="batchSize">Batch size</param>
        public SqlBulkUploader(
            string connectionString,
            string tableName,
            int batchSize = 100)
        {
            connectionString.NotNullOrEmpty(nameof(connectionString));
            tableName.NotNullOrEmpty(nameof(tableName));

            ConnectionString =
                ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;

            Uploader = new BulkUploadToSql<TEntity>(tableName, batchSize);

            Uploader.Initialise(GetDataTableMapping());
        }

        /// <summary>
        /// Gets the mapping for the entity to be copied
        /// </summary>
        /// <returns></returns>
        protected abstract DataTableMapping GetDataTableMapping();

        /// <summary>
        /// Upload all the entities as part of the same batch
        /// </summary>
        /// <param name="entities"></param>
        public Task UploadAsync(IEnumerable<TEntity> entities)
        {
            entities.NotNullOrEmpty(nameof(entities));

            Load(entities);

            return FlushAsync();
        }

        private void Load(IEnumerable<TEntity> entities)
        {
            Uploader.AddRange(entities);
        }

        private async Task FlushAsync()
        {
            DataTable dataTable;

            while (Uploader.NextBatch(out dataTable))
                await WriteToDatabaseAsync(dataTable);

            Uploader.Clear();
        }

        private async Task WriteToDatabaseAsync(DataTable dataTable)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var bulkCopy = new SqlCopy(
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null);

                bulkCopy.DestinationTableName = Uploader.TableName;

                connection.Open();

                await bulkCopy.WriteToServerAsync(dataTable);

                connection.Close();
            }
        }
    }
}
