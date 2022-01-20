using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface IWeatherRepository
	{
		OpenWeatherData GetOpenWeatherData(string city);
	}
}
