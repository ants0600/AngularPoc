using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Model;
using Domain.Repository;
using Newtonsoft.Json;

namespace InfrastructureTester.Mock
{
	public class MockCarRepository : ICarRepository
	{
		public List<Car> GetAllCars()
		{
			var json = File.ReadAllText("mockdata.json");
			return JsonConvert.DeserializeObject<List<Car>>(json);
		}

		public bool Insert(Car inserted)
		{
			throw new NotImplementedException();
		}

		public bool Update(Car updated)
		{
			throw new NotImplementedException();
		}

		public Car GetCarByName(string carName)
		{
			var allCars = this.GetAllCars();
			return allCars.FirstOrDefault(c => c.Name.Equals(carName, StringComparison.OrdinalIgnoreCase));
		}

		public Car GetCarById(int id)
		{
			var allCars = this.GetAllCars();
			return allCars.FirstOrDefault(c => c.Id == id);
		}

		public List<Car> TestDelayRun()
		{
			return new List<Car>();
		}

		public bool DeleteByIds(int[] ids)
		{
			throw new NotImplementedException();
		}

		public List<Car> GetCarsByPageIndex(int pageIndex, int pageSize, EnumFields sortBy, bool isAscending)
		{
			throw new NotImplementedException();
		}

		public int GetCarPageCount(int pageSize)
		{
			throw new NotImplementedException();
		}

		public ErrorEnum Validate(Car inserted)
		{
			return ErrorEnum.None;
		}

		public string GetErrorMessage(ErrorEnum errorCode)
		{
			return string.Empty;
		}
	}
}