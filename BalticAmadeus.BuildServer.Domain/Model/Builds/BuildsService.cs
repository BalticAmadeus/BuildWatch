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
				session.Save(build);
			}

			var buildRun = build.BuildRuns.SingleOrDefault(x => x.Id == buildRunId);
			if (buildRun == null)
			{
				buildRun = new BuildRun(buildId, buildRunId, title, timeStamp, username);
				build.AddBuild(buildRun);
			}
			
			if (status == BuildRunStatus.InProgress)
				return;

			if (finishTimeStamp == null)
				throw new ArgumentNullException(nameof(finishTimeStamp));

			buildRun.Finish(finishTimeStamp.Value, status);
		}
	}
}
