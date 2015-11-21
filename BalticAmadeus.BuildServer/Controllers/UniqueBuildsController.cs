using System.Collections.Generic;
using System.Web.Http;
using BalticAmadeus.BuildServer.Interfaces;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers
{
    public class UniqueBuildsController : ApiController
    {
		private readonly ISession _session;

		public UniqueBuildsController(ISession session)
		{
			_session = session;
		}

		[Route("api/uniqueBuilds/")]
		[HttpGet]
		public IEnumerable<Build> Get()
		{
			return _session.QueryOver<Build>()
				.Where(x => x.Status != BuildStatus.InProgress)
				
				.List();
		}
	}
}
