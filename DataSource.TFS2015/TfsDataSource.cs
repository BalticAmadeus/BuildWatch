using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Script.Serialization;
using BuildWatch.DataSource.Common;
using BuildWatch.DataSource.Common.DataService;
using DataSource.TFS2015.DataClasses;
using log4net;

namespace DataSource.TFS2015
{
    public class TfsDataSource : DataSourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (TfsDataSource));

        private string _baseUrl;

        public override void Poll(IDataService dataService, CancellationToken quitToken)
        {
            if (string.IsNullOrEmpty(_baseUrl))
                return;

            Log.Debug("Start polling");

            using (var httpClient = new HttpClient(new HttpClientHandler{UseDefaultCredentials = true}))
            {
                Log.Debug("Retrieving queued builds...");
                List<QueuedBuildInfo> queuedBuilds = GetQueuedBuilds(httpClient);

                Log.Debug("Retrieving build information...");
                List<FinishedBuildInfo> builds = GetFinishedBuilds(httpClient);

                Log.Debug("Pushing upstream");
                var req = new PushFinishedBuildsRequest
                {
                    DataSourceId = 3, // FIXME
                    BuildInfo = builds,
                    QueuedBuilds = queuedBuilds
                };

                dataService.PushFinishedBuilds(req);
            }

            Log.Debug("Polling complete");
        }

        private List<FinishedBuildInfo> GetFinishedBuilds(HttpClient httpClient)
        {
            var stringAsync = httpClient.GetStringAsync(_baseUrl + "/_apis/build/builds?api-version=2.0&statusFilter=completed");
            
            var buildObj = new JavaScriptSerializer().Deserialize<RootObject>(stringAsync.Result);
            
            var finishedBuilds = new List<FinishedBuildInfo>();

            foreach (var build in buildObj.Value.OrderByDescending(x => x.FinishTime))
            {
                if (finishedBuilds.Any(x => x.BuildName.Equals(build.Definition.Name)))
                    continue;

                var buildName = build.Definition.Name;

                if (!TryMatchBuildName(ref buildName))
                {
                    Log.Debug("... " + buildName + " (skipped)");
                    continue;
                }

                Log.Debug("... " + buildName + " (pushing)");

                string result = "OK";
                if (build.Result == "failed")
                    result = "FAIL";

                var finishedBuildInfo = new FinishedBuildInfo
                {
                    BuildInstance = build.BuildNumber,
                    BuildName = buildName,
                    Result = result,
                    UserAction = "Build",
                    TimeStamp = DateTime.Parse(build.FinishTime, CultureInfo.CurrentCulture).ToUniversalTime(),
                    UserName = build.RequestedFor.DisplayName
                };

                finishedBuilds.Add(finishedBuildInfo);
            }

            return finishedBuilds;
        }

        private List<QueuedBuildInfo> GetQueuedBuilds(HttpClient httpClient)
        {
            var inProgressStringAsync = httpClient.GetStringAsync(_baseUrl + "/_apis/build/builds?api-version=2.0&statusFilter=inProgress");
            var inProgressBuildResponse = new JavaScriptSerializer().Deserialize<RootObject>(inProgressStringAsync.Result);

            var notStartedStringAsync = httpClient.GetStringAsync(_baseUrl + "/_apis/build/builds?api-version=2.0&statusFilter=notStarted");
            var notStartedBuildResponse = new JavaScriptSerializer().Deserialize<RootObject>(notStartedStringAsync.Result);

            var postponedStringAsync = httpClient.GetStringAsync(_baseUrl + "/_apis/build/builds?api-version=2.0&statusFilter=postponed");
            var postponedBuildResponse = new JavaScriptSerializer().Deserialize<RootObject>(postponedStringAsync.Result);
            
            var queuedBuilds = new List<QueuedBuildInfo>();

            foreach (var build in Enumerable.Empty<Value>()
                .Concat(inProgressBuildResponse.Value)
                .Concat(notStartedBuildResponse.Value)
                .Concat(postponedBuildResponse.Value)
                .OrderByDescending(x => x.QueueTime))
            {
                var buildName = build.Definition.Name;

                if (!TryMatchBuildName(ref buildName))
                {
                    Log.Debug("... " + buildName + " (skipped)");
                    continue;
                }

                var queuedBuildInfo = new QueuedBuildInfo
                {
                    BuildName = buildName,
                    QueueTime = DateTime.Parse(build.QueueTime, CultureInfo.CurrentCulture).ToUniversalTime(),
                };

                queuedBuilds.Add(queuedBuildInfo);
            }

            return queuedBuilds;
        }

        public override void Initialize(DataSourceConfig config)
        {
            base.Initialize(config);

            _baseUrl = config["Tfs2015BaseUrl"];
        }
    }

}
