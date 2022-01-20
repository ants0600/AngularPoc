using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;
using Xunit.Sdk;
using XUnitApiTester;

namespace XUnitInfraTester
{
	public abstract class BaseApiTester
	{
		protected readonly Settings _settings;
		private readonly HttpClient _client = new();

		public BaseApiTester()
		{
			// Build a config object, using env vars and JSON providers.
			IConfiguration config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables()
				.Build();

			// Get values from the config given their key and their target type.
			this._settings = config.GetRequiredSection("Settings").Get<Settings>();
		}

		protected string DownloadString(string url, HttpMethod method, JsonContent jsonContent)
		{
			var webRequest = new HttpRequestMessage(method, url);
			if (jsonContent != null)
			{
				webRequest.Content = jsonContent;
			}

			var response = this._client.Send(webRequest);

			using var reader = new StreamReader(response.Content.ReadAsStream());

			return reader.ReadToEnd();
		}
	}
}
