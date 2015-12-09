namespace BalticAmadeus.BuildPusher.Infrastructure.Settings
{
	public class LocalSettingsService : ILocalSettingsService
	{
		private readonly Properties.Settings _settings = Properties.Settings.Default;

		public string ApiUrlBase
		{
			get { return _settings.ApiUrlBase; }
			set { _settings.ApiUrlBase = value; }
		}

		public string AppKey
		{
			get { return _settings.AppKey; }
			set
			{
				_settings.AppKey = value;
				_settings.Save();
			}
		}

		public int SettingsCacheTimeoutInMiliseconds
		{
			get { return _settings.SettingsCacheTimeoutInMiliseconds; }
			set
			{
				_settings.SettingsCacheTimeoutInMiliseconds = value;
				_settings.Save();
			}
		}
	}
}