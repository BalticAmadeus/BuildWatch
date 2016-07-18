using System.Web.Http;
using BalticAmadeus.BuildServer.Infrastructure;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers
{
	[BuildWatchExceptionFilter]
	public abstract class WriteControllerBase : ApiController
	{
		protected ISession Session { get; private set; }

		protected WriteControllerBase(ISession session)
		{
			Session = session;
		}
	}
}