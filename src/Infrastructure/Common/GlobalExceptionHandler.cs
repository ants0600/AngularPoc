using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Domain.Service;

namespace Infrastructure.Common
{
	public class GlobalExceptionHandler : ExceptionHandler, IGlobalExceptionHandler
	{
		/// <summary>
		///     {0}: message, {1}: stack trace
		/// </summary>
		private const string ERROR_MESSAGE_FORMAT = @"
Message = {0}

Stack Trace: {1}
";

		public string GetErrorMessage(Exception x)
		{
			var value = string.Format(ERROR_MESSAGE_FORMAT, x.Message, x.StackTrace);
			return value;
		}
		// TODO To define better exception hierarchy, then update this.
		//private static readonly IReadOnlyDictionary<Type, HttpStatusCode> Mapping =
		//    new Dictionary<Type, HttpStatusCode>
		//    {
		//        { typeof(AutoMapperMappingException), HttpStatusCode.BadRequest},
		//        { typeof(DbUpdateException), HttpStatusCode.Conflict}
		//    };

		public override void Handle(ExceptionHandlerContext context)
		{
			var statusCode = HttpStatusCode.InternalServerError;

			//var exceptionType = context.Exception.GetType();
			//if (Mapping.ContainsKey(exceptionType))
			//{
			//    statusCode = Mapping[exceptionType];
			//}

			var response = context.Request.CreateResponse(
				statusCode, context.Exception.ToString());

			context.Result = new ResponseMessageResult(response);
		}
	}
}