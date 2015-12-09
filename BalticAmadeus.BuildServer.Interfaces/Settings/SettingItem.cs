namespace BalticAmadeus.BuildServer.Interfaces.Settings
{
	public class SettingItem
	{
		public string Key { get; set; }
		public string Category { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }

		public SettingItem(string key, string category, string type, string value)
		{
			Key = key;
			Category = category;
			Type = type;
			Value = value;
		}

		public SettingItem()
		{
			
		}
	}
}