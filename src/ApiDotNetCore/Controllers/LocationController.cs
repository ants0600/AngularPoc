using Domain.Model;
using Domain.Model.Response;
using Domain.Repository;
using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApi.Controllers
{
	/// <summary>
	/// api for location. Ex: get cities, get countries
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]

	public class LocationController : ControllerBase
	{
		private readonly ILocationRepository _locationRepository;

		public LocationController(ILocationRepository locationRepository)
		{
			_locationRepository = locationRepository;
		}

		/// <summary>
		/// get cities by country id, read from database
		/// </summary>
		/// <param name="country"></param>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetCitiesByCountryName))]
		public List<City> GetCitiesByCountryName(string country)
		{
			return this._locationRepository.GetCitiesByCountryName(country);
		}

		/// <summary>
		/// get countries
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetCountries))]
		public List<Country> GetCountries()
		{
			return this._locationRepository.GetCountries();
		}
	}
}
