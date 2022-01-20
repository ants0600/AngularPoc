using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace Infrastructure.Extensions
{
	public abstract class BaseDataService
	{
		#region constructors

		protected BaseDataService(AdoNetHelper adoNetHelper)
			: this(null, adoNetHelper)
		{
			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			this._dbConnection = this.CreateConnection();
			// ReSharper restore DoNotCallOverridableMethodsInConstructor

			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			this._dataAdapter = this.CreateDataAdapter();
			// ReSharper restore DoNotCallOverridableMethodsInConstructor

			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			this._commandBuilder = this.CreateCommandBuilder();
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
		}

		#endregion

		public virtual void SetParameters<TCommand>(DbCommand dbCommand, IDictionary<string, object> parameters)
			where TCommand : DbCommand
		{
			this.SetParametersValue(dbCommand, parameters);
		}

		internal void SetParametersValue<TCommand>(TCommand dbCommand, IDictionary<string, object> parameters)
			where TCommand : DbCommand
		{
			if (parameters == null)
			{
				return;
			}

			if (parameters.Count <= 0)
			{
				return;
			}

			foreach (var item in parameters)
			{
				//for sql
				var sqlCommand = dbCommand as SqlCommand;
				if (sqlCommand != null)
				{
					this.SetParameterValue(sqlCommand, item);
					continue;
				}

				//for oledb
				var oleDbCommand = dbCommand as OleDbCommand;
				if (oleDbCommand != null)
				{
					this.SetParameterValue(oleDbCommand, item);
					continue;
				}

				throw new NotSupportedException("command type is not supported");
			}
		}

		/// <summary>
		///     for ole db
		/// </summary>
		protected internal void SetParameterValue(OleDbCommand dbCommand, KeyValuePair<string, object> item)
		{
			dbCommand.Parameters.AddWithValue(item.Key, item.Value);
		}

		/// <summary>
		///     for sql server
		/// </summary>
		protected internal void SetParameterValue(SqlCommand dbCommand, KeyValuePair<string, object> item)
		{
			dbCommand.Parameters.AddWithValue(item.Key, item.Value);
		}

		#region fields

		protected DbCommandBuilder _commandBuilder;
		protected DbDataAdapter _dataAdapter;
		protected DbCommand _dbCommand;
		protected DbConnection _dbConnection;
		protected readonly FileInfo _dataSource;
		private readonly AdoNetHelper _adoNetHelper;

		protected BaseDataService(FileInfo dataSource, AdoNetHelper adoNetHelper)
		{
			this._adoNetHelper = adoNetHelper;
			this._dataSource = dataSource;
		}

		#endregion

		#region abstract members

		public bool IsConnectingSqlServer
		{
			get
			{
				var value = this.ConnectionString.IndexOf(AdoNetHelper.SQL_SERVER_PROVIDER_NAME,
					            StringComparison.OrdinalIgnoreCase) >= 0;
				return value;
			}
		}

		public abstract string ConnectionString { get; }

		public abstract DbCommand CreateCommand(string query, CommandType commandType,
			IDictionary<string, object> parameters, DbTransaction transaction);

		protected abstract DbConnection CreateConnection();

		protected abstract DbDataAdapter CreateDataAdapter();

		protected abstract DbCommandBuilder CreateCommandBuilder();

		#endregion

		#region user defined methods

		public bool OpenConnection()
		{
			return this._adoNetHelper.OpenConnection(this._dbConnection);
		}

		public bool CloseConnection()
		{
			return this._adoNetHelper.CloseConnection(this._dbConnection);
		}

		public DbTransaction BeginTransaction()
		{
			return this._dbConnection.BeginTransaction();
		}

		public DataTable GetDataTable(DbCommand dbCommand, string tableName)
		{
			this._dataAdapter.SelectCommand = dbCommand;
			var value = new DataTable
			{
				TableName = tableName
			};
			this._dataAdapter.Fill(value);
			return value;
		}

		#endregion
	}
}
