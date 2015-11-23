using System.Collections.Generic;
using System.Web.Http;
using BalticAmadeus.BuildServer.Interfaces;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers
{
	public class BuildsController : ApiController
	{
		private readonly ISession _session;

		public BuildsController(ISession session)
		{
			_session = session;
		}

		[Route("api/builds/")]
		[HttpGet]
		public IEnumerable<Build> Get()
		{
			return _session.QueryOver<Build>().List();
		}

		[Route("api/builds/{buildDefinitionId}/{buildId}")]
		[HttpGet]
		public Build Get(int buildDefinitionId, int buildId)
		{
			return _session.Load<Build>(new BuildCompositeId(buildDefinitionId, buildId));
		}

		[Route("api/builds/")]
		[HttpPost]
		public void Post([FromBody] Build build)
		{
			using (var tx = _session.BeginTransaction())
			{
				_session.Save(build);
				tx.Commit();
			}
		}
	}
}
