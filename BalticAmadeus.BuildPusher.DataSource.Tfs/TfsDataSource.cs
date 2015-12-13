using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using BalticAmadeus.BuildPusher.DataSource.Tfs.DataClasses;
using BalticAmadeus.BuildPusher.Infrastructure;
using BalticAmadeus.BuildPusher.Infrastructure.Settings;
using BalticAmadeus.BuildServer.Interfaces;
using BalticAmadeus.BuildServer.Interfaces.Builds;

namespace BalticAmadeus.BuildPusher.DataSource.Tfs
{
    public class TfsDataSource : IDataSource
    {
	    private readonly ILocalSettingsService _localSettingsService;
	    private readonly IAppSettingsService _appSettingsService;
	    private readonly IHttpClientWrapper _httpClientWrapper;

	    private string _buildServerHost;
	    private string _dataSourceServerHost;
	    
	    public bool IsEnabled { get; private set; }

		public TfsDataSource(
			ILocalSettingsService localSettingsService, 
			IAppSettingsService appSettingsService,
			IHttpClientWrapper httpClientWrapper)
		{
			_localSettingsService = localSettingsService;
			_appSettingsService = appSettingsService;
			_httpClientWrapper = httpClientWrapper;
		}

	    public void Initialize()
		{
			_buildServerHost = _localSettingsService.ApiUrlBase;

			_dataSourceServerHost = _appSettingsService.GetString(SharedConstants.DataSource.TfsBaseUrlKey);
		    
			IsEnabled = true;
			if (string.IsNullOrWhiteSpace(_buildServerHost) ||
				string.IsNullOrWhiteSpace(_dataSourceServerHost))
				IsEnabled = false;
		}
		
		private HttpClient TfsHttpClientFactory()
		{
			var httpClient = new HttpClient(new HttpClientHandler {UseDefaultCredentials = true});
			
			return httpClient;
		}

		public void SynchronizeBuilds()
		{
			if (!IsEnabled)
				return;

			PushFinishedBuilds(PullFinishedBuilds());
			PushQueuedBuilds(PullQueuedBuilds());
		}

		private value[] PullQueuedBuilds()
		{
			string inProgressUrl = $"{_dataSourceServerHost}/_apis/build/builds?api-version=2.0&statusFilter=inProgress";
			string notStartedUrl= $"{_dataSourceServerHost}/_apis/build/builds?api-version=2.0&statusFilter=notStarted";

			var queuedBuilds = new List<value>();

			var inProgressBuilds = _httpClientWrapper.Get<rootObject>(inProgressUrl, TfsHttpClientFactory);
			if (inProgressBuilds == null)
				return queuedBuilds.ToArray();

			var notStartedBuilds = _httpClientWrapper.Get<rootObject>(notStartedUrl, TfsHttpClientFactory);
			if (notStartedBuilds == null)
				return queuedBuilds.ToArray();

			queuedBuilds.AddRange(inProgressBuilds.value);
			queuedBuilds.AddRange(notStartedBuilds.value);

			return queuedBuilds.OrderByDescending(x => x.queueTime).ToArray();
		}

	    private value[] PullFinishedBuilds()
	    {
			string url = $"{_dataSourceServerHost}/_apis/build/builds?api-version=2.0&statusFilter=completed";
			var finishedBuilds = new List<value>();

			var builds = _httpClientWrapper.Get<rootObject>(url, TfsHttpClientFactory);
		    if (builds == null)
			    return finishedBuilds.ToArray();

			foreach (var build in builds.value)
			{
				if (finishedBuilds.Any(x => x.definition.id == build.definition.id))
					continue;

				finishedBuilds.Add(build);
			}

			return finishedBuilds.OrderByDescending(x => x.finishTime).ToArray();
		}

		private void PushQueuedBuilds(value[] newQueuedBuilds)
		{
			string url = $"{_buildServerHost}/builds/addBuildRun";

			foreach (var build in newQueuedBuilds)
			{
				var command = new AddBuildRunCommand(
					build.definition.id.ToString(), 
					build.buildNumber, build.definition.name, 2,
					ParseDateTime(build.queueTime),
					null, build.requestedFor.uniqueName);

				_httpClientWrapper.Post(url, command);
			}
		}

		private void PushFinishedBuilds(value[] newFinishedBuilds)
		{
			string url = $"{_buildServerHost}/builds/addBuildRun";

			foreach (var build in newFinishedBuilds)
			{
				var command = new AddBuildRunCommand(
					build.definition.id.ToString(),
					build.buildNumber, build.definition.name, 
					ParseStatus(build.result),
					ParseDateTime(build.queueTime),
					ParseDateTime(build.finishTime),
					build.requestedFor.uniqueName);

				_httpClientWrapper.Post(url, command);
			}
		}

	    private static DateTime ParseDateTime(string dateTimeString)
	    {
		    return DateTime.Parse(dateTimeString, CultureInfo.CurrentUICulture).ToUniversalTime();
	    }

	    private static int ParseStatus(string status)
	    {
			return string.Compare(status, "succeeded", StringComparison.InvariantCultureIgnoreCase) == 0 ? 0 : 1;
	    }
    }
}
