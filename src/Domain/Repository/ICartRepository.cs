using System.Collections.Generic;
using Domain.Model;
using Domain.Model.Request;

namespace Domain.Repository
{
	public interface ICartRepository : IRepository
	{
		List<CartItem> GetCartItemsByUserName(string userName);
		double GetCartTotalPrice(string userName);
		bool Insert(AddCartItemRequest inserted);
	}
}