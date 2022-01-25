using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repository;
using Domain.Resources;
using Domain.Service;

namespace Infrastructure.Repository
{
	public class ProductRepository : BaseRepository, IProductRepository
	{
		public ProductRepository(IConnectionStrings connectionStrings, IQueryHelpers helper) : base(connectionStrings, helper)
		{
		}

		public List<Product> GetProducts()
		{
			return this._helper.GetListFromQuery<Product>(this._connection, ConstantValues.QUERY_SELECT_PRODUCTS, CommandType.Text,
				int.MaxValue, false);
		}

		public Product GetProductById(long id)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.id] = id
			};
			var values = this._helper.GetListFromQuery<Product>(this._connection, ConstantValues.QUERY_GET_PRODUCT_BY_ID, parameters,
				CommandType.Text, int.MaxValue, false);
			return values.FirstOrDefault();
		}
	}
}
