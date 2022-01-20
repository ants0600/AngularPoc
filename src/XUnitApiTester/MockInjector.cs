using Domain.Repository;
using Domain.Service;
using InfrastructureDotNetCore.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUnitApiTester.Repositories;

namespace XUnitApiTester
{
	public static class MockInjector
	{
		/// <summary>
		/// only mock repository, but not the service.
		/// </summary>
		public static void RegisterTypes(IServiceCollection services)
		{
			services.AddScoped<IWeatherRepository, MockWeatherRepository>();
			services.AddScoped<IWeatherService, WeatherService>();
			services.AddScoped<IGlobalExceptionHandler, GlobalExceptionHandler>();
		}
	}
}
