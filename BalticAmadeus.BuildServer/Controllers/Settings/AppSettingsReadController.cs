using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BalticAmadeus.BuildServer.Domain.Model.Settings;
using BalticAmadeus.BuildServer.Interfaces.Settings;
using NHibernate;
using NHibernate.Linq;

namespace BalticAmadeus.BuildServer.Controllers.Settings
{
	public class AppSettingsReadController : ReadControllerBase
	{
		public AppSettingsReadController(IStatelessSession session) : base(session)
		{
		}

		[Route("api/appSettings/{appKey}")]
		[HttpGet]
		[ResponseType(typeof(AppSettingItem[]))]
		public async Task<IHttpActionResult> ListAppSettings(string appKey)
		{
			await Task.Delay(1);

			var configurations = Session.Query<AppSetting>()
				.Where(x => x.Id.AppKey.Equals(appKey))
				.Select(x => new AppSettingItem(x.Id.SettingKey, x.Id.AppKey, x.Type, x.Value))
				.ToArray();

			return Ok(configurations);
		}

		[Route("api/appSettings/{appKey}/{settingKey}")]
		[HttpGet]
		[ResponseType(typeof(AppSettingItem))]
		public async Task<IHttpActionResult> GetSetting(string appKey, string settingKey)
		{
			await Task.Delay(1);

			var appSettingCompositeId = new AppSettingCompositeId(settingKey, appKey);
			var appSetting = Session.Get<AppSetting>(appSettingCompositeId);

			if (appSetting == null)
				return NotFound();

			var appSettingItem = new AppSettingItem(
				appSetting.Id.SettingKey, appSetting.Id.AppKey, appSetting.Type, appSetting.Value);

			return Ok(appSettingItem);
		}
	}
}