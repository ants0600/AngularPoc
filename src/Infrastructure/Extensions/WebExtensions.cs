using System;
using System.Net.Http;
using System.Text;
using Domain.Resources;
using Newtonsoft.Json;

public class WebExtensions
{
	/// <summary>
	///     custom deserialize function, log error
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="serialized"></param>
	/// <param name="jsonSerializerSettings"></param>
	/// <returns></returns>
	public T DeserializeObject<T>(string serialized, JsonSerializerSettings jsonSerializerSettings)
	{
		try
		{
			var value = JsonConvert.DeserializeObject<T>(serialized, jsonSerializerSettings);
			return value;
		}
		catch (JsonSerializationException x)
		{
			var message = GetErrorMessageForDeserializing(serialized, x);
			var ex = new JsonSerializationException(message);
			throw ex;
		}
	}

	/// <summary>
	///     try to download as string from url
	/// </summary>
	/// <param name="url"></param>
	/// <returns></returns>
	public string DownloadString(string url)
	{
		using (var client = CreateHttpClient())
		{
			var getAsync = client.GetAsync(url);
			using (var response = getAsync.Result)
			{
				using (var content = response.Content)
				{
					var readAsStringAsync = content.ReadAsStringAsync();
					var value = readAsStringAsync.Result;
					return value;
				}
			}
		}
	}

	/// <summary>
	///     try to download as string from url, with JSON body
	/// </summary>
	/// <param name="url"></param>
	/// <param name="body"></param>
	/// <returns></returns>
	public string DownloadStringWithJsonBody(string url, string body)
	{
		using (var client = CreateHttpClient())
		{
			var requestItem = new StringContent(body, Encoding.UTF8, UtilityResources.ApplicationTypeJson);
			var postAsync = client.PostAsync(url, requestItem);
			var readAsStringAsync = postAsync.Result.Content.ReadAsStringAsync();
			var value = readAsStringAsync.Result;
			return value;
		}
	}

	/// <summary>
	///     special error message for deserializing
	/// </summary>
	/// <param name="serialized"></param>
	/// <param name="x"></param>
	/// <returns></returns>
	public string GetErrorMessageForDeserializing(string serialized, JsonSerializationException x)
	{
		var value = string.Format(FieldText.DeserializingErrorMessageFormat, serialized, Environment.NewLine, x.Message,
			x.StackTrace);
		return value;
	}

	/// <summary>
	///     global ways to serialize object to JSON format, ex: using newtonsoft
	/// </summary>
	public string SerializeObject(object updated, JsonSerializerSettings settings)
	{
		var value = JsonConvert.SerializeObject(updated, settings);
		return value;
	}

	protected HttpClient CreateHttpClient()
	{
		return new HttpClient();
	}
}