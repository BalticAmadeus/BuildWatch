using System;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class BuildListModel
	{
		public string Name { get; set; }
		public string Instance { get; set; }
		public BuildStatus Status { get; set; }
		public DateTime TimeStamp { get; set; }
		public string User { get; set; }
		public byte[] PictureData { get; set; }
	}

	public enum BuildStatus
	{
		Success = 0,
		Fail = 1
	}
}
