using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
	public class OpenWeatherMain
	{
		/// <summary>
		/// temperature, assuming it's farenheit (since there is no unit).
		/// </summary>
		public double temp { get; set; }
		public double humidity { get; set; }
	}
}
