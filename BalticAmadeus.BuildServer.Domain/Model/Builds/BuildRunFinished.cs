using System;

namespace BalticAmadeus.BuildServer.Domain.Model.Builds
{
	public class BuildRunFinished : IDomainEvent
	{
		public string BuildId { get; set; }
		public string Id { get; set; }
		public DateTime FinishedTimeStamp { get; set; }
		public int Status { get; set; }

		public BuildRunFinished(string buildId, string id, DateTime finishedTimeStamp, int status)
		{
			BuildId = buildId;
			Id = id;
			FinishedTimeStamp = finishedTimeStamp;
			Status = status;
		}

		public BuildRunFinished()
		{
			//JsonSerializer
		}
	}
}