using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Infrastructure.Extensions;

namespace Infrastructure.Extensions
{
    public abstract class OleDbDataService :
        BaseDataService
    {
        private readonly SqlServerDataService _sqlServerDataService;

        protected OleDbDataService(AdoNetHelper adoNetHelper, SqlServerDataService sqlServerDataService) : base(adoNetHelper)
        {
            _sqlServerDataService = sqlServerDataService;
        }

        protected OleDbDataService(FileInfo dataSource, SqlServerDataService sqlServerDataService, AdoNetHelper adoNetHelper) :
            base(dataSource, adoNetHelper)
        {
            _sqlServerDataService = sqlServerDataService;
        }

        public override DbCommand CreateCommand(string query, CommandType commandType,
            IDictionary<string, object> parameters,
            DbTransaction transaction)
        {
            if (IsConnectingSqlServer)
            {
                var dbConnection = (SqlConnection)_dbConnection;
                return _sqlServerDataService.CreateSqlCommand(query,
                    commandType,
                    parameters,
                    transaction,
                    dbConnection);
            }
            var value = new OleDbCommand
            {
                CommandText = query,
                CommandType = commandType,
                //Connection = (OleDbConnection)base._dbConnection
                Connection = new OleDbConnection(ConnectionString)
            };
            value.Transaction = transaction == null ? value.Transaction : (OleDbTransaction)transaction;
            SetParameters<OleDbCommand>(value, parameters);
            return value;
        }

        protected override DbConnection CreateConnection()
        {
            if (IsConnectingSqlServer)
            {
                return _sqlServerDataService.CreateConnection(ConnectionString);
            }

            var value = new OleDbConnection(ConnectionString);
            return value;
        }

        protected override DbDataAdapter CreateDataAdapter()
        {
            if (IsConnectingSqlServer)
            {
                return _sqlServerDataService.CreateSqlDataAdapter();
            }

            var value = new OleDbDataAdapter();
            return value;
        }

        protected override DbCommandBuilder CreateCommandBuilder()
        {
            if (IsConnectingSqlServer)
            {
                var dataAdapter = (SqlDataAdapter)_dataAdapter;
                return _sqlServerDataService.CreateSqlCommandBuilder(dataAdapter);
            }

            var value = new OleDbCommandBuilder
            {
                DataAdapter = (OleDbDataAdapter)_dataAdapter
            };
            return value;
        }
    }
}