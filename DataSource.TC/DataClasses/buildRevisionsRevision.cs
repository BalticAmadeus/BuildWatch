namespace DataSource.TC.DataClasses
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class buildRevisionsRevision
	{

		private buildRevisionsRevisionVcsrootinstance vcsrootinstanceField;

		private string versionField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("vcs-root-instance")]
		public buildRevisionsRevisionVcsrootinstance vcsrootinstance
		{
			get
			{
				return this.vcsrootinstanceField;
			}
			set
			{
				this.vcsrootinstanceField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}
	}
}