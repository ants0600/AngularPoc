using System.Text;
using System.Text.Json;
using Domain.Model;
using Domain.Model.Response;
using Domain.Repository;
using InfrastructureDotNetCore.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Domain.Resources;
using Domain.Model.Exception;

namespace InfrastructureDotNetCore.Repository
{
	public class WeatherRepository : IWeatherRepository
	{
		private readonly IConfiguration _configuration;

		public WeatherRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected AppSettings AppSettings
		{
			get
			{
				var configurationSection = this._configuration.GetSection(nameof(AppSettings));
				string weatherUrl = configurationSection.GetSection(nameof(AppSettings.WeatherUrl)).Value;
				string weatherApiKey = configurationSection.GetSection(nameof(AppSettings.WeatherApiKey)).Value;
				AppSettings value = new AppSettings(weatherUrl, weatherApiKey);
				return value;
			}
		}

		/// <summary>
		/// invoke external API. ex:
		/// https://api.openweathermap.org/data/2.5/weather?q=london&appid=c0ab549d625fd3b50fb5b4440b6256f5
		/// 
		/// Then convert JSON result to object.
		/// </summary>
		public OpenWeatherData GetOpenWeatherData(string city)
		{
			var url = string.Empty;
			try
			{
				var appSettings = this.AppSettings;
				url = string.Format(appSettings.WeatherUrl, city, appSettings.WeatherApiKey);
				var s = ApiUtility.DownloadString(url, System.Net.Http.HttpMethod.Get, null);
				if (s.IndexOf(FieldText.ErrorCityNotFound, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					var errorMessage = GetInvalidCityErrorMessage(city);
					throw new CityNotFoundException(errorMessage);
				}

				var openWeatherData = JsonSerializer.Deserialize<OpenWeatherData>(s);
				return openWeatherData;
			}
			catch (Exception x)
			{
				if (x.InnerException is HttpRequestException)
				{
					var errorMessage = GetUrlNotReachableMessage(url);
					throw new HttpRequestException(errorMessage);
				}

				throw;
			}
		}
		private static string GetUrlNotReachableMessage(string url)
		{
			return string.Format(FieldText.ErrorUrlNotReachable, url);
		}

		private static string GetInvalidCityErrorMessage(string city)
		{
			return string.Format(FieldText.ErrorInvalidCity, city);
		}

	}
}
