using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using BalticAmadeus.BuildPusher.DataSource.TeamCity.DataClasses;
using BalticAmadeus.BuildPusher.Infrastructure;
using BalticAmadeus.BuildPusher.Infrastructure.Settings;
using BalticAmadeus.BuildServer.Interfaces;
using BalticAmadeus.BuildServer.Interfaces.Builds;

namespace BalticAmadeus.BuildPusher.DataSource.TeamCity
{
    public class TeamCityDataSource : IDataSource
    {
	    private readonly IAppSettingsService _appSettingsService;
	    private readonly ILocalSettingsService _localSettingsService;
	    private readonly IHttpClientWrapper _teamCityHttpClientWrapper;

	    public string BuildServerHost { get; private set; }
	    public string DataSourceServerHost { get; private set; }

		private string _teamCityUsername;
	    private string _teamCityPassword;

		public bool IsEnabled { get; private set; }
		
		public TeamCityDataSource(
			IAppSettingsService appSettingsService, 
			ILocalSettingsService localSettingsService, 
			IHttpClientWrapper teamCityHttpClientWrapper)
		{
			_appSettingsService = appSettingsService;
			_localSettingsService = localSettingsService;
			_teamCityHttpClientWrapper = teamCityHttpClientWrapper;
		}

	    public void Initialize()
	    {
			BuildServerHost = _localSettingsService.ApiUrlBase;

			DataSourceServerHost = _appSettingsService.GetString(SharedConstants.DataSource.TeamCityBaseUrlKey);
			_teamCityUsername = _appSettingsService.GetString(SharedConstants.DataSource.TeamCityUsernameKey);
			_teamCityPassword = _appSettingsService.GetString(SharedConstants.DataSource.TeamCityPasswordKey);

			IsEnabled = true;
			if (string.IsNullOrWhiteSpace(BuildServerHost) ||
				string.IsNullOrWhiteSpace(DataSourceServerHost) ||
				string.IsNullOrWhiteSpace(_teamCityUsername) ||
				string.IsNullOrWhiteSpace(_teamCityPassword))
				IsEnabled = false;
		}

	    private void ConfigureTeamCityHttpClient(HttpClient client)
	    {
			string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_teamCityUsername}:{_teamCityPassword}"));
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
		}

		public void SynchronizeBuilds()
	    {
			if (!IsEnabled)
				return;

		    PushFinishedBuilds(PullFinishedBuilds());
		    PushQueuedBuilds(PullQueuedBuilds());
	    }

		private void PushQueuedBuilds(build[] newQueuedBuilds)
		{
			string url = $"{BuildServerHost}/builds/addBuildRun";

			foreach (var build in newQueuedBuilds)
		    {
			    var username = build.triggered.type == "user"
				    ? build.triggered.user.username
				    : build.lastChanges.changes.First().username;

			    var command = new AddBuildRunCommand(
				    build.buildTypeId, build.id.ToString(),
				    build.buildTypeId, 2,
				    ParseDateTime(build.queuedDate), 
					null, username);

				_teamCityHttpClientWrapper.Post(url, command);
		    }
		}

	    private void PushFinishedBuilds(build[] newFinishedBuilds)
	    {
			string url = $"{BuildServerHost}/builds/addBuildRun";

			foreach (var build in newFinishedBuilds)
			{
				var username = build.triggered.type == "user"
					? build.triggered.user.username
					: build.lastChanges.changes.First().username;

				var command = new AddBuildRunCommand(
					build.buildTypeId, build.id.ToString(),
					build.buildTypeId,
					ParseStatus(build.status),
					ParseDateTime(build.queuedDate),
					ParseDateTime(build.finishDate),
					username);

				_teamCityHttpClientWrapper.Post(url, command);
			}
		}

	    private build[] PullQueuedBuilds()
	    {
		    string url = $"{DataSourceServerHost}/builds?locator=running:true";
			var queuedBuilds = new List<build>();

			var buildObj = _teamCityHttpClientWrapper.Get<builds>(url, ConfigureTeamCityHttpClient);		
		    if (buildObj == null)
			    return queuedBuilds.ToArray();
		    if (buildObj.build == null)
			    return queuedBuilds.ToArray();

		    foreach (var build in buildObj.build.OrderByDescending(x => x.number))
		    {
			    string detailsUrl = $"{DataSourceServerHost}/builds/id:{build.id}";

			    var buildInfo = _teamCityHttpClientWrapper.Get<build>(detailsUrl, ConfigureTeamCityHttpClient);
			    if (buildInfo == null)
				    continue;

				queuedBuilds.Add(buildInfo);
		    }

			return queuedBuilds.OrderByDescending(x => x.queuedDate).ToArray();
	    }

	    private build[] PullFinishedBuilds()
		{
			string url = $"{DataSourceServerHost}/builds";
			
			var finishedBuilds = new List<build>();

			var buildObj = _teamCityHttpClientWrapper.Get<builds>(url, ConfigureTeamCityHttpClient);
			if (buildObj == null)
				return finishedBuilds.ToArray();
			if (buildObj.build == null)
				return finishedBuilds.ToArray();

			foreach (var build in buildObj.build.OrderByDescending(x => x.number))
		    {
			    if (finishedBuilds.Any(x => x.buildTypeId == build.buildTypeId))
				    continue;

				string detailsUrl = $"{DataSourceServerHost}/builds/id:{build.id}";

				var buildInfo = _teamCityHttpClientWrapper.Get<build>(detailsUrl, ConfigureTeamCityHttpClient);
				if (buildInfo == null)
					continue;

				finishedBuilds.Add(buildInfo);
		    }

			return finishedBuilds.OrderByDescending(x => x.finishDate).ToArray();
	    }

	    private static DateTime ParseDateTime(string dateTimeString)
	    {
			var tempDate = DateTime.ParseExact(dateTimeString, "yyyyMMdd'T'HHmmss+ffff", CultureInfo.CurrentUICulture).ToUniversalTime();
			return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, tempDate.Hour, tempDate.Minute, tempDate.Second).ToUniversalTime();
		}

	    private static int ParseStatus(string status)
	    {
		    return string.Compare(status, "SUCCESS", StringComparison.InvariantCultureIgnoreCase) == 0 ? 0 : 1;
	    }
    }
}
