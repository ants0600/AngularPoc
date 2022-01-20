using Domain.Service;

namespace Domain.Model
{
	/// <summary>
	///     for reading all appsettings from web config,
	///     use string as type in all properties.
	///     for converting into certain types, put methods
	/// </summary>
	public interface IApplicationSettings : IService
	{
		string CostExceedLimit { get; set; }
		string CostExceedDiscount { get; set; }
		string NumberOfCarsLimit { get; set; }
		string NumberOfCarsDiscount { get; set; }
		string YearsLimit { get; set; }
		string YearsLimitDiscount { get; set; }

		int GetCostExceedLimit();
		float GetCostExceedDiscount();
		int GetNumberOfCarsLimit();
		float GetNumberOfCarsDiscount();
		int GetYearsLimit();
		float GetYearsLimitDiscount();
	}
}