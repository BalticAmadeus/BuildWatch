using System;
using BalticAmadeus.BuildPusher.Infrastructure.Settings;
using BalticAmadeus.BuildServer.Interfaces;
using NLog;

namespace BalticAmadeus.BuildPusher.Infrastructure
{
	public class LoggingService : ILoggingService
	{
		private readonly ILogger _fileLogger;
		private readonly ILogger _consoleLogger;

		private readonly ILocalSettingsService _localSettingsService;
		private readonly IAppSettingsService _appSettingsService;

		public LoggingService(ILocalSettingsService localSettingsService, IAppSettingsService appSettingsService)
		{
			_fileLogger = LogManager.GetLogger("file");
			_consoleLogger = LogManager.GetLogger("console");

			_localSettingsService = localSettingsService;
			_appSettingsService = appSettingsService;
		}

		public void Debug(string message)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 4)
				_fileLogger.Debug("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Debug("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}

		public void Debug(string message, Exception exception)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 4)
				_fileLogger.Debug(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Debug(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}

		public void Info(string message)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 3)
				_fileLogger.Info("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Info("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}

		public void Info(string message, Exception exception)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 3)
				_fileLogger.Info(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Info(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}

		}

		public void Warn(string message)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 2)
				_fileLogger.Warn("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Warn("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}

		public void Warn(string message, Exception exception)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 2)
				_fileLogger.Warn(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Warn(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}

		public void Error(string message)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 1)
				_fileLogger.Error("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Error("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}

		public void Error(string message, Exception exception)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 1)
				_fileLogger.Error(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Error(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}

		public void Fatal(string message)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 0)
				_fileLogger.Fatal("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Fatal("{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}

		public void Fatal(string message, Exception exception)
		{
			if (_appSettingsService.GetInt(SharedConstants.Logging.File) > 0)
				_fileLogger.Fatal(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Console) > 4)
				_consoleLogger.Fatal(exception, "{0} {1}", _localSettingsService.AppKey, message);

			if (_appSettingsService.GetInt(SharedConstants.Logging.Remote) > 4)
			{
				//TODO: Implement remote logging
			}
		}
	}
}
