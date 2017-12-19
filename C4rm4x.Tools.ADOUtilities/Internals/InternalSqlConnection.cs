#region Using

using System;
using System.Data.SqlClient;

#endregion

namespace C4rm4x.Tools.ADOUtilities
{
    internal class InternalSqlConnection : IDisposable
    {
        public SqlConnection InnerConnection { get; private set; }

        public InternalSqlConnection(string connectionString)
        {
            InnerConnection = new SqlConnection(connectionString);

            InnerConnection.Open();
        }

        public void Dispose()
        {
            InnerConnection.Close();
            InnerConnection.Dispose();
        }
    }
}
