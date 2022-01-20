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
	public class OpenWeatherData
	{
		public virtual OpenWeatherCoordinate coord { get; set; }
		public virtual List<OpenWeatherSky> weather { get; set; }
		public virtual int visibility { get; set; }
		public virtual OpenWeatherMain main { get; set; }
		public virtual OpenWeatherWind wind { get; set; }

		/// <summary>
		/// UTC time zone UNIX
		/// </summary>
		public virtual long dt { get; set; }

		public virtual string name { get; set; }
	}
}
