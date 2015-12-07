using System;

namespace BalticAmadeus.BuildServer.Interfaces
{
	public class BuildRun
	{
		public BuildRun(int buildId, string instance, DateTime timeStamp, string username)
		{
			BuildId = buildId;
			Instance = instance;
			StartedTimeStamp = timeStamp;
			Username = username;

			Status = BuildRunStatus.InProgress;
			FinishedTimeStamp = null;
			IsDeleted = false;
		}

		protected BuildRun()
		{
			//NH
		}

		public virtual int Id { get; protected set; }
		public virtual int BuildId { get; protected set; }
		public virtual string Instance { get; protected set; }
		public virtual BuildRunStatus Status { get; protected set; }
		public virtual DateTime StartedTimeStamp { get; protected set; }
		public virtual DateTime? FinishedTimeStamp { get; protected set; }
		public virtual string Username { get; protected set; }

		public virtual bool IsDeleted { get; protected set; }

		public virtual void Delete()
		{
			IsDeleted = true;
		}

		public virtual void Finish(DateTime timeStamp, BuildRunStatus status)
		{
			if (status == BuildRunStatus.InProgress)
				throw new ArgumentException("Cannot be InProgress", nameof(status));

			FinishedTimeStamp = timeStamp;
			Status = status;
		}

		public override bool Equals(object obj)
		{
			var other = obj as BuildRun;

			if (other == null)
				return false;

			return Id == other.Id
				   && BuildId == other.BuildId;
		}

		public override int GetHashCode()
		{
			int code = GetType().GetHashCode();
			code += 439 ^ Id.GetHashCode();
			code += 433 ^ BuildId.GetHashCode();

			return code;
		}
	}
}