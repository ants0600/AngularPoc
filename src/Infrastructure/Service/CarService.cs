using System.Collections.Generic;
using Domain.Resources;
using Domain.Model;
using Domain.Model.Exception;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
	public class CarService : ICarService
	{
		protected readonly IApplicationSettings _applicationSettings;
		private readonly ICarRepository _carRepository;

		public CarService(IApplicationSettings applicationSettings, ICarRepository carRepository)
		{
			this._applicationSettings = applicationSettings;
			this._carRepository = carRepository;
		}

		/// <summary>
		///     Get price after discount.
		///     Discount calculation rule:
		///     1. If total cost exceeds $100,000 apply 5% discount
		///     2. If number of cars is more than 2, apply 3% discount
		///     3. If cars year is before 2000, apply 10% discount
		///     4. The above rules are cumulative(i.e.all of them can be applicable at the same time)
		/// </summary>
		/// <param name="carName"></param>
		/// <param name="quantity"></param>
		/// <returns></returns>
		public double GetPriceAfterDiscounts(string carName, int quantity)
		{
			//get car by name
			var found = this.GetCarByName(carName);
			if (found == null)
			{
				throw new CarNotFoundException();
			}

			var totalPrice = found.Price * quantity;
			var year = found.Year;
			var totalDiscountPercent = 0.0;

			//1. If total cost exceeds $100,000 apply 5% discount
			var costExceedLimit = this._applicationSettings.GetCostExceedLimit();
			var costExceedDiscount = this._applicationSettings.GetCostExceedDiscount();
			if (totalPrice >= costExceedLimit)
			{
				totalDiscountPercent += costExceedDiscount;
			}

			//2. If number of cars is more than 2, apply 3% discount
			var numberOfCarsLimit = this._applicationSettings.GetNumberOfCarsLimit();
			var numberOfCarsDiscount = this._applicationSettings.GetNumberOfCarsDiscount();
			if (quantity > numberOfCarsLimit)
			{
				totalDiscountPercent += numberOfCarsDiscount;
			}

			//3. If cars year is before 2000, apply 10% discount
			var yearsLimit = this._applicationSettings.GetYearsLimit();
			var yearsLimitDiscount = this._applicationSettings.GetYearsLimitDiscount();
			if (year < yearsLimit)
			{
				totalDiscountPercent += yearsLimitDiscount;
			}

			var value = totalPrice * (ConstantValues.PERCENT - totalDiscountPercent) / ConstantValues.PERCENT;
			return value;
		}

		public List<Car> GetAllCars()
		{
			return this._carRepository.GetAllCars();
		}

		public bool Insert(Car inserted)
		{
			return this._carRepository.Insert(inserted);
		}

		public bool Update(Car updated)
		{
			return this._carRepository.Update(updated);
		}

		public Car GetCarByName(string carName)
		{
			return this._carRepository.GetCarByName(carName);
		}

		public string GetErrorMessageCarNotFound(string carName)
		{
			return string.Format(FieldText.CarNotFoundErrorMessageFormat, carName);
		}

		public string GetErrorMessageCarNotFound(int carId)
		{
			return string.Format(FieldText.CarIdNotFoundErrorMessageFormat, carId);
		}

		public Car GetCarById(int id)
		{
			return this._carRepository.GetCarById(id);
		}

		public bool DeleteByIds(int[] ids)
		{
			return this._carRepository.DeleteByIds(ids);
		}
	}
}