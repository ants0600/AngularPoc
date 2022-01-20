using Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace InfrastructureTester
{
	/// <summary>
	///     test basic database operation for cars
	/// </summary>
	[TestClass]
	public class CarServiceTester : BaseTester
	{
		private readonly ICarService _carService;

		public CarServiceTester()
		{
			this._carService = this._container.Resolve<ICarService>();
		}

		[TestMethod]
		public void InsertCar_Must_BeSuccessful()
		{

		}
	}
}