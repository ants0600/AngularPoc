using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
	/// <summary>
	/// Domain class for external API.
	/// Ex: to serialize from json string.
	/// </summary>
	public class OpenWeatherSky
	{
		public string main { get; set; }
		public string description { get; set; }
	}
}
