using System;
using System.Net.Http;

namespace BalticAmadeus.BuildPusher.Infrastructure
{
	public class ExceptionSafeHttpClientWrapper : IHttpClientWrapper
	{
		private readonly IHttpClientWrapper _httpClientWrapper;
		private readonly ILoggingService _loggingService;

		public ExceptionSafeHttpClientWrapper(IHttpClientWrapper httpClientWrapper, ILoggingService loggingService)
		{
			_httpClientWrapper = httpClientWrapper;
			_loggingService = loggingService;
		}

		public T Get<T>(string url)
		{
			return Get<T>(url, client => { });
		}

		public T Get<T>(string url, Action<HttpClient> httpClientConfiguration)
		{
			try
			{
				_loggingService.Debug($"Making GET request to resource at {url}");

				var item = _httpClientWrapper.Get<T>(url, httpClientConfiguration);

				_loggingService.Debug($"Server returned {item.AsJsonString()} resource for GET request from {url}");

				return item;
			}
			catch (HttpException httpException)
			{
				_loggingService.Error($"Server returned {httpException.HttpCode} code for GET request from {url}", httpException);
				return default(T);
			}
			catch (Exception exception)
			{
				_loggingService.Error($"An exception occurred before GET request to {url}", exception);
				return default(T);
			}
		}

		public void Post<T>(string url, T item)
		{
			Post(url, item, client => { });
		}

		public void Post<T>(string url, T item, Action<HttpClient> httpClientConfiguration)
		{
			try
			{
				_loggingService.Debug($"Making POST {item.AsJsonString()} request to resource at {url}");

				_httpClientWrapper.Post(url, item, httpClientConfiguration);

				_loggingService.Debug($"Server accepted POST request at {url}");
			}
			catch (HttpException httpException)
			{
				_loggingService.Error($"Server returned {httpException.HttpCode} code for POST {item} request from {url}", httpException);
			}
			catch (Exception exception)
			{
				_loggingService.Error($"An exception occurred before POST {item} request to {url}", exception);
			}
		}
	}
}
