namespace BalticAmadeus.BuildServer.Interfaces
{
	public class SharedConstants
	{
		public class DataSource
		{
			public const string RefreshTimeoutInMilisecondsKey = "DataSourceManager.RefreshTimeoutInMiliseconds";
			public const string TeamCityBaseUrlKey = "DataSource.TeamCity.BaseUrl";
			public const string TeamCityUsernameKey = "DataSource.TeamCity.Username";
			public const string TeamCityPasswordKey = "DataSource.TeamCity.Password";
		}

		public class Logging
		{
			public const string Console = "Logging.Console";
			public const string File = "Logging.File";
			public const string Remote = "Logging.Remote";
		}
	}
}
