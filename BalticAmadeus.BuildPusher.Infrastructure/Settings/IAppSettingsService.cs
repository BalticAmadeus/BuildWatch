namespace BalticAmadeus.BuildPusher.Infrastructure.Settings
{
	public interface IAppSettingsService
	{
		void ReloadSettings();

		string GetString(string key);
		int GetInt(string key);
	}
}