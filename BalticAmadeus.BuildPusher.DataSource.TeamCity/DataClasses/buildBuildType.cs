namespace BalticAmadeus.BuildPusher.DataSource.TeamCity.DataClasses
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class buildBuildType
	{

		private string idField;

		private string nameField;

		private string descriptionField;

		private string projectNameField;

		private string projectIdField;

		private string hrefField;

		private string webUrlField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string id
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
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string projectName
		{
			get
			{
				return this.projectNameField;
			}
			set
			{
				this.projectNameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string projectId
		{
			get
			{
				return this.projectIdField;
			}
			set
			{
				this.projectIdField = value;
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