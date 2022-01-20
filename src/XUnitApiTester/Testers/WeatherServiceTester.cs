using Domain.Repository;
using Domain.Service;
using InfrastructureDotNetCore.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace XUnitApiTester
{
	/// <summary>
	/// use mock data here to test converting farenheit to celcius
	/// </summary>
	public class WeatherServiceTester
	{
		private readonly ITestOutputHelper _outputHelper;
		private readonly IWeatherService _weatherService;

		public WeatherServiceTester(IWeatherService weatherService, ITestOutputHelper outputHelper)
		{
			_outputHelper = outputHelper;
			_weatherService = weatherService;
		}

		/// <summary>
		/// we read mock data here
		/// </summary>
		[Fact]
		public void ConvertFahrenheitToCelcius_MustBe_Successful()
		{
			//arrange
			var weatherResponse = this._weatherService.GetWeather(WeatherApiTester.UNDEFINED_CITY);
			Domain.Model.WeatherData weatherData = weatherResponse.WeatherData;
			var celcius = weatherData.Celcius;
			var farenheit = weatherData.Farenheit;
			this._outputHelper.WriteLine(weatherData.CityName);
			this._outputHelper.WriteLine(nameof(farenheit));
			this._outputHelper.WriteLine(farenheit.ToString());
			this._outputHelper.WriteLine(nameof(celcius));
			this._outputHelper.WriteLine(celcius.ToString());

			var match = WeatherService.GetCelciusFromFahrenheit(farenheit) == celcius;
			Assert.True(match);

		}
	}
}
