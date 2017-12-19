#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Tools.ADOUtilities
{
    /// <summary>
    /// Base class to transform data
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public class AbstractProcessor<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionStringName">The connection string name</param>
        public AbstractProcessor(
            string connectionStringName)
        {
            connectionStringName.NotNullOrEmpty(nameof(connectionStringName));

            ConnectionString = ConfigurationManager
                .ConnectionStrings[connectionStringName]
                .ConnectionString;
        }

        /// <summary>
        /// Runs the given store procedure
        /// </summary>
        /// <param name="function">The store procedure name</param>
        /// <param name="parameters">Collection of parameters</param>
        protected async Task RunAsync(string function, params SqlParameter[] parameters)
        {
            using (var connection = new InternalSqlConnection(ConnectionString))
            {
                using (var command = new InternalSqlCommand(function, connection))
                {
                    await command.ExecuteNonQueryAsync(parameters);
                }
            }
        }

        #region Parameter getters

        /// <summary>
        /// Creates a parameter of type NVarChar
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">The value</param>
        /// <returns>The parameter of type NVarChar and value</returns>
        protected static SqlParameter Get(string name, string value)
        {
            return Get(name, value, SqlDbType.NVarChar);
        }

        /// <summary>
        /// Creates a parameter of type DateTime2
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">The value</param>
        /// <returns>The parameter of type DateTime2 and value</returns>
        protected static SqlParameter Get(string name, DateTime value)
        {
            return Get(name, value, SqlDbType.DateTime2);
        }

        /// <summary>
        /// Creates a parameter of type Int
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">The value</param>
        /// <returns>The parameter of type Int and value</returns>
        protected static SqlParameter Get(string name, int value)
        {
            return Get(name, value, SqlDbType.Int);
        }

        /// <summary>
        /// Creates a parameter of type Float
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">The value</param>
        /// <returns>The parameter of type Float and value</returns>
        protected static SqlParameter Get(string name, float value)
        {
            return Get(name, value, SqlDbType.Float);
        }

        /// <summary>
        /// Creates a parameter of type Money
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">The value</param>
        /// <returns>The parameter of type Money and value</returns>
        protected static SqlParameter Get(string name, decimal value)
        {
            return Get(name, value, SqlDbType.Money);
        }

        /// <summary>
        /// Creates a parameter of type Bit
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">The value</param>
        /// <returns>The parameter of type Bit and value</returns>
        protected static SqlParameter Get(string name, bool value)
        {
            return Get(name, value, SqlDbType.Bit);
        }

        /// <summary>
        /// Creates a parameter of type UniqueIdentifier
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">The value</param>
        /// <returns>The parameter of type UniqueIdentifier and value</returns>
        protected static SqlParameter Get(string name, Guid value)
        {
            return Get(name, value, SqlDbType.UniqueIdentifier);
        }

        private static SqlParameter Get(string name, object value, SqlDbType type)
        {
            var parameter = new SqlParameter(name, type);

            parameter.Value = value ?? DBNull.Value;

            return parameter;
        }

        #endregion
    }
}
