using System.Data.SqlClient;
using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Repository
{
	public abstract class BaseRepository : IMsSqlRepository
	{
		protected readonly SqlConnection _connection;
		protected readonly IConnectionStrings _connectionStrings;
		protected readonly IQueryHelpers _helper;

		protected BaseRepository(IConnectionStrings connectionStrings, IQueryHelpers helper)
		{
			this._connectionStrings = connectionStrings;
			this._helper = helper;
			this._connection = this.CreateConnection();
		}

		public SqlConnection CreateConnection()
		{
			return new SqlConnection(this.ConnectionString);
		}

		public string ConnectionString => this._connectionStrings.MainDatabase;
	}
}