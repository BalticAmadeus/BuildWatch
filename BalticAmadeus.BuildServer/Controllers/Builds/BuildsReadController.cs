using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BalticAmadeus.BuildServer.Domain.Model.Builds;
using BalticAmadeus.BuildServer.Infrastructure;
using BalticAmadeus.BuildServer.Interfaces.Builds;
using NHibernate;
using NHibernate.Linq;

namespace BalticAmadeus.BuildServer.Controllers.Builds
{
	public class BuildsReadController : ApiController
	{
		private readonly ISession _session;

		public BuildsReadController(ISession session)
		{
			_session = session;
		}
		
		[Route("api/builds/")]
		[HttpGet]
		[BuildWatchExceptionFilter]
		[ResponseType(typeof(BuildItem[]))]
		public async Task<IHttpActionResult> ListBuilds()
		{
			await Task.Delay(1);

			var builds = _session.Query<Build>()
				.Select(x => new BuildItem(x.ExternalId, x.AliasTitle, x.ExternalId))
				.ToArray();

			return Ok(builds);
		}
	}
}
