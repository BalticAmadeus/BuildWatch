using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BuildWatch.DataSource.Common;
using BuildWatch.DataSource.Common.DataService;
using DataSource.TC.DataClasses;
using log4net;

namespace DataSource.TC
{
	public class TCDataSource : DataSourceBase
	{
		private static readonly ILog log = LogManager.GetLogger(typeof (TCDataSource));

		public string Password { get; set; }
		public string UserName { get; set; }
		public string BaseUrl { get; set; }

		public override void Poll(IDataService dataService, CancellationToken quitToken)
		{
            if (string.IsNullOrEmpty(BaseUrl))
                return;

			log.Debug("Start polling");

			using (var httpClient = new HttpClient())
			{
				string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", UserName, Password)));
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

				log.Debug("Retrieving queued builds...");
				List<QueuedBuildInfo> queuedBuilds = GetQueuedBuilds(httpClient);

				log.Debug("Retrieving build information...");
				List<FinishedBuildInfo> builds = GetFinishedBuilds(httpClient);

				log.Debug("Pushing upstream");
				var req = new PushFinishedBuildsRequest
				{
					DataSourceId = 2, // FIXME
					BuildInfo = builds,
					QueuedBuilds = queuedBuilds
				};

				dataService.PushFinishedBuilds(req);
			}

			log.Debug("Polling complete");
		}

		private List<FinishedBuildInfo> GetFinishedBuilds(HttpClient httpClient)
		{
			Task<string> stringAsync = httpClient.GetStringAsync(BaseUrl + "/builds");

			var buildObj = (builds) new XmlSerializer(typeof (builds)).Deserialize(new StringReader(stringAsync.Result));

			var finishedBuilds = new List<FinishedBuildInfo>();

			foreach (buildsBuild build in buildObj.build.OrderByDescending(x => x.number))
			{
				if (finishedBuilds.Any(x => x.BuildName == build.buildTypeId))
					continue;

				var buildName = build.buildTypeId;

				if (!TryMatchBuildName(ref buildName))
				{
					log.Debug("... " + buildName + " (skipped)");
					continue;
				}
                
                log.Debug("... " + buildName + " (pushing)");

				var bi = new FinishedBuildInfo
				{
					BuildInstance = build.number.ToString(),
					BuildName = buildName,
					Result = string.Compare(build.status, "SUCCESS", StringComparison.InvariantCultureIgnoreCase) == 0 ? "OK" : "FAIL",
					UserAction = "Build"
				};

				var buildInfo = GetBuildInfo(httpClient, build.id);

				bi.TimeStamp = DateTime.ParseExact(buildInfo.queuedDate, "yyyyMMdd'T'HHmmss+ffff", CultureInfo.CurrentCulture)
					.ToUniversalTime();

				if (buildInfo.triggered.type == "user")
				{
					// manually triggered build
					bi.UserName = buildInfo.triggered.user.username;
				}
				else
				{
					bi.UserName = buildInfo.lastChanges.changes.First().username;
				}

				finishedBuilds.Add(bi);
			}

			return finishedBuilds;
		}

		private build GetBuildInfo(HttpClient httpClient, ushort id)
		{
			var buildInfoString = httpClient.GetStringAsync(BaseUrl + "/builds/id:" + id).Result;
			var buildInfo = (build) new XmlSerializer(typeof (build)).Deserialize(new StringReader(buildInfoString));
			return buildInfo;
		}

		private List<QueuedBuildInfo> GetQueuedBuilds(HttpClient httpClient)
		{
			Task<string> stringAsync = httpClient.GetStringAsync(BaseUrl + "/builds?locator=running:true");

			var buildObj = (builds) new XmlSerializer(typeof (builds)).Deserialize(new StringReader(stringAsync.Result));

			var queuedBuilds = new List<QueuedBuildInfo>();

			if (buildObj.build != null)
			{
				foreach (buildsBuild build in buildObj.build)
				{
					var buildName = build.buildTypeId;

					if (!TryMatchBuildName(ref buildName))
					{
						log.Debug("... " + buildName + " (skipped)");
						continue;
					}

					var qbi = new QueuedBuildInfo
					{
						BuildName = buildName
					};

					var buildInfo = GetBuildInfo(httpClient, build.id);
					qbi.QueueTime = DateTime.ParseExact(buildInfo.queuedDate, "yyyyMMdd'T'HHmmss+ffff", CultureInfo.CurrentUICulture)
						.ToUniversalTime();

					queuedBuilds.Add(qbi);
				}
			}

			return queuedBuilds;
		}

		public override void Initialize(DataSourceConfig config)
		{
			base.Initialize(config);

			BaseUrl = config["TCBaseUrl"];
			UserName = config["TCUserName"];
			Password = config["TCPassword"];
		}
	}
}