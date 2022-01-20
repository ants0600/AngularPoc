using InfrastructureTester.DependencyInjection;
using Unity;

namespace InfrastructureTester
{
	public abstract class BaseTester
	{
		protected readonly UnityContainer _container = new UnityContainer();

		protected BaseTester()
		{
			InfrastructureTesterInjector.RegisterTypes(this._container);
		}
	}
}