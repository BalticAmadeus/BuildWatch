using System.Web.Http;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers
{
	public abstract class WriteControllerBase : ApiController
	{
		protected ISession Session { get; private set; }

		protected WriteControllerBase(ISession session)
		{
			Session = session;
		}
	}
}