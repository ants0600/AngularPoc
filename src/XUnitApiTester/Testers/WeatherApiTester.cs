using Domain.Model;
using Domain.Model.Response;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using XUnitInfraTester;

namespace XUnitApiTester
{
	public class WeatherApiTester : BaseApiTester
	{
		private const string URL_GET_WEATHER_BY_CITY = "/api/Weather/GetWeatherByCity?city={0}";
		private readonly ITestOutputHelper _outputHelper;
		public const string UNDEFINED_CITY = "gotham";

		public WeatherApiTester(ITestOutputHelper outputHelper)
		{
			_outputHelper = outputHelper;
		}

		/// <summary>
		/// given several use cases / permutations.
		/// Weather API must return value.
		/// </summary>
		[Theory]
		[InlineData("", "", ErrorEnum.CityNameIsEmpty)]
		[InlineData("bandung", "", ErrorEnum.None)]
		[InlineData(UNDEFINED_CITY, "", ErrorEnum.CityNotFound)]
		public void GetWeather_Must_ReturnValue(string city, string errorMessage, ErrorEnum errorCode)
		{
			try
			{
				//arrange, act
				var url = base._settings.ApiUrl + URL_GET_WEATHER_BY_CITY;
				url = string.Format(url, city);

				//assert
				string result = base.DownloadString(url, HttpMethod.Get, null);
				this._outputHelper.WriteLine(result);

				var response = JsonConvert.DeserializeObject<WeatherResponse>(result);
				var errorEnum = response.ErrorCode;
				if (string.IsNullOrEmpty(city))
				{
					errorEnum.Should().Equals(errorCode);
					return;
				}

				if (city.Equals(UNDEFINED_CITY, StringComparison.OrdinalIgnoreCase))
				{
					errorEnum.Should().Equals(errorCode);
					return;
				}

				Assert.True(response.WeatherData.CityName.Equals(city, StringComparison.OrdinalIgnoreCase));
			}
			catch (Exception x)
			{
				this._outputHelper.WriteLine(x.Message);
				this._outputHelper.WriteLine(x.StackTrace);
			}
		}
	}
}
