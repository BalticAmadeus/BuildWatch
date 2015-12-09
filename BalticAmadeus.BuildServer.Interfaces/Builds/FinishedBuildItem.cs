using System;

namespace BalticAmadeus.BuildServer.Interfaces.Builds
{
	public class FinishedBuildItem
	{
		public string BuildId { get; set; }
		public string BuildRunId { get; set; }
		public string Title { get; set; }
		public DateTime TimeStamp { get; set; }
		public string Status { get; set; }
		public string Username { get; set; }

		public FinishedBuildItem(string buildId, string buildRunId, string title, DateTime timeStamp, string status, string username)
		{
			BuildId = buildId;
			BuildRunId = buildRunId;
			Title = title;
			TimeStamp = timeStamp;
			Status = status;
			Username = username;
		}

		public FinishedBuildItem()
		{
		}
	}
}