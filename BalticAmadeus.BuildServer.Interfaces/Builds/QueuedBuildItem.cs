using System;

namespace BalticAmadeus.BuildServer.Interfaces.Builds
{
	public class QueuedBuildItem
	{
		public string BuildId { get; set; }
		public string BuildRunId { get; set; }
		public string Title { get; set; }
		public DateTime TimeStamp { get; set; }
		public string Username { get; set; }

		public QueuedBuildItem(string buildId, string buildRunId, string title, DateTime timeStamp, string username)
		{
			BuildId = buildId;
			BuildRunId = buildRunId;
			Title = title;
			TimeStamp = timeStamp;
			Username = username;
		}

		public QueuedBuildItem()
		{
			//JsonFormatter
		}
	}
}