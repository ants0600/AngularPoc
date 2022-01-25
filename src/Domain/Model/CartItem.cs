using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
	public class CartItem : Product
	{
		public long ProductId { get; set; }
		public int Quantity { get; set; }
		public double TotalPrice { get; set; }
	}
}
