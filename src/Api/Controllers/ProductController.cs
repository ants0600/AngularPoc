using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Domain.Model;
using Domain.Repository;
using Domain.Service;
using NLog;
using Swashbuckle.Swagger.Annotations;

namespace RestfulApi.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IProductRepository _productRepository;

		public ProductController(ILogger logger, IGlobalExceptionHandler globalExceptionHandler, IProductRepository productRepository) : base(logger,
			globalExceptionHandler)
		{
			this._productRepository = productRepository;
		}

		/// <summary>
		///     get all products from database
		/// </summary>
		[HttpGet]
		[Route(nameof(GetAllProducts))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(List<Product>))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(GetAllProducts))]
		public IHttpActionResult GetAllProducts()
		{
			try
			{
				var values = this._productRepository.GetProducts();
				return this.Ok(values);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}

		/// <summary>
		///     get all products from database
		/// </summary>
		[HttpGet]
		[Route(nameof(GetProductById))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(Product))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(GetProductById))]
		public IHttpActionResult GetProductById(long id)
		{
			try
			{
				var value = this._productRepository.GetProductById(id);
				return this.Ok(value);
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}
	}
}