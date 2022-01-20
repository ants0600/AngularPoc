using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Repository
{
	public interface ICarRepository : IRepository
	{
		List<Car> GetAllCars();
		bool Insert(Car inserted);
		bool Update(Car updated);
		Car GetCarByName(string carName);
		Car GetCarById(int id);
		List<Car> TestDelayRun();
		bool DeleteByIds(int[] ids);
		List<Car> GetCarsByPageIndex(int pageIndex, int pageSize, EnumFields sortBy, bool isAscending);
		int GetCarPageCount(int pageSize);
		ErrorEnum Validate(Car inserted);
		string GetErrorMessage(ErrorEnum errorCode);
	}
}