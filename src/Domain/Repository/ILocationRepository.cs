using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface ILocationRepository
	{
		List<Country> GetCountries();
		List<City> GetCitiesByCountryId(int countryId);

		List<City> GetCitiesByCountryName(string countryName);
	}
}
