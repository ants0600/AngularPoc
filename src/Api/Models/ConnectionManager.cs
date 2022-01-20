using System;
using System.Configuration;
using System.Reflection;
using Common.Helpers;
using NLog;

namespace RestfulApi.Models
{
	public class ConnectionManager
	{
		public ConnectionManager()
		{
		}

		public static void ResolveConnection()
		{
			Logger logger = LogManager.GetCurrentClassLogger();
			var writable = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

			foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
			{
				try
				{
					writable.SetValue(css, false);
					css.ConnectionString = css.ConnectionString.Decrypt();
                }
				catch (Exception ex)
				{
					logger.Error(ex.ToString());
				}
			}
		}
	}
}