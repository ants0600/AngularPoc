using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface ICustomerRepository
	{
		List<Customer> GetAllCustomers();

		List<Customer> GetCustomersByName(string name);

		bool Insert(Customer inserted);

		bool UpdateById(Customer inserted);

		bool DeleteById(long id);
	}
}
