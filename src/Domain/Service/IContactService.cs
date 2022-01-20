using Domain.Model;

namespace Domain.Service
{
	public interface IContactService : IService
	{
		bool Add(ContactUsItem inserted);
	}
}