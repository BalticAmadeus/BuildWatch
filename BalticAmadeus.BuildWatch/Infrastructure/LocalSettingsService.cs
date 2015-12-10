using BalticAmadeus.BuildWatch.Infrastructure.Settings;

namespace BuildWatch.Infrastructure
{
	public class LocalSettingsService : ILocalSettingsService
	{
		private readonly Properties.Settings _settings = Properties.Settings.Default;

		public string ApiUrlBase
		{
			get { return _settings.ApiUrlBase; }
			set { _settings.ApiUrlBase = value; }
		}
	}
}