using System.Web.Http;
using Domain.DependencyInjection;
using Infrastructure.DependencyInjection;
using NLog;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace RestfulApi.Models
{
	/// <summary>
	///     Simpleinjector webapi integration
	///     https://simpleinjector.readthedocs.io/en/latest/webapiintegration.html
	/// </summary>
	public static class SimpleInjectorConfig
	{
		/// <summary>
		/// </summary>
		public static Container ActiveContainer { get; private set; }

		/// <summary>
		/// </summary>
		public static void Initialize()
		{
			// Create the container as usual.
			ActiveContainer = new Container();

			//set lifestyle for dependency injection functions
			ActiveContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
			//ActiveContainer.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

			// Register your types, for instance:
			BindInterfacesToClasses(ActiveContainer);

			// This is an extension method from the integration package.
			ActiveContainer.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			ActiveContainer.Verify();

			GlobalConfiguration.Configuration.DependencyResolver =
				new SimpleInjectorWebApiDependencyResolver(ActiveContainer);
		}

		/// <summary>
		///     bind every interface to concreate classes
		/// </summary>
		private static void BindInterfacesToClasses(Container container)
		{
			////// Register your types, for instance using the scoped lifestyle:
			////var scopedLifestyle = Lifestyle.Scoped;

			//to define scopes here
			var scopedLifestyle = Lifestyle.Transient;

			//bind interfaces to classes

			//domain projects
			DefaultDomainInjector.RegisterTypes(container);

			//infrastructure projects
			DefaultInfrastructureInjector.RegisterTypes(container, scopedLifestyle);

			//only in this project
			Logger CurrentClassLogger()
			{
				return LogManager.GetCurrentClassLogger();
			}

			container.Register<ILogger>(CurrentClassLogger);
			container.Register(CurrentClassLogger);
		}
	}
}