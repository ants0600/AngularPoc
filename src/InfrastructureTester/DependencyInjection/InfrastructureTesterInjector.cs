using Domain.Model;
using Domain.Repository;
using Domain.Service;
using Infrastructure.Extensions;
using Infrastructure.Repository;
using Infrastructure.Service;
using InfrastructureTester.Mock;
using Unity;

namespace InfrastructureTester.DependencyInjection
{
	public static class InfrastructureTesterInjector
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

			//todo: sample only, either use mock data for unit testing
			//or use real repository for integration testing, which will operate the database (ex: DEV db)
			container.RegisterType<ICarRepository, MockCarRepository>();
			container.RegisterType<IContactRepository, ContactRepository>();
			container.RegisterType<ICarService, CarService>();
			container.RegisterType<IContactService, ContactService>();
		}
	}
}