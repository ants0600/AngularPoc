using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Exception
{
	public class CityNotFoundException : System.Exception
	{
		public CityNotFoundException(string message) : base(message)
		{
		}

	}
}
