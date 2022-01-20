using Domain.Model;
using Domain.Model.Exception;
using Domain.Model.Response;
using Domain.Repository;
using Domain.Resources;
using Domain.Service;
using InfrastructureDotNetCore.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace InfrastructureDotNetCore.Service
{
	public class WeatherService : IWeatherService
	{
		private readonly IGlobalExceptionHandler _globalExceptionHandler;
		private readonly IConfiguration _configuration;
		private readonly IWeatherRepository _weatherRepository;

		public WeatherService(IGlobalExceptionHandler globalExceptionHandler, IWeatherRepository weatherRepository)
		{
			_globalExceptionHandler = globalExceptionHandler;
			_weatherRepository = weatherRepository;
		}

		/// <summary>
		/// invoke external API. ex:
		/// https://api.openweathermap.org/data/2.5/weather?q=london&appid=c0ab549d625fd3b50fb5b4440b6256f5
		/// 
		/// Then convert JSON result to object.
		/// </summary>
		public virtual WeatherResponse GetWeather(string city)
		{
			var value = new WeatherResponse();

			try
			{
				city = $"{city}".Trim();
				if (string.IsNullOrEmpty(city))
				{
					value.ErrorCode = ErrorEnum.CityNameIsEmpty;
					value.ErrorMessage = FieldText.ErrorEmptyCityName;
					return value;
				}

				var openWeatherData = this._weatherRepository.GetOpenWeatherData(city);
				value.OriginalResponse = openWeatherData;
				value.WeatherData = ConvertWeather(openWeatherData);

				return value;
			}
			catch (HttpRequestException x)
			{
				value.ErrorCode = ErrorEnum.UrlNotReachable;
				value.ErrorMessage = x.Message;
				return value;
			}
			catch (CityNotFoundException x)
			{
				value.ErrorCode = ErrorEnum.CityNotFound;
				value.ErrorMessage = x.Message;
				return value;
			}
			catch (Exception x)
			{
				value.ErrorMessage = this._globalExceptionHandler.GetErrorMessage(x);
				return value;
			}
		}

		/// <summary>
		/// todo: this is not testable, because we DONT need to mock data in this process.
		/// Assuming the temperature is always in Farenheit.
		/// </summary>
		public static WeatherData ConvertWeather(OpenWeatherData source)
		{
			if (source == null)
			{
				return null;
			}

			//coordinate
			double latitude = 0;
			double longitude = 0;
			var coord = source.coord;
			if (coord != null)
			{
				latitude = coord.lat;
				longitude = coord.lon;
			}

			var utcDateTime = DateUtility.UnixTimeStampToDateTime(source.dt);

			//wind
			double windDegree = 0;
			double windSpeed = 0;
			var wind = source.wind;
			if (wind != null)
			{
				windDegree = wind.deg;
				windSpeed = wind.speed;
			}

			var visibility = source.visibility;

			//temperature, humidity
			double farenheit = 0;
			double humidity = 0;
			double celcius = 0;
			var main = source.main;
			if (main != null)
			{
				farenheit = main.temp;
				humidity = main.humidity;
				celcius = GetCelciusFromFahrenheit(farenheit);
			}

			//sky conditions
			var skyConditions = source.weather.Select(a => $"{a.description}").ToList();
			return new WeatherData(latitude, longitude, utcDateTime, windDegree, windSpeed, visibility, farenheit, celcius, skyConditions, source.name);
		}

		/// <summary>
		/// This is ETERNAL TRUTH.
		/// Doesn't need mock (like repository, SQL SERVER vs JSON).
		/// No matter in test or real data. The formula WILL NOT change.
		/// Ex: 1 F = 0.556 C
		/// </summary>
		public static double GetCelciusFromFahrenheit(double farenheit)
		{
			return (farenheit - 32) * 5 / 9;
		}


	}
}
