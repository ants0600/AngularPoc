using Dapper;
using Domain.Model;
using Domain.Repository;
using Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureDotNetCore.Repository
{
	public class ContactRepository : BaseRepository, IContactRepository
	{
		public ContactRepository(Microsoft.Extensions.Configuration.IConfiguration configuration) : base(configuration)
		{
		}

		public bool Add(ContactUsItem inserted)
		{
			var parameters = new Dictionary<string, object>
			{
				[DataBaseParameters.Email] = inserted.Email,
				[DataBaseParameters.FullName] = inserted.FullName,
				[DataBaseParameters.message] = inserted.Message
			};

			int s = base._connection.Execute(ConstantValues.QUERY_INSERT_CONTACT_US, parameters, null, int.MaxValue, System.Data.CommandType.Text);
			return s > 0;
		}
	}
}
