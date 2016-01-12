namespace BalticAmadeus.BuildServer.Interfaces
{
	public class SharedConstants
	{
		public class DataSource
		{
			public const string RefreshTimeoutInMilisecondsKey = "DataSourceManager.RefreshTimeoutInMiliseconds";
			public const string UsernameMask = "DataSourceManager.UsernameMask";

			public const string TeamCityBaseUrlKey = "DataSource.TeamCity.BaseUrl";
			public const string TeamCityUsernameKey = "DataSource.TeamCity.Username";
			public const string TeamCityPasswordKey = "DataSource.TeamCity.Password";

			public static string TfsBaseUrlKey = "DataSource.Tfs.BaseUrl";
			public static string TfsUsernameKey = "DataSource.Tfs.Username";
			public static string TfsPasswordKey = "DataSource.Tfs.Password";
		}
	}
}
