#region Using

using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Tools.ADOUtilities
{
    internal class InternalSqlCommand : IDisposable
    {
        public SqlCommand InnerCommand { get; private set; }

        public InternalSqlCommand(string function, InternalSqlConnection connection)
        {
            InnerCommand = new SqlCommand(function, connection.InnerConnection);
        }

        public void Dispose()
        {
            InnerCommand.Parameters.Clear();
            InnerCommand.Dispose();
        }

        public Task<int> ExecuteNonQueryAsync(
            params SqlParameter[] parameters)
        {
            InnerCommand.Parameters.AddRange(parameters);

            InnerCommand.CommandType = CommandType.StoredProcedure;

            return InnerCommand.ExecuteNonQueryAsync();
        }
    }
}
