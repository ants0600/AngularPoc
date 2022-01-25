using System.Collections.Generic;
using System.Data;
using Domain.Model;
using Domain.Model.Request;
using Domain.Repository;
using Domain.Resources;
using Domain.Service;

namespace Infrastructure.Repository
{
	public class CartRepository : BaseRepository, ICartRepository
	{
		public CartRepository(IConnectionStrings connectionStrings, IQueryHelpers helper) : base(connectionStrings, helper)
		{
		}

		public List<CartItem> GetCartItemsByUserName(string userName)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.id] = userName
			};
			return this._helper.GetListFromQuery<CartItem>(this._connection, ConstantValues.QUERY_GET_CART_DETAILS_BY_USER_NAME,
				parameters,
				CommandType.Text, int.MaxValue, false);
		}

		public double GetCartTotalPrice(string userName)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.id] = userName
			};
			var value = this._helper.ExecuteScalar<double>(this._connection,
				ConstantValues.QUERY_GET_CART_TOTAL_PRICE_BY_USER_NAME,
				parameters, CommandType.Text, int.MaxValue, false);
			return value;
		}

		public bool Insert(AddCartItemRequest inserted)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.userName] = inserted.UserName,
				[DataBaseParameters.productId] = inserted.ProductId,
				[DataBaseParameters.quantity] = inserted.Quantity
			};
			var executeNonQuery = this._helper.ExecuteNonQuery(this._connection, ConstantValues.QUERY_INSERT_CART_ITEM,
				parameters, CommandType.Text, int.MaxValue);
			return executeNonQuery > 0;
		}
	}
}