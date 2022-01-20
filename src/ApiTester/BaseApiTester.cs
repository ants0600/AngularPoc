using System.Configuration;
using System.Net.Http;
using System.Text;

namespace RestfulApiTester
{
	public abstract class BaseApiTester
	{
		protected readonly string _apiUrl;

		protected BaseApiTester()
		{
			this._apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
		}

		public static string PostUrl(string url, string body)
		{
			var client = new HttpClient();
			HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");
			var response = client.PostAsync(url, content);
			if (response == null)
			{
				return string.Empty;
			}


			var result = response.Result;
			if (result == null)
			{
				return string.Empty;
			}

			var httpContent = result.Content;
			if (httpContent == null)
			{
				return string.Empty;
			}

			var s = httpContent.ReadAsStringAsync().Result;
			return s;
		}
	}
}