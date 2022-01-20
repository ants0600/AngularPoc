using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Domain.Model;
using Domain.Model.Exception;
using Domain.Model.Request;
using Domain.Model.Response;
using Domain.Service;
using Infrastructure.Common;
using NLog;
using Swashbuckle.Swagger.Annotations;

namespace RestfulApi.Controllers
{
	/// <summary>
	///     simple controller, can document here
	/// </summary>
	public class CarController : BaseApiController
	{
		private readonly ICarService _service;

		/// <summary>
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="globalExceptionHandler"></param>
		/// <param name="service"></param>
		public CarController(ILogger logger, IGlobalExceptionHandler globalExceptionHandler, ICarService service) : base(
			logger,
			globalExceptionHandler)
		{
			this._service = service;
		}

		/// <summary>
		///     API method to insert car into database
		/// </summary>
		/// <param name="inserted">
		///     inserted car data
		/// </param>
		/// <returns>
		///     status of insertion
		/// </returns>
		[HttpPost]
		[Route(nameof(AddCar))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(UpdateCarResponse))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(AddCar))]
		public IHttpActionResult AddCar(Car inserted)
		{
			try
			{
				var status = this._service.Insert(inserted);
				var response = new UpdateCarResponse(inserted, status, ErrorEnum.None, string.Empty);
				return this.Ok(response);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}

		/// <summary>
		/// delete cars by multiple ids
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		[HttpPost]
		[Route(nameof(DeleteCarByIds))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(bool))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(DeleteCarByIds))]
		public IHttpActionResult DeleteCarByIds(int[] ids)
		{
			try
			{
				var status = this._service.DeleteByIds(ids);
				return this.Ok(status);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}

		/// <summary>
		///     API method to get all cars from database
		/// </summary>
		/// <returns>
		///     all list of cars in database
		/// </returns>
		[HttpGet]
		[Route(nameof(GetAllCars))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(List<Car>))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(GetAllCars))]
		public IHttpActionResult GetAllCars()
		{
			try
			{
				var allCars = this._service.GetAllCars();
				return this.Ok(allCars);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}

		/// <summary>
		///     API method get car by id
		/// </summary>
		/// <param name="id">
		///     searched car id
		/// </param>
		/// <returns>
		///     found car data
		/// </returns>
		[HttpGet]
		[Route(nameof(GetCarById))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(Car))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(GetCarById))]
		public IHttpActionResult GetCarById(int id)
		{
			try
			{
				var found = this._service.GetCarById(id);
				return this.Ok(found);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
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
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(Car))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(GetCarByName))]
		public IHttpActionResult GetCarByName(string name)
		{
			try
			{
				var found = this._service.GetCarByName(name);
				return this.Ok(found);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}

		/// <summary>
		///     API method to update a specific car by id
		/// </summary>
		/// <param name="updated">
		///     car data to be updated
		/// </param>
		/// <returns>
		///     status of update
		/// </returns>
		[HttpPost]
		[Route(nameof(UpdateCar))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(UpdateCarResponse))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(UpdateCar))]
		public IHttpActionResult UpdateCar(Car updated)
		{
			try
			{
				var carId = updated.Id;
				var status = this._service.Update(updated);
				var error = status ? ErrorEnum.None : ErrorEnum.CarIdNotFound;
				var errorMessage = status ? string.Empty : this._service.GetErrorMessageCarNotFound(carId);
				var response = new UpdateCarResponse(updated, status, error, errorMessage);
				return this.Ok(response);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}

		/// <summary>
		///     API method to calculate discount of buying a specific car with specific quantity
		/// </summary>
		/// <param name="request">
		///     Car purchase data
		/// </param>
		/// <returns>
		///     total discount of purchased car
		/// </returns>
		[HttpPost]
		[Route(nameof(CalculateDiscount))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(CalculateDiscountResponse))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(CalculateDiscount))]
		public IHttpActionResult CalculateDiscount([FromBody] CalculateDiscountRequest request)
		{
			var errorMessage = string.Empty;
			var carName = request.CarName;
			try
			{
				//try to find car by name, then calculate discount
				var found = this._service.GetCarByName(carName);

				//calculate discount after car is found
				var calculateDiscount = this._service.GetPriceAfterDiscounts(carName, request.Quantity);
				var response = new CalculateDiscountResponse(request, found, calculateDiscount, ErrorEnum.None, errorMessage);
				return this.Ok(response);
			}
			catch (CarNotFoundException x)
			{
				errorMessage = this._service.GetErrorMessageCarNotFound(carName);
				var response = new CalculateDiscountResponse(request, null, 0, ErrorEnum.CarNameNotFound, errorMessage);
				return this.Ok(response);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}
	}
}