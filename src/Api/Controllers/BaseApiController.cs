using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using Domain.Service;
using Infrastructure.Common;
using NLog;

namespace RestfulApi.Controllers
{
	/// <summary>
	///     base api for all controllers in this project
	/// </summary>
	public abstract class BaseApiController : ApiController
	{
		public const string NAME_OF_OK = nameof(HttpStatusCode.OK);
		public const string NAME_OF_INTERNAL_SERVER_ERROR = nameof(HttpStatusCode.InternalServerError);

		/// <summary>
		///     for global error handler
		/// </summary>
		protected readonly IGlobalExceptionHandler _globalExceptionHandler;

		/// <summary>
		///     for logging everything
		/// </summary>
		protected readonly ILogger _logger;

		/// <summary>
		///     basic constructor for all api
		/// </summary>
		protected BaseApiController(ILogger logger, IGlobalExceptionHandler globalExceptionHandler)
		{
			this._logger = logger;
			this._globalExceptionHandler = globalExceptionHandler;
		}

		/// <summary>
		///     global ways to handle exception,
		///     ex: log error, then follow the base class behavior
		/// </summary>
		protected override ExceptionResult InternalServerError(Exception x)
		{
			var message = this._globalExceptionHandler.GetErrorMessage(x);
			this._logger.Error(x, message);

			return base.InternalServerError(x);
		}
	}
}