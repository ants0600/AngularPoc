using System;
using System.Net;
using System.Web.Http;
using Domain.Service;
using Infrastructure.Common;
using NLog;
using Swashbuckle.Swagger.Annotations;

namespace RestfulApi.Controllers
{
	/// <summary>
	///     simple math API, contains basic math operations like; add, minus
	/// </summary>
	public class SimpleMathController : BaseApiController
	{
		public SimpleMathController(ILogger logger, IGlobalExceptionHandler globalExceptionHandler) : base(logger,
			globalExceptionHandler)
		{
		}

		/// <summary>
		///     Ex: one plus one equals two.
		///     <br></br>
		///     Satu tambah satu sama dengan dua.
		///     <br></br>
		///     比如： 一加一等于二。
		/// </summary>
		/// <param name="a">
		///     first number
		/// </param>
		/// <param name="b">
		///     second number
		/// </param>
		/// <returns>
		///     double, result of number 1 plus number 2
		/// </returns>
		[HttpPost]
		[Route(nameof(Plus))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(double))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(Plus))]
		public IHttpActionResult Plus(double a, double b)
		{
			var value = a + b;
			return this.Ok(value);
		}

		/// <summary>
		///     Ex: one minus one equals zero.
		///     <br></br>
		///     Satu dikurang satu sama dengan dua.
		///     <br></br>
		///     比如： 一减一等于零。
		/// </summary>
		/// <param name="a">
		///     first number
		/// </param>
		/// <param name="b">
		///     second number
		/// </param>
		/// <returns>
		///     double, result of number 1 minus number 2
		/// </returns>
		[HttpPost]
		[Route(nameof(Minus))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(double))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(Minus))]
		public IHttpActionResult Minus(double a, double b)
		{
			var value = a - b;
			return this.Ok(value);
		}
	}
}