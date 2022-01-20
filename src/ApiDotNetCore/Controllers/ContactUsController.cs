using Domain.Model;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApi.Controllers
{
	/// <summary>
	/// api for contact us operation
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]

	public class ContactUsController : ControllerBase, IContactUsController
	{
		private readonly IContactRepository _repository;

		public ContactUsController(IContactRepository repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// add contact us item to database
		/// </summary>
		/// <param name="inserted"></param>
		/// <returns></returns>
		[HttpPost]
		[Route(nameof(AddContactUs))]
		public bool AddContactUs([FromBody] ContactUsItem inserted)
		{
			return _repository.Add(inserted);
		}
	}
}
