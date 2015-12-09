namespace BalticAmadeus.BuildPusher.DataSource.TeamCity.DataClasses
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public partial class build
	{
		private string statusTextField;

		private buildBuildType buildTypeField;

		private string queuedDateField;

		private string startDateField;

		private string finishDateField;

		private buildTriggered triggeredField;

		private buildLastChanges lastChangesField;

		private buildChanges changesField;

		private buildRevisions revisionsField;

		private buildAgent agentField;

		private buildArtifacts artifactsField;

		private buildRelatedIssues relatedIssuesField;

		private buildStatistics statisticsField;

		private ushort idField;

		private string buildTypeIdField;

		private string numberField;

		private string statusField;

		private string stateField;

		private string hrefField;

		private string webUrlField;

		/// <remarks/>
		public string statusText
		{
			get
			{
				return this.statusTextField;
			}
			set
			{
				this.statusTextField = value;
			}
		}

		/// <remarks/>
		public buildBuildType buildType
		{
			get
			{
				return this.buildTypeField;
			}
			set
			{
				this.buildTypeField = value;
			}
		}

		/// <remarks/>
		public string queuedDate
		{
			get
			{
				return this.queuedDateField;
			}
			set
			{
				this.queuedDateField = value;
			}
		}

		/// <remarks/>
		public string startDate
		{
			get
			{
				return this.startDateField;
			}
			set
			{
				this.startDateField = value;
			}
		}

		/// <remarks/>
		public string finishDate
		{
			get
			{
				return this.finishDateField;
			}
			set
			{
				this.finishDateField = value;
			}
		}

		/// <remarks/>
		public buildTriggered triggered
		{
			get
			{
				return this.triggeredField;
			}
			set
			{
				this.triggeredField = value;
			}
		}

		/// <remarks/>
		public buildLastChanges lastChanges
		{
			get
			{
				return this.lastChangesField;
			}
			set
			{
				this.lastChangesField = value;
			}
		}

		/// <remarks/>
		public buildChanges changes
		{
			get
			{
				return this.changesField;
			}
			set
			{
				this.changesField = value;
			}
		}

		/// <remarks/>
		public buildRevisions revisions
		{
			get
			{
				return this.revisionsField;
			}
			set
			{
				this.revisionsField = value;
			}
		}

		/// <remarks/>
		public buildAgent agent
		{
			get
			{
				return this.agentField;
			}
			set
			{
				this.agentField = value;
			}
		}

		/// <remarks/>
		public buildArtifacts artifacts
		{
			get
			{
				return this.artifactsField;
			}
			set
			{
				this.artifactsField = value;
			}
		}

		/// <remarks/>
		public buildRelatedIssues relatedIssues
		{
			get
			{
				return this.relatedIssuesField;
			}
			set
			{
				this.relatedIssuesField = value;
			}
		}

		/// <remarks/>
		public buildStatistics statistics
		{
			get
			{
				return this.statisticsField;
			}
			set
			{
				this.statisticsField = value;
			}
		}

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
		public string buildTypeId
		{
			get
			{
				return this.buildTypeIdField;
			}
			set
			{
				this.buildTypeIdField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string number
		{
			get
			{
				return this.numberField;
			}
			set
			{
				this.numberField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string state
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
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