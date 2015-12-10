using System;

namespace BalticAmadeus.BuildServer.Domain.Model.Builds
{
	public class QueuedBuildRunAdded : IDomainEvent
	{
		public string BuildId { get; set; }
		public string Id { get; set; }
		public string Instance { get; set; }
		public DateTime TimeStamp { get; set; }
		public string Username { get; set; }

		public QueuedBuildRunAdded(string buildId, string id, string instance, DateTime timeStamp, string username)
		{
			BuildId = buildId;
			Id = id;
			Instance = instance;
			TimeStamp = timeStamp;
			Username = username;
		}

		public QueuedBuildRunAdded()
		{
			//JsonSerializer
		}
	}
}
