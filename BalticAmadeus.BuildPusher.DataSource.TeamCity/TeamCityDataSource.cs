using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using BalticAmadeus.BuildPusher.DataSource.TeamCity.DataClasses;
using BalticAmadeus.BuildPusher.Infrastructure;
using BalticAmadeus.BuildPusher.Infrastructure.Http;
using BalticAmadeus.BuildPusher.Infrastructure.Settings;
using BalticAmadeus.BuildServer.Interfaces;
using BalticAmadeus.BuildServer.Interfaces.Builds;

namespace BalticAmadeus.BuildPusher.DataSource.TeamCity
{
    public class TeamCityDataSource : IDataSource
    {
	    private readonly IAppSettingsService _appSettingsService;
	    private readonly ILocalSettingsService _localSettingsService;
	    private readonly IHttpClientWrapper _httpClientWrapper;

	    private string _buildServerHost;
	    private string _dataSourceServerHost;
		private string _teamCityUsername;
	    private string _teamCityPassword;

		public bool IsEnabled { get; private set; }
		
		public TeamCityDataSource(
			IAppSettingsService appSettingsService, 
			ILocalSettingsService localSettingsService, 
			IHttpClientWrapper httpClientWrapper)
		{
			_appSettingsService = appSettingsService;
			_localSettingsService = localSettingsService;
			_httpClientWrapper = httpClientWrapper;
		}

	    public void Initialize()
	    {
			_buildServerHost = _localSettingsService.ApiUrlBase;

			_dataSourceServerHost = _appSettingsService.GetString(SharedConstants.DataSource.TeamCityBaseUrlKey);
			_teamCityUsername = _appSettingsService.GetString(SharedConstants.DataSource.TeamCityUsernameKey);
			_teamCityPassword = _appSettingsService.GetString(SharedConstants.DataSource.TeamCityPasswordKey);

			IsEnabled = true;
			if (string.IsNullOrWhiteSpace(_buildServerHost) ||
				string.IsNullOrWhiteSpace(_dataSourceServerHost) ||
				string.IsNullOrWhiteSpace(_teamCityUsername) ||
				string.IsNullOrWhiteSpace(_teamCityPassword))
				IsEnabled = false;
		}

	    private HttpClient TeamCityHttpClientFactory()
	    {
		    var httpClient = new HttpClient();

			string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_teamCityUsername}:{_teamCityPassword}"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

		    return httpClient;
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
			string url = $"{_buildServerHost}/builds/addBuildRun";

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

				_httpClientWrapper.Post(url, command);
		    }
		}

	    private void PushFinishedBuilds(build[] newFinishedBuilds)
	    {
			string url = $"{_buildServerHost}/builds/addBuildRun";

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

				_httpClientWrapper.Post(url, command);
			}
		}

	    private build[] PullQueuedBuilds()
	    {
		    string url = $"{_dataSourceServerHost}/builds?locator=running:true";
			var queuedBuilds = new List<build>();

			var buildObj = _httpClientWrapper.Get<builds>(url, TeamCityHttpClientFactory);		
		    if (buildObj == null)
			    return queuedBuilds.ToArray();
		    if (buildObj.build == null)
			    return queuedBuilds.ToArray();

		    foreach (var build in buildObj.build.OrderByDescending(x => x.number))
		    {
			    string detailsUrl = $"{_dataSourceServerHost}/builds/id:{build.id}";

			    var buildInfo = _httpClientWrapper.Get<build>(detailsUrl, TeamCityHttpClientFactory);
			    if (buildInfo == null)
				    continue;

				queuedBuilds.Add(buildInfo);
		    }

			return queuedBuilds.OrderByDescending(x => x.queuedDate).ToArray();
	    }

	    private build[] PullFinishedBuilds()
		{
			string url = $"{_dataSourceServerHost}/builds";
			
			var finishedBuilds = new List<build>();

			var buildObj = _httpClientWrapper.Get<builds>(url, TeamCityHttpClientFactory);
			if (buildObj == null)
				return finishedBuilds.ToArray();
			if (buildObj.build == null)
				return finishedBuilds.ToArray();

			foreach (var build in buildObj.build.OrderByDescending(x => x.number))
		    {
			    if (finishedBuilds.Any(x => x.buildTypeId == build.buildTypeId))
				    continue;

				string detailsUrl = $"{_dataSourceServerHost}/builds/id:{build.id}";

				var buildInfo = _httpClientWrapper.Get<build>(detailsUrl, TeamCityHttpClientFactory);
				if (buildInfo == null)
					continue;

				finishedBuilds.Add(buildInfo);
		    }

			return finishedBuilds.OrderByDescending(x => x.finishDate).ToArray();
	    }

	    private static DateTime ParseDateTime(string dateTimeString)
	    {
			return DateTime.ParseExact(dateTimeString, "yyyyMMdd'T'HHmmss+ffff", CultureInfo.CurrentUICulture).ToUniversalTime();
		}

	    private static int ParseStatus(string status)
	    {
		    return string.Compare(status, "SUCCESS", StringComparison.InvariantCultureIgnoreCase) == 0 ? 0 : 1;
	    }
    }
}
