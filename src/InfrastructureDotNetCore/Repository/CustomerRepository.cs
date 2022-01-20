using Domain.Model;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureDotNetCore.Repository
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly ApplicationDbContext _db;

		public CustomerRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public bool DeleteById(long id)
		{
			throw new NotImplementedException();
		}

		public List<Customer> GetAllCustomers()
		{
			var values = this._db.Customers.
				OrderBy(c => c.Name).
				ToList();
			return values;
		}

		public List<Customer> GetCustomersByName(string name)
		{
			var values = this._db.Customers.
				Where(c => c.Name.Contains(name)).
				ToList();
			return values;

		}

		public bool Insert(Customer inserted)
		{
			this._db.Customers.Add(inserted);
			this._db.SaveChanges();
			return true;
		}

		public bool UpdateById(Customer inserted)
		{
			var updated = this._db.Customers.FirstOrDefault(c => c.Id == inserted.Id);
			if (updated == null)
			{
				return false;
			}

			updated.Name = inserted.Name;
			updated.Address = inserted.Address;
			this._db.SaveChanges();

			return true;
		}
	}
}
