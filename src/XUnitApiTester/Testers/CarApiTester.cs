using Domain.Model;
using FluentAssertions;
using System;
using Xunit;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Domain.Model.Response;

namespace XUnitInfraTester
{
	/// <summary>
	/// this class consume API
	/// </summary>
	public class CarApiTester : BaseApiTester
	{
		public const string CAR_API = "/api/Car/";
		public const string GET_CARS_PAGE_COUNT = CAR_API + nameof(ICarController.GetCarPageCount) + "?pageSize={0}";
		public const string GET_CARS_BY_PAGE_INDEX = CAR_API + nameof(ICarController.GetCarsByPageIndex) + "?pageIndex={0}&pageSize={1}&orderBy={2}&isAscending={3}";
		public const string GET_CARS = CAR_API + nameof(ICarController.GetCars);
		public const string ADD_CAR = CAR_API + nameof(ICarController.AddCar);
		public const string GET_CAR_BY_NAME = CAR_API + nameof(ICarController.GetCarByName) + "?name={0}";
		public const string UPDATE_CAR = CAR_API + nameof(ICarController.UpdateCarById);
		public const string GET_CAR_BY_ID = CAR_API + nameof(ICarController.GetCarById) + "?id={0}";
		public const string DELETE_CARS = CAR_API + nameof(ICarController.DeleteCars);

		private readonly ITestOutputHelper _outputHelper;

		public CarApiTester(ITestOutputHelper outputHelper)
		{
			_outputHelper = outputHelper;
		}

		/// <summary>
		/// ex: http://localhost:5000/api/Car/GetCars
		/// 
		/// must not be NULL
		/// </summary>
		[Fact]
		public void GetCarList_MustGiveValues()
		{
			//arrange, act
			var url = base._settings.ApiUrl + GET_CARS;
			string result = base.DownloadString(url, HttpMethod.Get, null);

			//assert
			var cars = JsonConvert.DeserializeObject<List<Car>>(result);
			cars.Should().NotBeNull();
		}

		/// <summary>
		/// update existing car must be successful. Steps:
		/// Insert car,
		/// Get car by name,
		/// Update fields,
		/// Get car by id,
		/// Then assert
		/// </summary>
		/// <param name="id"></param>
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]

		public void UpdateExistingCar_MustBe_Successful(int id)
		{
			//arrange, act
			//insert car
			string insertedCarName = id + nameof(Car.Name) + this.GetTimeStamp();
			Car inserted = new()
			{
				Id = id,
				Name = insertedCarName,
				Price = id * 1000,
				Year = id + 2000
			};

			this.GetInsertCarStatus(inserted);

			//get car by name, then update fields
			string newName = id + nameof(UPDATE_CAR) + this.GetTimeStamp();
			Car updated = this.GetCarByName(insertedCarName);
			id = updated.Id;
			updated.Price = 100000;
			updated.Year = 2006;
			updated.Name = newName;
			var s = this.UpdateCar(updated);

			//get car by id
			inserted = this.GetCarById(id);
			updated.Should().BeEquivalentTo(inserted);
		}

		/// <summary>
		/// test validation functions before insert
		/// </summary>
		/// <param name="name"></param>
		/// <param name="year"></param>
		/// <param name="price"></param>
		/// <param name="expected"></param>
		[Theory]
		[InlineData("", 2012, 10000, ErrorEnum.CarNameIsEmpty)]
		[InlineData("New Car Invalid Year", 3000, 10000, ErrorEnum.CarYearIsInvalid)]
		[InlineData("New Car Invalid Price", 3000, 0, ErrorEnum.CarPriceIsInvalid)]
		public void ValidateCar_BeforeInsert_MustBeSuccessful(string name, int year, double price, ErrorEnum expected)
		{
			var inserted = new Car()
			{
				Name = name,
				Year = year,
				Price = price
			};
			var s = this.InvokeInsertCarApi(inserted);
			s.Error.Should().Be(expected);
		}

		private bool UpdateCar(Car updated)
		{
			var jsonContent = JsonContent.Create<Car>(updated);
			string url = base._settings.ApiUrl + UPDATE_CAR;
			string s = base.DownloadString(url, HttpMethod.Post, jsonContent);
			var response = JsonConvert.DeserializeObject<UpdateCarResponse>(s);
			return response.Status;
		}

