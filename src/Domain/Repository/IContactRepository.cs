using Domain.Model;

namespace Domain.Repository
{
	public interface IContactRepository : IRepository
	{
		bool Add(ContactUsItem inserted);
	}
}