using Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureDotNetCore.Repository
{
	public abstract class BaseRepository
	{
		protected readonly IDbConnection _connection = new SqlConnection();
		private const string CONNECTION_STRING_NAME = nameof(IConnectionStrings.MainDatabase);
		public BaseRepository(IConfiguration configuration)
		{
			this._connection.ConnectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME);
		}
	}
}