		private Car GetCarById(int id)
		{
			var url = base._settings.ApiUrl + GET_CAR_BY_ID;
			url = string.Format(url, id);
			string s = base.DownloadString(url, HttpMethod.Get, null);
			var value = JsonConvert.DeserializeObject<Car>(s);
			return value;
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void DeleteExistingCar_MustBe_Successful(int id)
		{
			//arrange, act
			//insert car first
			string insertedCarName = id + nameof(Car.Name) + this.GetTimeStamp();
			Car inserted = new()
			{
				Id = id,
				Name = insertedCarName,
				Price = id * 1000,
				Year = id + 2000
			};

			this.GetInsertCarStatus(inserted);

			//find car by name, then get id
			var afterInsert = this.GetCarByName(insertedCarName);
			id = afterInsert.Id;
			int[] ids = { id };

			//delete by id
			bool isDeleted = this.DeleteCars(ids);

			//find by id, it must be null
			var afterDelete = this.GetCarById(id);
			var isNotFound = afterDelete == null;

			isNotFound.Should().BeTrue();
		}

		private bool DeleteCars(int[] ids)
		{
			var url = base._settings.ApiUrl + DELETE_CARS;
			var jsonContent = JsonContent.Create(ids);
			string s = base.DownloadString(url, HttpMethod.Post, jsonContent);
			return bool.Parse(s);
		}

		/// <summary>
		/// invoke api to insert car must give true result.
		/// Then the inserted row must be found.
		/// </summary>
		/// <param name="id"></param>
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]

		public void InsertCar_MustBe_Successful(int id)
		{
			//arrange, act
			string insertedCarName = id + nameof(Car.Name) + this.GetTimeStamp();
			Car inserted = new()
			{
				Id = id,
				Name = insertedCarName,
				Price = id * 1000,
				Year = id + 2000
			};

			this.GetInsertCarStatus(inserted);
			Car found = this.GetCarByName(insertedCarName);
			found.Id = id;
			found.Should().BeEquivalentTo(inserted);

		}

		private bool GetInsertCarStatus(Car inserted)
		{
			var updateCarResponse = this.InvokeInsertCarApi(inserted);
			return updateCarResponse.Status;
		}

		private UpdateCarResponse InvokeInsertCarApi(Car inserted)
		{
			var url = base._settings.ApiUrl + ADD_CAR;
			var content = JsonContent.Create<Car>(inserted);
			var postResponse = base.DownloadString(url, HttpMethod.Post, content);
			return JsonConvert.DeserializeObject<UpdateCarResponse>(postResponse);
		}

		private Car GetCarByName(string carName)
		{
			var url = base._settings.ApiUrl + GET_CAR_BY_NAME;
			url = string.Format(url, carName);
			string text = base.DownloadString(url, HttpMethod.Get, null);
			this._outputHelper.WriteLine(text);

			Car value = JsonConvert.DeserializeObject<Car>(text);
			return value;
		}

		/// <summary>
		/// no matter the parameter value,
		/// the API get car page count must NOT return null
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		[Theory]
		[InlineData(0, 10, EnumFields.Id, true)]
		[InlineData(100, 5, EnumFields.Name, false)]
		[InlineData(0, 0, EnumFields.Year, true)]
		[InlineData(-1, -1, EnumFields.Year, false)]

		public void GetPagingCars_MustNot_BeNull(int pageIndex, int pageSize, EnumFields orderBy, bool isAscending)
		{
			var url = base._settings.ApiUrl + GET_CARS_BY_PAGE_INDEX;
			url = string.Format(url, pageIndex, pageSize, orderBy.GetHashCode(), isAscending);
			string text = base.DownloadString(url, HttpMethod.Get, null);
			this._outputHelper.WriteLine(text);
			JsonConvert.DeserializeObject<List<Car>>(text).Should().NotBeNull();
		}

		/// <summary>
		/// no matter the parameter, the page count API must return positive number
		/// </summary>
		/// <param name="pageSize"></param>
		[Theory]
		[InlineData(10)]
		[InlineData(100)]
		[InlineData(0)]
		[InlineData(-5)]
		public void GetCarPageCount_MustNotGive_NegativeNumber(int pageSize)
		{
			var url = base._settings.ApiUrl + GET_CARS_PAGE_COUNT;
			url = string.Format(url, pageSize);
			string text = base.DownloadString(url, HttpMethod.Get, null);
			this._outputHelper.WriteLine(text);
			int.Parse(text).Should().BeGreaterThan(0);
		}

		private string GetTimeStamp()
		{
			var now = DateTime.Now;
			return string.Format("{0}-{1}-{2} {3}:{4}:{5}:{6}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
		}
	}
}
