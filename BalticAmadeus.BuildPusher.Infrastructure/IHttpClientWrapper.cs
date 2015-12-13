using System;
using System.Net.Http;

namespace BalticAmadeus.BuildPusher.Infrastructure
{
	public interface IHttpClientWrapper
	{
		T Get<T>(string url);
		T Get<T>(string url, Action<HttpClient> httpClientConfiguration);
		void Post<T>(string url, T item);
		void Post<T>(string url, T item, Action<HttpClient> httpClientConfiguration);
	}
}
