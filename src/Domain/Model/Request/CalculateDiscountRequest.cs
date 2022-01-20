using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
	public class CalculateDiscountRequest
	{
		public string CarName { get; set; }
		public int Quantity { get; set; }
	}
}
