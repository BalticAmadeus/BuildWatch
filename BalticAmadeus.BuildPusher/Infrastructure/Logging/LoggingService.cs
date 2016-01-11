using System;
using BalticAmadeus.BuildPusher.Infrastructure.Settings;
using NLog;

namespace BalticAmadeus.BuildPusher.Infrastructure.Logging
{
	public class LoggingService : ILoggingService
	{
		private readonly ILogger _fileLogger;
		private readonly ILogger _consoleLogger;

		private readonly ILocalSettingsService _localSettingsService;

		public LoggingService(ILocalSettingsService localSettingsService)
		{
			_fileLogger = LogManager.GetLogger("file");
			_consoleLogger = LogManager.GetLogger("console");

			_localSettingsService = localSettingsService;
		}

		public void Debug(string message)
		{
			_fileLogger.Debug("{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Debug("{0} {1}", _localSettingsService.AppKey, message);
		}

		public void Debug(string message, Exception exception)
		{
			_fileLogger.Debug(exception, "{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Debug(exception, "{0} {1}", _localSettingsService.AppKey, message);
		}

		public void Info(string message)
		{
			_fileLogger.Info("{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Info("{0} {1}", _localSettingsService.AppKey, message);
		}

		public void Info(string message, Exception exception)
		{
			_fileLogger.Info(exception, "{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Info(exception, "{0} {1}", _localSettingsService.AppKey, message);
		}
		
		public void Warn(string message)
		{
			_fileLogger.Warn("{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Warn("{0} {1}", _localSettingsService.AppKey, message);
		}

		public void Warn(string message, Exception exception)
		{
			_fileLogger.Warn(exception, "{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Warn(exception, "{0} {1}", _localSettingsService.AppKey, message);
		}

		public void Error(string message)
		{
			_fileLogger.Error("{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Error("{0} {1}", _localSettingsService.AppKey, message);

			//TODO: Report to remove server
		}

		public void Error(string message, Exception exception)
		{
			_fileLogger.Error(exception, "{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Error(exception, "{0} {1}", _localSettingsService.AppKey, message);

			//TODO: Report to remove server
		}

		public void Fatal(string message)
		{
			_fileLogger.Fatal("{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Fatal("{0} {1}", _localSettingsService.AppKey, message);

			//TODO: Report to remove server
		}

		public void Fatal(string message, Exception exception)
		{
			_fileLogger.Fatal(exception, "{0} {1}", _localSettingsService.AppKey, message);
			_consoleLogger.Fatal(exception, "{0} {1}", _localSettingsService.AppKey, message);

			//TODO: Report to remove server
		}
	}
}
