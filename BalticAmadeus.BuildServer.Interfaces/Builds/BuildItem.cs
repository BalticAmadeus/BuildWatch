namespace BalticAmadeus.BuildServer.Interfaces.Builds
{
	public class BuildItem
	{
		public string BuildId { get; set; }
		public string AliasTitle { get; set; }
		public string UniqueTitle { get; set; }

		public BuildItem(string buildId, string aliasTitle, string uniqueTitle)
		{
			BuildId = buildId;
			AliasTitle = aliasTitle;
			UniqueTitle = uniqueTitle;
		}

		public BuildItem()
		{
			
		}
	}
}
