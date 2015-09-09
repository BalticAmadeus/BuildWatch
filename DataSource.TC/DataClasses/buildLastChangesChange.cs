namespace DataSource.TC.DataClasses
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class buildLastChangesChange
	{

		private ushort idField;

		private string versionField;

		private string usernameField;

		private string dateField;

		private string hrefField;

		private string webUrlField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ushort id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
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

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string username
		{
			get
			{
				return this.usernameField;
			}
			set
			{
				this.usernameField = value;
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

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string webUrl
		{
			get
			{
				return this.webUrlField;
			}
			set
			{
				this.webUrlField = value;
			}
		}
	}
}