namespace BalticAmadeus.BuildPusher.DataSource.TeamCity.DataClasses
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class buildRevisions
	{

		private buildRevisionsRevision revisionField;

		private byte countField;

		/// <remarks/>
		public buildRevisionsRevision revision
		{
			get
			{
				return this.revisionField;
			}
			set
			{
				this.revisionField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public byte count
		{
			get
			{
				return this.countField;
			}
			set
			{
				this.countField = value;
			}
		}
	}
}