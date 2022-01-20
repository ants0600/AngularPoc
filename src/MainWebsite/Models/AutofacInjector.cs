using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MainWebsite.Models;

namespace MainWebsite.Models
{
	public class AutofacInjector
	{
		public static void Initialize()
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			// Set the dependency resolver to be Autofac.
			var container = builder.Build();

			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}

	}
}


