using Domain.Repository;
using Domain.Service;
using Infrastructure.Repository;
using Infrastructure.Service;
using SimpleInjector;

namespace Infrastructure.DependencyInjection
{
	public static class DefaultInfrastructureInjector
	{
		/// <summary>
		///     binding interfaces to concrete classes.
		///     Ex: for repository, service classes
		/// </summary>
		public static void RegisterTypes(Container container, Lifestyle lifestyle)
		{
			container.Register<ICarRepository, CarRepository>(lifestyle);
			container.Register<IContactRepository, ContactRepository>(lifestyle);
			container.Register<ICarService, CarService>(lifestyle);
			container.Register<IContactService, ContactService>(lifestyle);
		}
	}
}