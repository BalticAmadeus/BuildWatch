using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using BalticAmadeus.BuildServer.Domain.Model.Builds;
using BalticAmadeus.BuildServer.Interfaces.Builds;
using NHibernate;
using NHibernate.Linq;

namespace BalticAmadeus.BuildServer.Controllers.Builds
{
	public class FinishedBuildsController : ApiController
	{
		private readonly ISession _session;

		public FinishedBuildsController(ISession session)
		{
			_session = session;
		}

		[Route("api/finishedBuilds/")]
		[HttpGet]
		[ResponseType(typeof (FinishedBuildItem[]))]
		public async Task<IHttpActionResult> ListBuilds()
		{
			await Task.Delay(1);

			var builds = _session.Query<Build>();

			var buildItems = new List<FinishedBuildItem>();

			foreach (var build in builds)
			{
				var lastBuild = build.BuildRuns.FirstOrDefault(x => x.FinishedTimeStamp != null);
				if (lastBuild == null)
					continue;

				if (lastBuild.Status == BuildRunStatus.Success)
					buildItems.Add(new FinishedBuildItem(lastBuild.BuildId, lastBuild.BuildId, build.AliasTitle,
						lastBuild.FinishedTimeStamp.Value, lastBuild.Status.ToString(), lastBuild.Username));

				BuildRun firstFailedBuild = null;
				foreach (var buildRun in build.BuildRuns)
				{
					if (buildRun.Status == BuildRunStatus.Success)
						break;

					firstFailedBuild = buildRun;
				}

				if (firstFailedBuild == null)
					continue;

				buildItems.Add(new FinishedBuildItem(lastBuild.BuildId, lastBuild.BuildId, build.AliasTitle,
					lastBuild.FinishedTimeStamp.Value, lastBuild.Status.ToString(), firstFailedBuild.Username));
			}

			return Ok(buildItems);
		}
	}
}