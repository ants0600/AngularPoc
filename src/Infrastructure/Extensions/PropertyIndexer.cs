using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Extensions
{
	public abstract class PropertyIndexer
	{
		protected readonly List<PropertyInfo> _propertyInfoList;

		protected PropertyIndexer()
		{
			////get all string properties
			var allProperties = this.GetType().GetProperties();
			this._propertyInfoList =
				allProperties.Where(a => a.PropertyType == typeof(string) && a.CanWrite).ToList();
		}
	}
}