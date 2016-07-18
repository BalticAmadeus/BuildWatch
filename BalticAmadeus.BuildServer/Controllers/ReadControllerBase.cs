using System.Web.Http;
using BalticAmadeus.BuildServer.Infrastructure;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers
{
	[BuildWatchExceptionFilter]
	public abstract class ReadControllerBase : ApiController
	{
		protected IStatelessSession Session { get; private set; }
		
		protected ReadControllerBase(IStatelessSession session)
		{
			Session = session;
		}
	}
}