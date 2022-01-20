using Domain.Model;
using Domain.Repository;
using Domain.Service;
using Infrastructure.Extensions;
using Infrastructure.Repository;
using Infrastructure.Service;
using Unity;

namespace ConsoleApp.DependencyInjection
{
	public static class InfrastructureConsoleInjector
	{
		/// <summary>
		///     binding interfaces to concrete classes.
		///     Ex: for repository, service classes
		/// </summary>
		/// <param name="container"></param>
		public static void RegisterTypes(UnityContainer container)
		{
			//domain
			container.RegisterType<IQueryHelpers, QueryHelpers>();
			container.RegisterType<IDateHelper, DateHelper>();
			container.RegisterType<IApplicationSettings, ApplicationSettings>();
			container.RegisterType<IConnectionStrings, ConnectionStrings>();

			//infrastructures
			container.RegisterType<ICarRepository, CarRepository>();
			container.RegisterType<IContactRepository, ContactRepository>();
			container.RegisterType<ICarService, CarService>();
			container.RegisterType<IContactService, ContactService>();
		}
	}
}