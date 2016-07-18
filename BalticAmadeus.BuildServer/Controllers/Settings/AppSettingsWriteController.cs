using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;
using BalticAmadeus.BuildServer.Domain.Model.Settings;
using BalticAmadeus.BuildServer.Infrastructure;
using BalticAmadeus.BuildServer.Interfaces;
using BalticAmadeus.BuildServer.Interfaces.Settings;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers.Settings
{
	public class AppSettingsWriteController : WriteControllerBase
	{
		public AppSettingsWriteController(ISession session) : base(session)
		{
		}

		[Route("api/appSettings/")]
		[HttpPost]
		[BuildWatchExceptionFilter]
		public async Task<IHttpActionResult> RegisterApp([FromBody] RegisterAppSettingsData data)
		{
			await Task.Delay(1);

			try
			{
				using (var tx = Session.BeginTransaction())
				{
					var defaultSettings = new[]
					{
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSource.TeamCityBaseUrlKey, data.AppKey), "string", ""),
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSource.TeamCityUsernameKey, data.AppKey), "string", ""),
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSource.TeamCityPasswordKey, data.AppKey), "string", ""),

						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSource.TfsBaseUrlKey, data.AppKey), "string", ""),
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSource.TfsUsernameKey, data.AppKey), "string", ""),
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSource.TfsPasswordKey, data.AppKey), "string", ""),

						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSource.RefreshTimeoutInMilisecondsKey, data.AppKey), "int", "30000"),
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSource.UsernameMask, data.AppKey), "string", "")
					};

					foreach (var appSetting in defaultSettings)
						Session.Save(appSetting);

					tx.Commit();
				}
			}
			catch (Exception e)
			{
				Debugger.Break();
				//TODO: Handle this
			}

			return Ok();
		}
	}
}