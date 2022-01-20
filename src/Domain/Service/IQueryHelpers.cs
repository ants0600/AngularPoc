using System.Collections.Generic;
using System.Data;

namespace Domain.Service
{
	public interface IQueryHelpers : IService
	{
		/// <summary>
		///     execute non query with parameters
		/// </summary>
		int ExecuteNonQuery(IDbConnection cn, string query, Dictionary<string, object> parameters,
			CommandType commandType, int commandTimeout);

		/// <summary>
		///     execute non query without parameters
		/// </summary>
		int ExecuteNonQuery(IDbConnection cn, string query, CommandType commandType, int commandTimeout);

		/// <summary>
		///     execute query, store results in list of object
		/// </summary>
		List<T> GetListFromQuery<T>(IDbConnection cn, string query, CommandType commandType, int commandTimeout,
			bool isBuffered)
			where T : class, new();

		/// <summary>
		///     execute query, store results in list of object
		/// </summary>
		List<T> GetListFromQuery<T>(IDbConnection cn, string query, Dictionary<string, object> parameters,
			CommandType commandType, int commandTimeout,
			bool isBuffered)
			where T : class, new();
	}
}