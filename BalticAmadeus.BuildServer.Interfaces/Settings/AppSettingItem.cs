namespace BalticAmadeus.BuildServer.Interfaces.Settings
{
	public class AppSettingItem
	{
		public string Key { get; set; }
		public string AppKey { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }

		public AppSettingItem(string key, string appKey, string type, string value)
		{
			Key = key;
			AppKey = appKey;
			Type = type;
			Value = value;
		}

		public AppSettingItem()
		{

		}
	}
}
