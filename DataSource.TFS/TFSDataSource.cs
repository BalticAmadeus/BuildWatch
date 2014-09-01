using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildWatch.DataSource.Common;
using BuildWatch.DataSource.Common.DataService;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Build.Client;

namespace BuildWatch.DataSource.TFS
{
    public class TFSDataSource : DataSourceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(TFSDataSource));

        public string TeamServerUri { get; private set; }
        public string TeamProjectCollection { get; private set; }
        public string TeamProjectName { get; private set; }
        public string TeamServerUser { get; private set; }
        public string TeamServerPassword { get; private set; }

        private TfsConfigurationServer configServer;
        private TfsTeamProjectCollection projectCollection;
        private IBuildServer buildService;

        public override void Initialize(DataSourceConfig config)
        {
            base.Initialize(config);
            TeamServerUri = config["TeamServerUri"];
            TeamProjectCollection = config["TeamProjectCollection"];
            TeamProjectName = config["TeamProjectName"];
            TeamServerUser = config.Get("TeamServerUser", "");
            TeamServerPassword = config.Get("TeamServerPassword", "");
            log.Info(string.Format("TFS Data Source initialized for {0}, {1}/{2}", TeamServerUri, TeamProjectCollection, TeamProjectName));
        }

        public override void Poll(IDataService dataService, System.Threading.CancellationToken quitToken)
        {
            log.Debug("TFS: Start polling");

            if (configServer == null)
            {
                if (!string.IsNullOrWhiteSpace(TeamServerUser))
                    configServer = new TfsConfigurationServer(new Uri(TeamServerUri), new System.Net.NetworkCredential(TeamServerUser, TeamServerPassword));
                else
                    configServer = new TfsConfigurationServer(new Uri(TeamServerUri));
            }

            if (projectCollection == null)
            {
                var collectionNodes = configServer.CatalogNode.QueryChildren(
                    new[] { CatalogResourceTypes.ProjectCollection },
                    false, CatalogQueryOptions.None);

                var collectionNode = collectionNodes.FirstOrDefault(n => n.Resource.DisplayName == TeamProjectCollection);
                if (collectionNode == null)
                    throw new KeyNotFoundException(string.Format("Project collection {0} was not found at {1}",
                        TeamProjectCollection, TeamServerUri));

                projectCollection = configServer.GetTeamProjectCollection(new Guid(collectionNode.Resource.Properties["InstanceId"]));
            }

            if (buildService == null)
            {
                buildService = projectCollection.GetService<IBuildServer>();
            }

            // Go through each build definition and retrieve last status
            log.Debug("Retrieving build information...");
            IBuildDefinition[] allBuilds = buildService.QueryBuildDefinitions(TeamProjectName);
            List<FinishedBuildInfo> builds = new List<FinishedBuildInfo>();
            foreach (IBuildDefinition bdef in allBuilds)
            {
                if (quitToken.IsCancellationRequested)
                {
                    log.Info("Aborting");
                    return;
                }
                string buildName = bdef.Name;
                if (!TryMatchBuildName(ref buildName))
                {
                    log.Debug("... " + buildName + " (skipped)");
                    continue;
                }
                log.Debug("... " + bdef.Name + " -> " + buildName);
                var bdSpec = buildService.CreateBuildDetailSpec(bdef);
                bdSpec.MaxBuildsPerDefinition = 1;
                bdSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;
                bdSpec.Status = BuildStatus.Failed | BuildStatus.Succeeded | BuildStatus.PartiallySucceeded;
                var bq = buildService.QueryBuilds(bdSpec);
                if (bq.Builds.Length != 1)
                    continue;
                IBuildDetail bd = bq.Builds[0];
                var bi = new FinishedBuildInfo()
                {
                    BuildInstance = bd.BuildNumber,
                    BuildName = buildName,
                    TimeStamp = bd.FinishTime,
                    Result = (bd.Status == BuildStatus.Succeeded) ? "OK" : "FAIL",
                    UserName = bd.RequestedFor,
                    UserAction = bd.Reason.ToString() // FIXME
                };
                builds.Add(bi);
            }

            log.Debug("Pushing upstream");
            var req = new PushFinishedBuildsRequest
            {
                DataSourceId = 1, // FIXME
                BuildInfo = builds
            };
            dataService.PushFinishedBuilds(req);

            log.Debug("TFS: Polling complete");
        }
    }
}
