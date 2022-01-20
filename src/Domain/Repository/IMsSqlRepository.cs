using System.Data.SqlClient;

namespace Domain.Repository
{
	public interface IMsSqlRepository : IDataBaseRepository
	{
		SqlConnection CreateConnection();
	}
}