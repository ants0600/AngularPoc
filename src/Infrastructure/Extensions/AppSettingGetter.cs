using System;
using System.Configuration;

namespace Infrastructure.Extensions
{
	/// <summary>
	///     Summary description for AppSettingGetter
	/// </summary>
	public abstract class AppSettingGetter : PropertyIndexer
	{
		#region constructors

		protected AppSettingGetter()
		{
			this.GetAppSettingsFromWebConfig();
		}

		#endregion

		#region user defined methods

		/// <summary>
		///     Gets the app settings.
		/// </summary>
		private void GetAppSettingsFromWebConfig()
		{
			////set string properties on this class
			var properties = this._propertyInfoList;
			if (properties.Count <= 0)
			{
				const string MESSAGE = nameof(this.GetAppSettingsFromWebConfig);
				throw new NullReferenceException(MESSAGE);
			}

			foreach (var property in properties)
			{
				var propertyName = property.Name;
				var propertyValue = ConfigurationManager.AppSettings[propertyName];

				property.SetValue(this, propertyValue, null);
			}
		}

		#endregion
	}
}