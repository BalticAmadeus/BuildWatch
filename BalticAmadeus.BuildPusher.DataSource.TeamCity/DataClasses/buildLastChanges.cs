using System.Xml.Serialization;

namespace BalticAmadeus.BuildPusher.DataSource.TeamCity.DataClasses
{
	/// <remarks/>
	[XmlType(AnonymousType = true)]
	public partial class buildLastChanges
	{

		private buildLastChangesChange[] _changesField;

		private byte countField;

		/// <remarks/>
		[XmlElement("change")]
		public buildLastChangesChange[] changes
		{
			get
			{
				return this._changesField;
			}
			set
			{
				this._changesField = value;
			}
		}

		/// <remarks/>
		[XmlAttribute()]
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