using System;
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

		// GET api/builds
		public IEnumerable<Build> Get()
		{
			return _session.QueryOver<Build>().List();
		}

		// GET api/builds/5
		public Build Get(int id)
		{
			return _session.Load<Build>(id);
		}

		// POST api/builds
		public void Post([FromBody] Build build)
		{
			using (var tx = _session.BeginTransaction())
			{
				_session.Save(build);
				tx.Commit();
			}
		}

		// PUT api/builds/5
		public void Put(int id, [FromBody] Build build)
		{
			using (var tx = _session.BeginTransaction())
			{
				var entity = _session.Load<Build>(id);

				entity.Status = build.Status;
				entity.Instance = build.Instance;
				entity.Name = build.Name;
				entity.TimeStamp = build.TimeStamp;
				entity.User = build.User;

				tx.Commit();
			}
		}

		// DELETE api/builds/5
		public void Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}
