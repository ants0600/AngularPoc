using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
	public class Product
	{
		public long Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public long UnitId { get; set; }
		public string MobileCardImageUrl { get; set; }
		public string UnitName { get; set; }
	}
}
