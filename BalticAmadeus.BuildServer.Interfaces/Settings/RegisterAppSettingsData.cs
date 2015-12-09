namespace BalticAmadeus.BuildServer.Interfaces.Settings
{
	public class RegisterAppSettingsData : ICommandData
	{
		public string AppKey { get; set; }

		public RegisterAppSettingsData(string appKey)
		{
			AppKey = appKey;
		}
	}

	public class ICommandData
	{
	}
}
