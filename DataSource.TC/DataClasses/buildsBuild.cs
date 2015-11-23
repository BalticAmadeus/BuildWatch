using System.Xml.Serialization;

namespace DataSource.TC.DataClasses
{
	/// <remarks />
	[XmlType(AnonymousType = true)]
	public class buildsBuild
	{
		/// <remarks />
		[XmlAttribute]
		public ushort id { get; set; }

		/// <remarks />
		[XmlAttribute]
		public string buildTypeId { get; set; }

		/// <remarks />
		[XmlAttribute]
		public string number { get; set; }

		/// <remarks />
		[XmlAttribute]
		public string status { get; set; }

		/// <remarks />
		[XmlAttribute]
		public string state { get; set; }

		/// <remarks />
		[XmlAttribute]
		public string href { get; set; }

		/// <remarks />
		[XmlAttribute]
		public string webUrl { get; set; }
	}
}