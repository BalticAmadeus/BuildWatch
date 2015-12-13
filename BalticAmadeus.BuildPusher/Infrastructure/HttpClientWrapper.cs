using System;
using System.Net;
using System.Net.Http;

namespace BalticAmadeus.BuildPusher.Infrastructure
{
	public class HttpClientWrapper : IHttpClientWrapper
	{
		public T Get<T>(string url, Func<HttpClient> httpClientFactory)
		{
			using (var httpClient = httpClientFactory())
			{
				var response = httpClient.GetAsync(url).Result;
				if (response.StatusCode != HttpStatusCode.OK)
					throw new HttpException(response.ReasonPhrase, response.StatusCode.ToString());

				return response.Content.As<T>();
			}
		}

		public T Get<T>(string url)
		{
			return Get<T>(url, () => new HttpClient());
		}

		public void Post<T>(string url, T item, Func<HttpClient> httpClientFactory)
		{
			using (var httpClient = httpClientFactory())
			{
				var content = item.AsJsonStringContent();

				var response = httpClient.PostAsync(url, content).Result;
				if (response.StatusCode != HttpStatusCode.OK)
					throw new HttpException(response.ReasonPhrase, response.StatusCode.ToString());
			}
		}

		public void Post<T>(string url, T item)
		{
			Post(url, item, () => new HttpClient());
		}
	}
}