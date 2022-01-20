using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Domain.Repository;
using Domain.Service;
using Infrastructure.Repository;
using NLog;

namespace RestfulApi.Models
{
	public class AutofacInjectorConfig
	{
		public static void Initialize()
		{
			var builder = new ContainerBuilder();

			// Register your MVC controllers. (MvcApplication is the name of
			// the class in Global.asax.)
			var presentationAssembly = typeof(WebApiApplication).Assembly;
			builder.RegisterApiControllers(presentationAssembly);

			//builder.RegisterType<ApiTestService>().As<IApiTestService>().InstancePerRequest();

			// Register repositories, services
			var domainAssembly = typeof(IRepository).Assembly;
			var infrastructureAssembly = typeof(BaseRepository).Assembly;
			builder.RegisterAssemblyTypes(infrastructureAssembly, infrastructureAssembly, domainAssembly)
				.Where(x => x.IsClass && !x.IsAbstract && (typeof(IRepository).IsAssignableFrom(x) || typeof(IService).IsAssignableFrom(x)))
				.AsImplementedInterfaces()
				.InstancePerDependency();



			var currentClassLogger = LogManager.GetCurrentClassLogger();
			builder.Register(c => currentClassLogger).As<ILogger>();

			//build
			var container = builder.Build();
			var resolver = new AutofacWebApiDependencyResolver(container);
			GlobalConfiguration.Configuration.DependencyResolver = resolver;
		}
	}
}