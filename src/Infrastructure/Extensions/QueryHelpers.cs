using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Domain.Service;

namespace Infrastructure.Extensions
{
	/// <summary>
	///     contains all functions about SELECT CLAUSE operations.
	///     Rite now it is coupled strongly with Dapper
	/// </summary>
	public class QueryHelpers : IQueryHelpers
	{
		/// <summary>
		///     execute non query with parameters
		/// </summary>
		public int ExecuteNonQuery(IDbConnection cn, string query, Dictionary<string, object> parameters,
			CommandType commandType, int commandTimeout)
		{
			int value;
			if (parameters == null)
			{
				return this.ExecuteNonQuery(cn, query, commandType, commandTimeout);
			}

			var count = parameters.Count;
			if (count <= 0)
			{
				return this.ExecuteNonQuery(cn, query, commandType, commandTimeout);
			}

			//set parameters
			var param = new DynamicParameters();
			foreach (var item in parameters)
			{
				var key = item.Key;
				var itemValue = item.Value;
				param.Add(key, itemValue);
			}

			value = cn.Execute(query, param, null, commandTimeout, commandType);
			return value;
		}

		/// <summary>
		///     execute non query without parameters
		/// </summary>
		public int ExecuteNonQuery(IDbConnection cn, string query, CommandType commandType, int commandTimeout)
		{
			var value = cn.Execute(query, null, null, commandTimeout, commandType);
			return value;
		}

		/// <summary>
		///     execute query, store results in list of object
		/// </summary>
		public List<T> GetListFromQuery<T>(IDbConnection cn, string query, CommandType commandType, int commandTimeout,
			bool isBuffered) where T : class, new()
		{
			//sample of dapper query, invoke SELECT to MODEL CLASS, POCO classes
			var result = cn.Query<T>(query, null, null, isBuffered, commandTimeout, commandType);
			var values = new List<T>();
			if (result == null)
			{
				return values;
			}
			
			values = result.ToList();
			return values;
		}

		/// <summary>
		///     execute query, store results in list of object
		/// </summary>
		public List<T> GetListFromQuery<T>(IDbConnection cn, string query, Dictionary<string, object> parameters,
			CommandType commandType, int commandTimeout,
			bool isBuffered) where T : class, new()
		{
			if (parameters == null)
			{
				return this.GetListFromQuery<T>(cn, query, commandType, commandTimeout, isBuffered);
			}

			var count = parameters.Count;
			if (count <= 0)
			{
				return this.GetListFromQuery<T>(cn, query, commandType, commandTimeout, isBuffered);
			}

			//set parameters
			var param = new DynamicParameters();
			foreach (var item in parameters)
			{
				var name = item.Key;
				var value1 = item.Value;
				param.Add(name, value1);
			}

			//execiute query
			var result = cn.Query<T>(query, param, null, isBuffered, commandTimeout, commandType);
			var values = new List<T>();
			if (result == null)
			{
				return values;
			}
			values = result.ToList();
			return values;
		}

		public T GetSingleOrDefault<T>(IDbConnection cn, string query, Dictionary<string, object> parameters,
			CommandType commandType, int commandTimeout,
			bool isBuffered)
			where T : class, new()
		{
			var values = this.GetListFromQuery<T>(cn, query, commandType, commandTimeout, isBuffered);
			if (values == null)
			{
				return null;
			}

			var count = values.Count;
			if (count <= 0)
			{
				return null;
			}

			//get first element
			var value = values[0];
			return value;
		}
	}
}