using System;

namespace BalticAmadeus.BuildServer.Interfaces
{
	public class Build
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Instance { get; set; }
		public virtual BuildStatus Status { get; set; }
		public virtual DateTime TimeStamp { get; set; }
		public virtual string User { get; set; }
	}
}