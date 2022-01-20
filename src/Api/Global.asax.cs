using System.Web;
using System.Web.Http;
using RestfulApi.Models;

namespace RestfulApi
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_End()
		{
			//if (EventRegistration.instanceBus != null)
			//    EventRegistration.instanceBus.Dispose();
			//EventPublisher.Dispose();
		}

		protected void Application_Start()
		{
			////// GlobalConfiguration.Configure(WebApiConfig.Register);
			//////EventRegistration.Start(); //Start all Event Handler Listener   
			////ConnectionManager.ResolveConnection();
			SwaggerStarter.Start();

			GlobalConfiguration.Configure(WebApiConfig.Register);
			//todo: for simple injector SimpleInjectorConfig.Initialize();
			AutofacInjectorConfig.Initialize();
		}
	}
}