using Domain.Model;
using Infrastructure.Extensions;

namespace Infrastructure.Service
{
	/// <summary>
	///     list all connection strings here
	/// </summary>
	public class ConnectionStrings : ConnectionStringGetter, IConnectionStrings
	{
		public string MainDatabase { get; set; }
	}
}