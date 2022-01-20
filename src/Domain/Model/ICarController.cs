using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
	public interface ICarController
	{
		UpdateCarResponse AddCar(Car inserted);
		Car GetCarByName(string name);
		List<Car> GetCars();

		List<Car> GetCarsByPageIndex(int pageIndex, int pageSize, EnumFields sortBy, bool isAscending);
		UpdateCarResponse UpdateCarById(Car updated);

		Car GetCarById(int id);

		bool DeleteCars(int[] ids);

		int GetCarPageCount(int pageSize);
	}
}
