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
	public class OpenWeatherCoordinate
	{
		public virtual double lon { get; set; }
		public virtual double lat { get; set; }
	}
}
