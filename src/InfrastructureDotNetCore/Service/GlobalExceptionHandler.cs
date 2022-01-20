using Domain.Model;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureDotNetCore.Service
{
	public class GlobalExceptionHandler : IGlobalExceptionHandler
	{
		public string GetErrorMessage(Exception x)
		{
			return string.Format(ConstantValues.ERROR_MESSAGE_FORMAT, x, x.Message, x.StackTrace);
		}

		public void Handle(System.Web.Http.ExceptionHandling.ExceptionHandlerContext context)
		{

		}
	}
}
