using System.Collections.Generic;
using Domain.Model;

namespace Domain.Service
{
	public interface ICarService : IService
	{
		double GetPriceAfterDiscounts(string carName, int quantity);
		List<Car> GetAllCars();
		bool Insert(Car inserted);
		bool Update(Car updated);
		Car GetCarByName(string carName);
		string GetErrorMessageCarNotFound(string carName);
		string GetErrorMessageCarNotFound(int carId);
		Car GetCarById(int id);
		bool DeleteByIds(int[] ids);
	}
}