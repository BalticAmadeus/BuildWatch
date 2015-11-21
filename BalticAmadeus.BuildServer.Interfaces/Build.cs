using System;

namespace BalticAmadeus.BuildServer.Interfaces
{
	public class Build
	{
		public virtual BuildCompositeId Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Instance { get; set; }
		public virtual BuildStatus Status { get; set; }
		public virtual DateTime TimeStamp { get; set; }
		public virtual string Username { get; set; }
		public virtual bool IsDeleted { get; set; }
	}
}