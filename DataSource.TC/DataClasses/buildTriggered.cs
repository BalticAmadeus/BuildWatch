namespace DataSource.TC.DataClasses
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class buildTriggered
	{

		private buildTriggeredUser userField;

		private string typeField;

		private string dateField;

		/// <remarks/>
		public buildTriggeredUser user
		{
			get
			{
				return this.userField;
			}
			set
			{
				this.userField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string date
		{
			get
			{
				return this.dateField;
			}
			set
			{
				this.dateField = value;
			}
		}
	}
}