using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
	/// <summary>
	/// POCO class, only to simplify the response from external API.
	/// 
	/// </summary>
	public class WeatherData
	{
		public WeatherData()
		{

		}
		public WeatherData(double latitude, double longitude, DateTime utcDateTime, double windDegree, double windSpeed, int visibility, double farenheit, double celcius, IEnumerable<string> skyConditions, string cityName)
		{
			this.Latitude = latitude;
			this.Longitude = longitude;
			this.UtcDateTime = utcDateTime;
			this.WindDegree = windDegree;
			this.WindSpeed = windSpeed;
			this.Visibility = visibility;
			this.Farenheit = farenheit;
			this.Celcius = celcius;
			this.SkyConditions = string.Join(ConstantValues.COMMA, skyConditions);
			this.CityName = cityName;
		}

		public double Latitude { get; set; } = 0;
		public double Longitude { get; set; } = 0;

		public DateTime UtcDateTime { get; set; } = DateTime.MinValue;

		public string UtcDateTimeLongFormat
		{
			get
			{
				return this.UtcDateTime.ToString(ConstantValues.FORMAT_DD_MMM_YYYY_HH_MM_SS);
			}
		}
		public double WindDegree { get; set; }
		public double WindSpeed { get; set; }
		public int Visibility { get; set; }
		public double Farenheit { get; set; }
		public double Celcius { get; set; }
		public string SkyConditions { get; set; }
		public string CityName { get; set; }
	}
}
