using System.Collections.Generic;
using System.Data;
using Domain.Resources;
using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Repository
{
	public class ContactRepository : BaseRepository, IContactRepository
	{
		public ContactRepository(IConnectionStrings connectionStrings, IQueryHelpers helper) : base(connectionStrings, helper)
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

			var executeNonQuery = this._helper.ExecuteNonQuery(this._connection, ConstantValues.QUERY_INSERT_CONTACT_US,
				parameters,
				CommandType.Text, int.MaxValue);
			return executeNonQuery > 0;
		}
	}
}