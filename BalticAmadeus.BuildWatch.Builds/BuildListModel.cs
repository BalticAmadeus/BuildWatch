using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class BuildListModel
	{
		public string Name { get; set; }
		public string Instance { get; set; }
		public string Result { get; set; }
		public DateTime TimeStamp { get; set; }
		public string User { get; set; }
		public byte[] PictureData { get; set; }
	}
}
