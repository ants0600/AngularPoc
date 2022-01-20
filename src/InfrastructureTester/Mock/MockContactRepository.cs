using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repository;

namespace InfrastructureTester.Mock
{
	public class MockContactRepository : IContactRepository
	{
		public bool Add(ContactUsItem inserted)
		{
			throw new NotImplementedException();
		}
	}
}
