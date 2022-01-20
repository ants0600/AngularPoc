using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Infrastructure.Extensions
{
	public class AdoNetHelper
	{
		private const short MS_ACCESS_BOOLEAN_TRUE = -1;
		private const short MS_ACCESS_FALSE = 0;
		internal const string SQL_SERVER_PROVIDER_NAME = "Provider=SQLOLEDB.1";

		public int ExecuteNonQuery(IDbCommand cmd)
		{
			IDbConnection cn = cmd.Connection;
			this.OpenConnection(cn);
			int value = cmd.ExecuteNonQuery();
			this.CloseConnection(cn);
			return value;
		}

		public bool RemoveDataRowList(DataTable dataTable, List<DataRowView> deletedList)
		{
			List<DataRow> deletedRowList = deletedList.Select(a => a.Row).ToList();
			return this.RemoveDataRowList(dataTable, deletedRowList);
		}

		public bool SetRowValues(DataRow updated, IDictionary<string, object> rowValues)
		{
			if (rowValues == null)
			{
				return false;
			}

			if (rowValues.Count <= 0)
			{
				return false;
			}

			foreach (var item in rowValues)
			{
				updated[item.Key] = item.Value;
			}

			return true;
		}

		public DataRow GetDataRow(DataTable dataTable, int index)
		{
			if (dataTable == null)
			{
				return null;
			}

			DataRowCollection rows = dataTable.Rows;
			if (rows == null)
			{
				return null;
			}

			if (rows.Count <= 0)
			{
				return null;
			}

			bool isValidIndex = index >= 0 && index <= rows.Count - 1;
			return isValidIndex ? rows[index] : null;
		}

		public void DeleteDataRow(DataTable dataTable, int index)
		{
			if (dataTable == null)
			{
				return;
			}

			DataRowCollection rows = dataTable.Rows;
			if (rows == null)
			{
				return;
			}

			if (rows.Count <= 0)
			{
				return;
			}

			bool isValidIndex = index >= 0 && index <= rows.Count - 1;
			if (!isValidIndex)
			{
				return;
			}

			dataTable.Rows[index].Delete();
		}

		public bool InsertRow(DataTable dataTable, SortedList<string, object> rowValues, int index)
		{
			DataRow inserted = dataTable.NewRow();
			this.SetRowValues(inserted, rowValues);
			if (index >= 0 && index < dataTable.Rows.Count - 1)
			{
				dataTable.Rows.InsertAt(inserted, index);
			}
			else
			{
				dataTable.Rows.Add(inserted);
			}

			bool isRowExisted = dataTable.Rows.IndexOf(inserted) >= 0;
			return isRowExisted;
		}

		public bool UpdateRow(DataTable dataTable, SortedList<string, object> rowValues, int index)
		{
			DataRowCollection rows = dataTable.Rows;
			if (rows.Count <= 0)
			{
				return false;
			}

			if (index < 0 || index >= rows.Count)
			{
				return false;
			}

			DataRow updated = dataTable.Rows[index];
			this.SetRowValues(updated, rowValues);
			return true;
		}

		public SortedList<string, object> GetRowValues(DataRow dataRow)
		{
			DataColumnCollection columns = dataRow.Table.Columns;
			var values = new SortedList<string, object>();
			for (int i = 0; i < columns.Count; i++)
			{
				DataColumn column = columns[i];
				string columnName = column.ColumnName;
				values[columnName] = dataRow[columnName];
			}

			return values;
		}


		public bool AddDataRowList(DataTable dataTable,
			List<DataRowView> inserted)
		{
			List<DataRow> rowList = inserted.Select(a => a.Row).ToList();
			return this.AddDataRowList(dataTable, rowList);
		}

		public bool AddDataRowList(DataTable dataTable,
			List<DataRow> inserted)
		{
			foreach (var rowValues in inserted.
				Select(a => this.GetRowValues(a)))
			{
				this.InsertRow(dataTable, rowValues, Int32.MaxValue);
			}
			return true;
		}

		public bool RemoveDataRowList(DataTable dataTable,
			List<DataRow> deletedList)
		{
			foreach (DataRow item in deletedList)
			{
				dataTable.Rows.Remove(item);
			}

			return true;
		}

		/// <summary>
		///     e.g:
		///     whereClause = [Name like '%An%']
		/// </summary>
		public bool IsRowExist(DataTable dataTable, string whereClause)
		{
			return this.GetDataRow(dataTable, whereClause) != null;
		}

		/// <summary>
		///     e.g:
		///     whereClause = [Name like '%An%']
		/// </summary>
		public DataRow GetDataRow(DataTable dataTable, string whereClause)
		{
			var dataView = new DataView(dataTable)
			{
				RowFilter = whereClause
			};

			return dataView.Count > 0 ? dataView[0].Row : null;
		}

		public bool CloseConnection(IDbConnection dbConnection)
		{
			if (dbConnection.State != ConnectionState.Open)
			{
				return false;
			}

			dbConnection.Close();
			return true;
		}

		public bool OpenConnection(IDbConnection dbConnection)
		{
			if (dbConnection.State != ConnectionState.Closed)
			{
				return false;
			}

			dbConnection.Open();
			return true;
		}

		public TTarget ExecuteScalar<TTarget>(IDbCommand dbCommand)
		{
			var cn = dbCommand.Connection;
			this.OpenConnection(cn);
			object resultValue = dbCommand.ExecuteScalar();
			this.CloseConnection(cn);

			var value = (TTarget)resultValue;
			return value;
		}

		public short GetMsAccessBooleanValue(bool value)
		{
			return value ? MS_ACCESS_BOOLEAN_TRUE : MS_ACCESS_FALSE;
		}

		public bool WriteXml(DataTable dt, FileInfo xmlFile)
		{
			string filePath = xmlFile.FullName;
			dt.WriteXml(filePath, XmlWriteMode.WriteSchema, false);
			xmlFile = new FileInfo(filePath);
			return xmlFile.Exists;
		}

		public IDbCommand CreateCommand(IDbConnection cn, string query, Dictionary<string, object> parameters, CommandType commandType, int timeout)
		{
			var value = cn.CreateCommand();
			value.CommandText = query;
			value.CommandType = commandType;
			value.CommandTimeout = timeout;
			parameters = parameters ?? new Dictionary<string, object>();
			foreach (var item in parameters)
			{
				var key = item.Key;
				var itemValue = item.Value;

				//sql
				var sqlCommand = value as SqlCommand;
				if (sqlCommand != null)
				{
					sqlCommand.Parameters.AddWithValue(key, itemValue);
				}

				//oledb
				var oleDbCommand = value as OleDbCommand;
				if (oleDbCommand != null)
				{
					oleDbCommand.Parameters.AddWithValue(key, itemValue);
				}
			}

			return value;
		}
	}
}