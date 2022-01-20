using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.IO;

namespace InfrastructureDotNetCore.Utility
{
	public static class ApiUtility
	{
		private static readonly HttpClient _client = new HttpClient();
		public static string DownloadString(string url, HttpMethod method, HttpContent jsonContent)
		{
			var webRequest = new HttpRequestMessage(method, url);
			if (jsonContent != null)
			{
				webRequest.Content = jsonContent;
			}

			return _client.SendAsync(webRequest).Result.Content.ReadAsStringAsync().Result;
		}
	}
}
