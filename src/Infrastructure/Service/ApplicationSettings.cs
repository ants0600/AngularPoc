using Domain.Model;
using Infrastructure.Extensions;

namespace Infrastructure.Service
{
	/// <summary>
	///     make sure all properties IS STRING,
	///     will be used to read appsettings
	/// </summary>
	public class ApplicationSettings : AppSettingGetter, IApplicationSettings
	{
		private readonly TryParser _tryParser = new TryParser();
		public string CostExceedLimit { get; set; }
		public string CostExceedDiscount { get; set; }
		public string NumberOfCarsLimit { get; set; }
		public string NumberOfCarsDiscount { get; set; }
		public string YearsLimit { get; set; }
		public string YearsLimitDiscount { get; set; }

		public int GetCostExceedLimit()
		{
			return this._tryParser.ConvertToInteger(this.CostExceedLimit);
		}

		public float GetCostExceedDiscount()
		{
			return this._tryParser.ConvertToFloat(this.CostExceedDiscount);
		}

		public int GetNumberOfCarsLimit()
		{
			return this._tryParser.ConvertToInteger(this.NumberOfCarsLimit);
		}

		public float GetNumberOfCarsDiscount()
		{
			return this._tryParser.ConvertToFloat(this.NumberOfCarsDiscount);
		}

		public int GetYearsLimit()
		{
			return this._tryParser.ConvertToInteger(this.YearsLimit);
		}

		public float GetYearsLimitDiscount()
		{
			return this._tryParser.ConvertToFloat(this.YearsLimitDiscount);
		}
	}
}