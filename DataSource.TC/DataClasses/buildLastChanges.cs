using System.Xml.Serialization;

namespace DataSource.TC.DataClasses
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
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