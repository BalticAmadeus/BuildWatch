using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BalticAmadeus.BuildServer.Domain.Model.Settings;
using BalticAmadeus.BuildServer.Interfaces;
using BalticAmadeus.BuildServer.Interfaces.Settings;
using NHibernate;
using NHibernate.Linq;

namespace BalticAmadeus.BuildServer.Controllers.Settings
{
	public class SettingsReadController : ReadControllerBase
	{
		public SettingsReadController(IStatelessSession session) : base(session)
		{
		}
		
		[Route("api/settings/")]
		[HttpGet]
		[ResponseType(typeof(SettingItem[]))]
		public async Task<IHttpActionResult> ListConfigurations()
		{
			await Task.Delay(1);

			var configurations = Session.Query<Setting>()
				.Select(x => new SettingItem(x.Id.SettingKey, x.Id.Category, x.Type, x.Value))
				.ToArray();

			return Ok(configurations);
		}
	}
}