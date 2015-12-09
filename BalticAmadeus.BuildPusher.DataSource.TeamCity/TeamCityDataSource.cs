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
	    private readonly string _buildServerBaseUrl;
	    private readonly string _teamCityBaseUrl;
	    private readonly string _teamCityUsername;
	    private readonly string _teamCityPassword;

		public bool IsEnabled { get; private set; }

		public TeamCityDataSource(IAppSettingsService appSettingsService, ILocalSettingsService localSettingsService)
	    {
		    _buildServerBaseUrl = localSettingsService.ApiUrlBase;
		    _teamCityBaseUrl = appSettingsService.GetString(SharedConstants.DataSourceTeamCityBaseUrlKey);
			_teamCityUsername = appSettingsService.GetString(SharedConstants.DataSourceTeamCityUsernameKey);
			_teamCityPassword = appSettingsService.GetString(SharedConstants.DataSourceTeamCityPasswordKey);

			IsEnabled = true;
			if (string.IsNullOrWhiteSpace(_buildServerBaseUrl) ||
				string.IsNullOrWhiteSpace(_teamCityBaseUrl) ||
		        string.IsNullOrWhiteSpace(_teamCityUsername) ||
		        string.IsNullOrWhiteSpace(_teamCityPassword))
			    IsEnabled = false;
		}

	    public void SynchronizeBuilds()
	    {
			IEnumerable<build> newQueuedBuilds;
			IEnumerable<build> newFinishedBuilds;

			using (var httpClient = new HttpClient())
			{
				string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_teamCityUsername}:{_teamCityPassword}"));
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

				newQueuedBuilds = PullQueuedBuilds(httpClient);
				newFinishedBuilds = PullFinishedBuilds(httpClient);
			}

			using (var httpClient = new HttpClient())
			{
				PushFinishedBuilds(httpClient, newFinishedBuilds);
				PushQueuedBuilds(httpClient, newQueuedBuilds);
			}
		}

		private void PushQueuedBuilds(HttpClient httpClient, IEnumerable<build> newQueuedBuilds)
	    {
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

			    var addBuildRunResponse = httpClient.PostAsync($"{_buildServerBaseUrl}/builds/addBuildRun", command.AsJsonStringContent()).Result;
				if (!addBuildRunResponse.IsSuccessStatusCode)
					throw new NotImplementedException("Handle this!");
		    }
	    }

	    private void PushFinishedBuilds(HttpClient httpClient, IEnumerable<build> newFinishedBuilds)
	    {
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

				var addBuildRunResponse = httpClient.PostAsync($"{_buildServerBaseUrl}/builds/addBuildRun", command.AsJsonStringContent()).Result;
				if (!addBuildRunResponse.IsSuccessStatusCode)
					throw new NotImplementedException("Handle this!");
			}
	    }

	    private IEnumerable<build> PullQueuedBuilds(HttpClient httpClient)
	    {
		    var buildsResp = httpClient.GetAsync($"{_teamCityBaseUrl}/builds?locator=running:true").Result;
		    var buildObj = buildsResp.Content.As<builds>();

		    var queuedBuilds = new List<build>();

		    if (buildObj.build == null)
			    return queuedBuilds;

		    foreach (var build in buildObj.build.OrderByDescending(x => x.number))
		    {
			    var buildInfo = GetBuildInfo(httpClient, build.id);

			    queuedBuilds.Add(buildInfo);
		    }

		    return queuedBuilds.OrderByDescending(x => x.queuedDate);
	    }

	    private IEnumerable<build> PullFinishedBuilds(HttpClient httpClient)
	    {
		    var buildsResp = httpClient.GetAsync($"{_teamCityBaseUrl}/builds").Result;
		    var buildObj = buildsResp.Content.As<builds>();

		    var finishedBuilds = new List<build>();

		    foreach (var build in buildObj.build.OrderByDescending(x => x.number))
		    {
			    if (finishedBuilds.Any(x => x.buildTypeId == build.buildTypeId))
				    continue;

			    var buildInfo = GetBuildInfo(httpClient, build.id);

			    finishedBuilds.Add(buildInfo);
		    }

		    return finishedBuilds.OrderByDescending(x => x.finishDate);
	    }

	    private build GetBuildInfo(HttpClient httpClient, int id)
		{
			var buildResp = httpClient.GetAsync($"{_teamCityBaseUrl}/builds/id:{id}").Result;
			var build = buildResp.Content.As<build>();

			return build;
		}

	    private static DateTime ParseDateTime(string dateTimeString)
	    {
			var tempDate = DateTime.ParseExact(dateTimeString, "yyyyMMdd'T'HHmmss+ffff", CultureInfo.CurrentCulture).ToUniversalTime();
			return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, tempDate.Hour, tempDate.Minute, tempDate.Second).ToUniversalTime();
		}

	    private static int ParseStatus(string status)
	    {
		    return string.Compare(status, "SUCCESS", StringComparison.InvariantCultureIgnoreCase) == 0 ? 0 : 1;
	    }
    }
}
