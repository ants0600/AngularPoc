using DotNetCoreApi.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Service;
using System.Reflection;
using Domain.Repository;
using InfrastructureDotNetCore.Repository;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using InfrastructureDotNetCore.Service;

namespace DotNetCoreApi.Models
{
	public static class DependencyInjector
	{
		private static readonly Assembly _domainAssembly = typeof(IRepository).Assembly;

		/// <summary>
		/// todo: bind interfaces to concrete classes here
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>

		public static void RegisterTypes(Microsoft.Extensions.DependencyInjection.IServiceCollection services, IConfiguration configuration)
		{
			//todo: dot net entity framework bind db context
			string connectionString = configuration.GetConnectionString(nameof(IConnectionStrings.MainDatabase));
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});
			services.AddDbContext<BookContext>(o => o.UseSqlite("Data source=books.db"));

			//bind interface to concrete class
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IBookRepository, BookRepository>();
			services.AddScoped<IContactRepository, ContactRepository>();
			services.AddScoped<IWeatherService, WeatherService>();
			services.AddScoped<ILocationRepository, LocationRepository>();
			services.AddScoped<IGlobalExceptionHandler, GlobalExceptionHandler>();
			services.AddScoped<IWeatherRepository, WeatherRepository>();

			RegisterDomainTypes(services);
		}

		private static void RegisterDomainTypes(IServiceCollection services)
		{
			services.AddScoped<ICarRepository, CarRepository>();
		}
	}
}
