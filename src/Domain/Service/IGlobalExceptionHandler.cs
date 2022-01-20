using System;
using System.Web.Http.ExceptionHandling;

namespace Domain.Service
{
	public interface IGlobalExceptionHandler : IService
	{
		string GetErrorMessage(Exception x);
		void Handle(ExceptionHandlerContext context);
	}
}