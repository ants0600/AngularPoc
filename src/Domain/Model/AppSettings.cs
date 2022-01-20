using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
	public class AppSettings
	{
		public AppSettings(string weatherUrl, string weatherApiKey)
		{
			WeatherUrl = weatherUrl;
			WeatherApiKey = weatherApiKey;
		}

		public string WeatherUrl { get; }
		public string WeatherApiKey { get; }
	}
}
