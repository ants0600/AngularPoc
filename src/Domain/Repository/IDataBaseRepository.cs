namespace Domain.Repository
{
	public interface IDataBaseRepository : IRepository
	{
		string ConnectionString { get; }
	}
}