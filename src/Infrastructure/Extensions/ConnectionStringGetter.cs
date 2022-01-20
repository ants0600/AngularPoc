using System;
using System.Configuration;

namespace Infrastructure.Extensions
{
	public abstract class ConnectionStringGetter : PropertyIndexer
	{
		protected ConnectionStringGetter()
		{
			this.GetConnectionStringsFromWebConfig();
		}

		private void GetConnectionStringsFromWebConfig()
		{
			var properties = this._propertyInfoList;
			if (properties.Count <= 0)
			{
				const string ERROR_MESSAGE = nameof(this.GetConnectionStringsFromWebConfig);
				throw new NullReferenceException(ERROR_MESSAGE);
			}

			foreach (var property in properties)
			{
				var propertyName = property.Name;
				var propertyValue = ConfigurationManager.ConnectionStrings[propertyName].ConnectionString;

				property.SetValue(this, propertyValue, null);
			}
		}
	}
}