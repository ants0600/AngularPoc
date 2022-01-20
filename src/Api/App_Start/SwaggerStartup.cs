using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace RestfulApi
{
	/// <summary>
	/// </summary>
	public class SwaggerStartup
	{
		public static string XmlCommentsFilePath
		{
			get
			{
				var basePath = AppDomain.CurrentDomain.RelativeSearchPath;
				var fileName = typeof(SwaggerStartup).GetTypeInfo().Assembly.GetName().Name + ".xml";
				return Path.Combine(basePath, fileName);
			}
		}

		public static List<SwaggerSettings> GetSwaggerConfig()
		{
			try
			{
				var swaggers = new List<SwaggerSettings>();
				var config = (SwaggerConfig) ConfigurationManager.GetSection("swaggerConfig/SwaggerSettings");
				foreach (SwaggerSettings swagger in config.Swagger)
				{
					swaggers.Add(swagger);
				}

				return swaggers;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}