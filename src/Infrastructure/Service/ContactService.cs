using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Repository;
using Domain.Service;

namespace Infrastructure.Service
{
	public class ContactService : IContactService
	{
		private readonly IContactRepository _repository;

		public ContactService(IContactRepository repository)
		{
			this._repository = repository;
		}

		public bool Add(ContactUsItem inserted)
		{
			return this._repository.Add(inserted);
		}
	}
}
