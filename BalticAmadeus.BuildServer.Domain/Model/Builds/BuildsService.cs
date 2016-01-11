using System;
using System.Linq;
using NHibernate;

namespace BalticAmadeus.BuildServer.Domain.Model.Builds
{
	public class BuildsService
	{
		public void AddBuildRun(ISession session, string buildId, string buildRunId, string title, int status, DateTime timeStamp, DateTime? finishTimeStamp, string username)
		{
			var build = session.Get<Build>(buildId);
			if (build == null)
			{
				build = new Build(buildId);
				build.Rename(title);
				session.Save(build);

				var buildCreatedEvent = new BuildCreated(buildId);
				DomainEvents.Raise(buildCreatedEvent, session);
			}

			var buildRun = build.BuildRuns.SingleOrDefault(x => x.Id == buildRunId);
			if (buildRun == null)
			{
				buildRun = new BuildRun(buildId, buildRunId, title, timeStamp, username);
				build.AddBuild(buildRun);

				var buildRunAddedEvent = new QueuedBuildRunAdded(buildId, buildRunId, title, timeStamp, username);
				DomainEvents.Raise(buildRunAddedEvent, session);
			}

			if (buildRun.Status != BuildRunStatus.InProgress)
				return;

			if (status == BuildRunStatus.InProgress)
				return;

			if (finishTimeStamp == null)
				throw new ArgumentNullException(nameof(finishTimeStamp));

			buildRun.Finish(finishTimeStamp.Value, status);

			var buildRunFinishedEvent = new BuildRunFinished(buildId, buildRunId, finishTimeStamp.Value, status);
			DomainEvents.Raise(buildRunFinishedEvent, session);
		}
	}
}
