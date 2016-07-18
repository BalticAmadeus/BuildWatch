using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using BalticAmadeus.BuildPusher.DataSource.Tfs.DataClasses;
using BalticAmadeus.BuildPusher.Infrastructure;
using BalticAmadeus.BuildPusher.Infrastructure.Http;
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
	    private string _usernameMask;
	    private string _filter;
	    
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
		    _usernameMask = _appSettingsService.GetString(SharedConstants.DataSource.UsernameMask);
		    _filter = _appSettingsService.GetString(SharedConstants.DataSource.Filter);

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
		}

	    private value[] PullFinishedBuilds()
	    {
			string url = $"{_dataSourceServerHost}/_apis/build/builds?api-version=2.0&statusFilter=all";
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

		//private void PushQueuedBuilds(value[] newQueuedBuilds)
		//{
		//	string url = $"{_buildServerHost}/builds/addBuildRun";

		//	foreach (var build in newQueuedBuilds)
		//	{
		//		if (CanSkipByFilter(build.definition.name, _filter))
		//			continue;

		//		var command = new AddBuildRunCommand(
		//			build.definition.id.ToString(), 
		//			build.buildNumber, build.definition.name, 2, 
		//			ParseDateTime(build.queueTime) ?? DateTime.Now, null, 
		//			ParseUsername(build.requestedFor.uniqueName, _usernameMask));

		//		_httpClientWrapper.Post(url, command);
		//	}
		//}

	    private void PushFinishedBuilds(value[] newFinishedBuilds)
		{
			string url = $"{_buildServerHost}/builds/addBuildRun";

			foreach (var build in newFinishedBuilds)
			{
				if (CanSkipByFilter(build.definition.name, _filter))
					continue;

				var command = new AddBuildRunCommand(
					build.definition.id.ToString(),
					build.buildNumber, build.definition.name, 
					ParseStatus(build.result),
					ParseDateTime(build.queueTime) ?? DateTime.Now, //HACK: Check why there can be null value
					ParseDateTime(build.finishTime),
					ParseUsername(build.requestedFor.uniqueName, _usernameMask));

				_httpClientWrapper.Post(url, command);
			}
		}

	    private static DateTime? ParseDateTime(string dateTimeString)
	    {
		    if (string.IsNullOrWhiteSpace(dateTimeString))
			    return null;

		    return DateTime.Parse(dateTimeString, CultureInfo.CurrentUICulture).ToUniversalTime();
	    }

	    private static int ParseStatus(string status)
	    {
			return string.Compare(status, "succeeded", StringComparison.InvariantCultureIgnoreCase) == 0 ? 0 : 1;
	    }

	    private static string ParseUsername(string username, string mask)
	    {
		    if (string.IsNullOrWhiteSpace(mask))
			    return username;

		    return Regex.Replace(username, mask, string.Empty, RegexOptions.IgnoreCase);
	    }

	    private static bool CanSkipByFilter(string buildName, string filter)
		{
			if (string.IsNullOrWhiteSpace(filter))
				return false;

			return Regex.IsMatch(buildName, filter, RegexOptions.IgnoreCase);
	    }
    }
}
