using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
	public interface IWeatherService
	{
		WeatherResponse GetWeather(string city);

	}
}
