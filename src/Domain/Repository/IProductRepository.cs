using System.Collections.Generic;
using Domain.Model;

namespace Domain.Repository
{
	public interface IProductRepository : IRepository
	{
		List<Product> GetProducts();
		Product GetProductById(long id);
	}
}