using Domain.Service;

namespace Domain.Model
{
	/// <summary>
	///     use string in all properties
	/// </summary>
	public interface IConnectionStrings : IService
	{
		/// <summary>
		///     main database connection string
		/// </summary>
		string MainDatabase { get; set; }
	}
}