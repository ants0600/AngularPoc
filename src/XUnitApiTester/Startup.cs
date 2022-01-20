using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.DependencyInjection;

namespace XUnitApiTester
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			MockInjector.RegisterTypes(services);
		}
	}
}