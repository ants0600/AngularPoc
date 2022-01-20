using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
	/// <summary>
	/// weather response. Ex: get from external API
	/// </summary>
	public class WeatherResponse
	{
		public OpenWeatherData OriginalResponse { get; set; }
		public WeatherData WeatherData { get; set; }
		public ErrorEnum ErrorCode { get; set; } = ErrorEnum.None;

		public string ErrorMessage { get; set; } = string.Empty;
		
	}
}
