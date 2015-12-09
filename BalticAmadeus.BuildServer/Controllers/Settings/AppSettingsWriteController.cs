using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;
using BalticAmadeus.BuildServer.Domain.Model.Settings;
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
		public async Task<IHttpActionResult> RegisterApp([FromBody] RegisterAppSettingsData data)
		{
			await Task.Delay(1);

			try
			{
				using (var tx = Session.BeginTransaction())
				{
					var defaultSettings = new[]
					{
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSourceTeamCityBaseUrlKey, data.AppKey),
							"string", "http://sisdev-sc2build:8080/httpAuth/app/rest"),
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSourceTeamCityUsernameKey, data.AppKey),
							"string", "buildwatch"),
						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSourceTeamCityPasswordKey, data.AppKey),
							"string", "buildwatch1"),

						new AppSetting(new AppSettingCompositeId("DataSource.Tfs.BaseUrl", data.AppKey), "string", ""),
						new AppSetting(new AppSettingCompositeId("DataSource.Tfs.ProjectCollection", data.AppKey), "string", ""),
						new AppSetting(new AppSettingCompositeId("DataSource.Tfs.ProjectName", data.AppKey), "string", ""),
						new AppSetting(new AppSettingCompositeId("DataSource.Tfs.Username", data.AppKey), "string", ""),
						new AppSetting(new AppSettingCompositeId("DataSource.Tfs.Password", data.AppKey), "string", ""),

						new AppSetting(new AppSettingCompositeId("DataSource.Tfs2015.BaseUrl", data.AppKey), "string", ""),

						new AppSetting(new AppSettingCompositeId(SharedConstants.DataSourceRefreshTimeoutInMilisecondsKey, data.AppKey),
							"int", "30000"),
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