using System;

namespace BalticAmadeus.BuildPusher.Infrastructure.Http
{
	public class HttpException : Exception
	{
		public string HttpCode { get; private set; }
	
		public HttpException(string exceptionMessage, string httpCode) : base(exceptionMessage)
		{
			HttpCode = httpCode;
		}

		public HttpException(string exceptionMessage, string httpCode, Exception innerException) : base(exceptionMessage, innerException)
		{
			HttpCode = httpCode;
		}
	}
}