using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BalticAmadeus.BuildServer.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace BalticAmadeus.BuildServer.Controllers
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
		[ResponseType(typeof(FinishedBuildItem[]))]
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
						lastBuild.FinishedTimeStamp.ToString(), lastBuild.Status.ToString(), lastBuild.Username));

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
					lastBuild.FinishedTimeStamp.ToString(), lastBuild.Status.ToString(), firstFailedBuild.Username));
			}

			return Ok(buildItems);
		}

		[Route("api/finishedBuilds/{buildId}")]
		[HttpGet]
		[ResponseType(typeof(FinishedBuildItem))]
		public async Task<IHttpActionResult> GetBuild(int buildId)
		{
			await Task.Delay(1);

			return Ok(_session.Load<Build>(buildId));
		}
	}

	public class FinishedBuildItem
	{
		public int BuildId { get; set; }
		public int BuildRunId { get; set; }
		public string Title { get; set; }
		public string TimeStamp { get; set; }
		public string Status { get; set; }
		public string Username { get; set; }

		public FinishedBuildItem(int buildId, int buildRunId, string title, string timeStamp, string status, string username)
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
