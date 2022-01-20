using Domain.Common.Extensions;
using Domain.Model;
using SimpleInjector;

namespace Domain.DependencyInjection
{
	public class DefaultDomainInjector
	{
		public static void RegisterTypes(Container container)
		{
			//common
			container.Register<IQueryHelpers, QueryHelpers>();
			container.Register<IDateHelper, DateHelper>();
			container.Register<IApplicationSettings, ApplicationSettings>();
			container.Register<IConnectionStrings, ConnectionStrings>();
		}
	}
}