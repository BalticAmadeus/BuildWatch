using System;

namespace BalticAmadeus.BuildServer.Interfaces.Builds
{
	public class DeleteBuildCommand
	{
		public Guid BuildId { get; set; }

		public DeleteBuildCommand(Guid buildId)
		{
			BuildId = buildId;
		}

		public DeleteBuildCommand()
		{
			
		}
	}
}