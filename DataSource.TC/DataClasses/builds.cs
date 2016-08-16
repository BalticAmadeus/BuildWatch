using System.Xml.Serialization;

namespace DataSource.TC.DataClasses
{
	/// <remarks />
	[XmlType(AnonymousType = true)]
	[XmlRoot(Namespace = "", IsNullable = false)]
	public class builds
	{
		/// <remarks />
		[XmlElement("build")]
		public buildsBuild[] build { get; set; }

		/// <remarks />
		[XmlAttribute]
		public int count { get; set; }

		/// <remarks />
		[XmlAttribute]
		public string href { get; set; }

		/// <remarks />
		[XmlAttribute]
		public string nextHref { get; set; }
	}
}