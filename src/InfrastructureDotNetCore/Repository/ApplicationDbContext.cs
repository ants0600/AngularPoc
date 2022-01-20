using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureDotNetCore.Repository
{
	/// <summary>
	/// todo: dot net entity framework db context
	/// </summary>
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{


		}

		public DbSet<Customer> Customers { get; set; }
	}
}
