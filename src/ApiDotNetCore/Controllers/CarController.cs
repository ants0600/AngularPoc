using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repository;
using Domain.Model.Response;

namespace DotNetCoreApi.Controllers
{
	/// <summary>
	/// API methods about cars, all operations about car data
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CarController : ControllerBase, ICarController
	{
		private readonly ICarRepository _carRepository;

		public CarController(ICarRepository carRepository)
		{
			this._carRepository = carRepository;
		}

		/// <summary>
		/// get all cars from database
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetCars))]
		public List<Car> GetCars()
		{
			return this._carRepository.GetAllCars();
		}

		/// <summary>
		///     API method get car by name
		/// </summary>
		/// <param name="name">
		///     searched car name
		/// </param>
		/// <returns>
		///     found car full data
		/// </returns>
		[HttpGet]
		[Route(nameof(GetCarByName))]
		public Car GetCarByName(string name)
		{
			return this._carRepository.GetCarByName(name);
		}

		/// <summary>
		/// insert car to database
		/// </summary>
		/// <param name="inserted"></param>
		/// <returns></returns>
		[HttpPost]
		[Route(nameof(AddCar))]

		public UpdateCarResponse AddCar([FromBody] Car inserted)
		{
			ErrorEnum errorCode = this._carRepository.Validate(inserted);
			if (errorCode != ErrorEnum.None)
			{
				string errorMessage = this._carRepository.GetErrorMessage(errorCode);
				return new UpdateCarResponse(inserted, false, errorCode, errorMessage);
			}

			var status = this._carRepository.Insert(inserted);
			var value = new UpdateCarResponse(inserted, status, errorCode, string.Empty);
			return value;
		}

		/// <summary>
		/// update car by id
		/// </summary>
		/// <param name="updated"></param>
		/// <returns></returns>
		[HttpPost]
		[Route(nameof(UpdateCarById))]

		public UpdateCarResponse UpdateCarById([FromBody] Car updated)
		{
			ErrorEnum errorCode = this._carRepository.Validate(updated);
			if (errorCode != ErrorEnum.None)
			{
				string errorMessage = this._carRepository.GetErrorMessage(errorCode);
				return new UpdateCarResponse(updated, false, errorCode, errorMessage);
			}

			bool status = this._carRepository.Update(updated);
			var value = new UpdateCarResponse(updated, status, ErrorEnum.None, string.Empty);
			return value;
		}

		/// <summary>
		/// get car by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetCarById))]

		public Car GetCarById(int id)
		{
			return this._carRepository.GetCarById(id);
		}

		/// <summary>
		/// delete cars by ids
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		[HttpPost]
		[Route(nameof(DeleteCars))]

		public bool DeleteCars([FromBody] int[] ids)
		{
			return this._carRepository.DeleteByIds(ids);
		}

		/// <summary>
		/// get cars by page index
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="orderBy">
		/// 0: id, 1: Name, 2: Price, 3: Year
		/// </param>
		/// <returns></returns>
		/// <param name="isAscending"></param>

		[HttpGet]
		[Route(nameof(GetCarsByPageIndex))]
		public List<Car> GetCarsByPageIndex(int pageIndex, int pageSize, EnumFields orderBy, bool isAscending)
		{
			return this._carRepository.GetCarsByPageIndex(pageIndex, pageSize, orderBy, isAscending);
		}

		/// <summary>
		/// get page count by page size
		/// </summary>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetCarPageCount))]

		public int GetCarPageCount(int pageSize)
		{
			return this._carRepository.GetCarPageCount(pageSize);
		}
	}
}
