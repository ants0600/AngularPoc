using Dapper;
using Domain.Model;
using Domain.Repository;
using Domain.Resources;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfrastructureDotNetCore.Repository
{
	public class LocationRepository : BaseRepository, ILocationRepository
	{
		public LocationRepository(IConfiguration configuration) : base(configuration)
		{
		}

		public List<City> GetCitiesByCountryId(int countryId)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.id] = countryId
			};
			var values = this._connection.Query<City>(ConstantValues.QUERY_GET_CITIES_BY_COUNTRY_ID, parameters, null, true, int.MaxValue, System.Data.CommandType.Text).ToList();
			return values;
		}

		public List<City> GetCitiesByCountryName(string countryName)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.name] = countryName
			};
			var values = this._connection.Query<City>(ConstantValues.QUERY_GET_CITIES_BY_COUNTRY_NAME, parameters, null, true, int.MaxValue, System.Data.CommandType.Text).ToList();
			return values;
		}

		public List<Country> GetCountries()
		{
			var values = this._connection.Query<Country>(ConstantValues.QUERY_GET_COUNTRIES).ToList();
			return values;
		}
	}
}
