using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainWebsite.Models
{
	public class TestService : ITestService
	{
		public string Test()
		{
			return "some test method";
		}
	}
}