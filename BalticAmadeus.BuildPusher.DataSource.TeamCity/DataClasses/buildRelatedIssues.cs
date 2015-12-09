namespace BalticAmadeus.BuildPusher.DataSource.TeamCity.DataClasses
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class buildRelatedIssues
	{

		private string hrefField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string href
		{
			get
			{
				return this.hrefField;
			}
			set
			{
				this.hrefField = value;
			}
		}
	}
}