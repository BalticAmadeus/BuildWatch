using System;

namespace BalticAmadeus.BuildServer.Interfaces.Builds
{
	public class RenameBuildCommand
	{
		public Guid BuildId { get; set; }
		public string Title { get; set; }

		public RenameBuildCommand(Guid buildId, string title)
		{
			BuildId = buildId;
			Title = title;
		}

		public RenameBuildCommand()
		{
			
		}
	}
}