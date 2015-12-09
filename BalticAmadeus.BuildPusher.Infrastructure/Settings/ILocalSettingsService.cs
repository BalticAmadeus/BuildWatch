namespace BalticAmadeus.BuildPusher.Infrastructure.Settings
{
	public interface ILocalSettingsService
	{
		string ApiUrlBase { get; set; }
		string AppKey { get; set; }
		int SettingsCacheTimeoutInMiliseconds { get; set; }
	}
}
