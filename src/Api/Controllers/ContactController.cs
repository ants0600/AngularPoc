using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Domain.Model;
using Domain.Model.Response;
using Domain.Service;
using Infrastructure.Common;
using NLog;
using Swashbuckle.Swagger.Annotations;

namespace RestfulApi.Controllers
{
	public class ContactController : BaseApiController
	{
		private readonly IContactService _service;

		public ContactController(ILogger logger, IGlobalExceptionHandler globalExceptionHandler, IContactService service) : base(logger, globalExceptionHandler)
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
		[Route(nameof(AddContactUs))]
		[SwaggerResponse(HttpStatusCode.OK, NAME_OF_OK, typeof(bool))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, NAME_OF_INTERNAL_SERVER_ERROR, typeof(Exception))]
		[SwaggerOperation(nameof(AddContactUs))]
		public IHttpActionResult AddContactUs(ContactUsItem inserted)
		{
			try
			{
				return this.Ok(this._service.Add(inserted));
			}
			catch (Exception x)
			{
				return this.InternalServerError(x);
			}
		}
	}
}