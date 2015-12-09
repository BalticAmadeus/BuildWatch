namespace BalticAmadeus.BuildPusher.Infrastructure.Settings
{
	public interface IAppSettingsService
	{
		string GetString(string key);
		int GetInt(string key);
	}
}