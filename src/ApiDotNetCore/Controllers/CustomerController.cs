using Domain.Model;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApi.Controllers
{
	/// <summary>
	/// API methods about cars, all operations about car data
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]

	class CustomerController : ControllerBase
	{
		private readonly ICustomerRepository _repository;

		public CustomerController(ICustomerRepository repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// get all customers from db
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetAllCustomers))]

		public List<Customer> GetAllCustomers()
		{
			return this._repository.GetAllCustomers();
		}

		/// <summary>
		/// filter customer list by name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[HttpGet]
		[Route(nameof(GetCustomersByName))]
		public List<Customer> GetCustomersByName(string name)
		{
			return this._repository.GetCustomersByName(name);
		}

		/// <summary>
		/// insert 1 row into db
		/// </summary>
		/// <param name="inserted"></param>
		/// <returns></returns>
		[HttpPost]
		[Route(nameof(Insert))]
		public bool Insert(Customer inserted)
		{
			return this._repository.Insert(inserted);
		}

		[HttpPost]
		[Route(nameof(UpdateById))]

		public bool UpdateById(Customer updated)
		{
			return this._repository.UpdateById(updated);
		}
	}
}
