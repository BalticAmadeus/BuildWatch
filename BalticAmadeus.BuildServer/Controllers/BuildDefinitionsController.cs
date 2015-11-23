using System.Collections.Generic;
using System.Web.Http;
using BalticAmadeus.BuildServer.Interfaces;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers
{
	public class BuildDefinitionsController : ApiController
    {
	    private readonly ISession _session;

	    public BuildDefinitionsController(ISession session)
	    {
		    _session = session;
	    }

		[Route("api/buildDefinitions/")]
		[HttpGet]
		public IEnumerable<BuildDefinition> Get()
        {
	        return _session.QueryOver<BuildDefinition>().List();
        }

		[Route("api/buildDefinitions/{buildDefinitionId}")]
		[HttpGet]
		public BuildDefinition Get(int buildDefinitionId)
        {
	        return _session.Load<BuildDefinition>(buildDefinitionId);
        }

		[Route("api/buildDefinitions/")]
		[HttpPost]
		public void Post([FromBody]BuildDefinition buildDefinition)
        {
	        using (var tx = _session.BeginTransaction())
	        {
		        _session.Save(buildDefinition);
				tx.Commit();
	        }
        }

		[Route("api/buildDefinitions/{buildDefinitionId}")]
		[HttpPut]
		public void Put(int buildDefinitionId, [FromBody]BuildDefinition buildDefinition)
		{
			using (var tx = _session.BeginTransaction())
			{
				var entity = _session.Load<BuildDefinition>(buildDefinitionId);
				entity.AliasTitle = buildDefinition.AliasTitle;
				entity.OriginalTitle = buildDefinition.OriginalTitle;
				tx.Commit();
			}
		}

		[Route("api/buildDefinitions/")]
		[HttpDelete]
		public void Delete(int id)
        {
	        using (var tx = _session.BeginTransaction())
	        {
		        var entity = _session.Load<BuildDefinition>(id);
		        _session.Delete(entity);
				tx.Commit();
	        }
        }
    }
}
