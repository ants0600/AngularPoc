using Domain.Model.Response;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace XUnitApiTester.Repositories
{
	/// <summary>
	/// use mock data here, not invoking external API, but instead use JSON file
	/// </summary>
	public class MockWeatherRepository : IWeatherRepository
	{
		/// <summary>
		/// mock data from open weather. Ex: as if we invoke
		/// https://api.openweathermap.org/data/2.5/weather?q=paris&appid=c0ab549d625fd3b50fb5b4440b6256f5
		/// </summary>
		private const string MOCK_FILE_NAME = "mockopenweatherdata.json";
		public OpenWeatherData GetOpenWeatherData(string city)
		{
			string fileContent = File.ReadAllText(MOCK_FILE_NAME);
			var value = JsonSerializer.Deserialize<OpenWeatherData>(fileContent);
			return value;
		}
	}
}
