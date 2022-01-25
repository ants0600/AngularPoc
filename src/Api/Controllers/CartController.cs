using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Domain.Model;
using Domain.Model.Request;
using Domain.Repository;
using Domain.Service;
using NLog;
using Swashbuckle.Swagger.Annotations;

namespace RestfulApi.Controllers
{
	/// <summary>
	///     all operations about cart
	/// </summary>
	public class CartController : BaseApiController
	{
		private readonly ICartRepository _cartRepository;

		public CartController(ILogger logger, IGlobalExceptionHandler globalExceptionHandler,
			ICartRepository cartRepository) : base(logger,
			globalExceptionHandler)
		{
			this._cartRepository = cartRepository;
		}

		/// <summary>
		///     get cart total price from database by user name
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetCartTotalPriceByUserName))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(double))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(GetCartTotalPriceByUserName))]
		public IHttpActionResult GetCartTotalPriceByUserName(string userName)
		{
			try
			{
				var values = this._cartRepository.GetCartTotalPrice(userName);
				return this.Ok(values);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}

		/// <summary>
		///     get cart items from database by user name
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetCartItemsByUserName))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(List<CartItem>))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(GetCartItemsByUserName))]
		public IHttpActionResult GetCartItemsByUserName(string userName)
		{
			try
			{
				var values = this._cartRepository.GetCartItemsByUserName(userName);
				return this.Ok(values);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}

		/// <summary>
		/// insert a cart item to database
		/// </summary>
		/// <param name="request">
		/// inserted data
		/// </param>
		/// <returns></returns>
		[HttpPost]
		[Route(nameof(AddCartItem))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(bool))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(AddCartItem))]
		public IHttpActionResult AddCartItem([FromBody]AddCartItemRequest request)
		{
			try
			{
				var value = this._cartRepository.Insert(request);
				return this.Ok(value);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}
	}
}