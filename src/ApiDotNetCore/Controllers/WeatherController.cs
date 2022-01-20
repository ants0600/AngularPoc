using Domain.Model;
using Domain.Model.Response;
using Domain.Repository;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApi.Controllers
{
	/// <summary>
	/// api for contact us operation
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]

	public class WeatherController : ControllerBase
	{
		private readonly IWeatherService _weatherService;

		public WeatherController(IWeatherService weatherService)
		{
			_weatherService = weatherService;
		}

		/// <summary>
		/// get weather by city, will invoke external API.
		/// </summary>
		/// <param name="city"></param>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetWeatherByCity))]
		public WeatherResponse GetWeatherByCity(string city)
		{
			return this._weatherService.GetWeather(city);
		}
	}
}
