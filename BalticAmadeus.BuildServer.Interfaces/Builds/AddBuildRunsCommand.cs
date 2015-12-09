using System;

namespace BalticAmadeus.BuildServer.Interfaces.Builds
{
	public class AddBuildRunCommand
	{
		public string BuildId { get; set; }
		public string BuildRunId { get; set; }
		public string Title { get; set; }
		public int Status { get; set; }
		public DateTime TimeStamp { get; set; }
		public DateTime? FinishTimeStamp { get; set; }
		public string Username { get; set; }

		public AddBuildRunCommand(string buildId, string buildRunId, string title, int status, DateTime timeStamp, DateTime? finishTimeStamp, string username)
		{
			BuildId = buildId;
			BuildRunId = buildRunId;
			Title = title;
			Status = status;
			TimeStamp = timeStamp;
			FinishTimeStamp = finishTimeStamp;
			Username = username;
		}

		public AddBuildRunCommand()
		{
			//JsonSerializer
		}
	}

	public class BuildRunItem
	{
		public string BuildId { get; protected set; }
		public string Status { get; protected set; }
		public DateTime TimeStamp { get; protected set; }
		public string Title { get; protected set; }
		public string Username { get; protected set; }

		public BuildRunItem(string buildId, string status, DateTime timeStamp, string title, string username)
		{
			BuildId = buildId;
			Status = status;
			TimeStamp = timeStamp;
			Title = title;
			Username = username;
		}

		public BuildRunItem()
		{
			//JsonSerializer
		}
	}
}