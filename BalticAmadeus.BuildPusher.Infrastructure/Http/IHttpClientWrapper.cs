using System;
using System.Net.Http;

namespace BalticAmadeus.BuildPusher.Infrastructure.Http
{
	public interface IHttpClientWrapper
	{
		T Get<T>(string url);
		T Get<T>(string url, Func<HttpClient> httpClientFactory);
		void Post<T>(string url, T item);
		void Post<T>(string url, T item, Func<HttpClient> httpClientFactory);
	}
}
