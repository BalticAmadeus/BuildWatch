using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BalticAmadeus.BuildServer.Domain.Model.Builds;
using BalticAmadeus.BuildServer.Interfaces;
using BalticAmadeus.BuildServer.Interfaces.Builds;
using NHibernate;
using NHibernate.Linq;

namespace BalticAmadeus.BuildServer.Controllers.Builds
{
    public class QueuedBuildsController : ApiController
    {
	    private readonly IStatelessSession _session;

	    public QueuedBuildsController(IStatelessSession session)
	    {
		    _session = session;
	    }

		[Route("api/queuedBuilds/")]
		[HttpGet]
		[ResponseType(typeof(QueuedBuildItem[]))]
	    public async Task<IHttpActionResult> ListBuilds()
	    {
			await Task.Delay(1);

			var builds = _session.Query<Build>()
				.ToArray();

			var buildItems = new List<QueuedBuildItem>();

			foreach (var build in builds)
			{
				var inProgressBuildRuns = build.BuildRuns
					.Where(x => x.Status == BuildRunStatus.InProgress)
					.ToArray();

				foreach (var buildRun in inProgressBuildRuns)
				{
					buildItems.Add(new QueuedBuildItem(
						buildRun.BuildId,
						buildRun.Id,
						build.AliasTitle,
						buildRun.StartedTimeStamp,
						buildRun.Username));
				}
			}

			return Ok(buildItems);
		}
	}
}
