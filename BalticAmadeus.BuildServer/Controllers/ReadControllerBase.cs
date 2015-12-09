using System.Web.Http;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers
{
	public abstract class ReadControllerBase : ApiController
	{
		protected IStatelessSession Session { get; private set; }
		
		protected ReadControllerBase(IStatelessSession session)
		{
			Session = session;
		}
	}
}