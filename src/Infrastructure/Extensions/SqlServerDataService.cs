using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Infrastructure.Extensions;

namespace Infrastructure.Extensions
{
    /// <summary>
    ///     please inherit this class
    /// </summary>
    public abstract class SqlServerDataService : BaseDataService
    {
        #region user defined methods

        protected SqlServerDataService(AdoNetHelper adoNetHelper) : base(adoNetHelper)
        {
        }

        protected sealed override DbCommandBuilder CreateCommandBuilder()
        {
            var sqlDataAdapter = (SqlDataAdapter) _dataAdapter;
            return CreateSqlCommandBuilder(sqlDataAdapter);
        }

        internal DbCommandBuilder CreateSqlCommandBuilder(SqlDataAdapter sqlDataAdapter)
        {
            return new SqlCommandBuilder
            {
                DataAdapter = sqlDataAdapter
            };
        }

        protected sealed override DbDataAdapter CreateDataAdapter()
        {
            return CreateSqlDataAdapter();
        }

        internal SqlDataAdapter CreateSqlDataAdapter()
        {
            return new SqlDataAdapter();
        }

        protected sealed override DbConnection CreateConnection()
        {
            var connectionString = ConnectionString;
            return CreateConnection(connectionString);
        }

        public SqlConnection CreateConnection(string connectionString)
        {
            var value = new SqlConnection();
            if (string.IsNullOrEmpty(connectionString))
            {
                return value;
            }

            connectionString = connectionString.Replace(AdoNetHelper.SQL_SERVER_PROVIDER_NAME, string.Empty);
            value.ConnectionString = connectionString;
            return value;
        }

        public string BuildConnectionString(string dataSource, string initialCatalog, string userId,
            string password, int connectTimeout)
        {
            var csb = new SqlConnectionStringBuilder
            {
                ConnectTimeout = connectTimeout,
                DataSource = dataSource,
                UserID = userId,
                Password = password,
                InitialCatalog = initialCatalog
            };

            return csb.ConnectionString;
        }

        public sealed override DbCommand CreateCommand(string query, CommandType commandType,
            IDictionary<string, object> parameters,
            DbTransaction transaction)
        {
            var dbConnection = (SqlConnection) _dbConnection;
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.ConnectionString = ConnectionString;
            }
            return CreateSqlCommand(query, commandType, parameters, transaction, dbConnection);
        }

        internal DbCommand CreateSqlCommand(string commandText, CommandType commandType,
            IDictionary<string, object> parameters, DbTransaction transaction,
            SqlConnection dbConnection)
        {
            var value = new SqlCommand
            {
                CommandText = commandText,
                CommandType = commandType,
                Connection = dbConnection,
                CommandTimeout = int.MaxValue
            };

            value.Transaction = transaction == null ? value.Transaction : (SqlTransaction) transaction;
            value.Parameters.Clear();
            SetParametersValue(value, parameters);
            return value;
        }

        #endregion
    }
}