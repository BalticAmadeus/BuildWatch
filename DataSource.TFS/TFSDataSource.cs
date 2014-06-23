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
    public class TFSDataSource : IDataSource
    {
        public string TeamServerUri { get; private set; }
        public string TeamProjectCollection { get; private set; }
        public string TeamProjectName { get; private set; }
        public string TeamServerUser { get; private set; }
        public string TeamServerPassword { get; private set; }

        private TfsConfigurationServer configServer;
        private TfsTeamProjectCollection projectCollection;
        private IBuildServer buildService;

        public void Initialize(DataSourceConfig config)
        {
            TeamServerUri = config["TeamServerUri"];
            TeamProjectCollection = config["TeamProjectCollection"];
            TeamProjectName = config["TeamProjectName"];
            TeamServerUser = config.Get("TeamServerUser", "");
            TeamServerPassword = config.Get("TeamServerPassword", "");
            Console.WriteLine(string.Format("TFS Data Source initialized for {0}, {1}/{2}", TeamServerUri, TeamProjectCollection, TeamProjectName));
        }

        public void Poll(IDataService dataService, System.Threading.CancellationToken quitToken)
        {
            Console.WriteLine("TFS: Start polling");

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
            Console.WriteLine("Retrieving build information...");
            IBuildDefinition[] allBuilds = buildService.QueryBuildDefinitions(TeamProjectName);
            List<FinishedBuildInfo> builds = new List<FinishedBuildInfo>();
            foreach (IBuildDefinition bdef in allBuilds)
            {
                Console.WriteLine("..." + bdef.Name);
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
                    BuildName = bd.BuildDefinition.Name,
                    TimeStamp = bd.FinishTime,
                    Result = (bd.Status == BuildStatus.Succeeded) ? "OK" : "FAIL",
                    UserName = bd.RequestedFor,
                    UserAction = bd.Reason.ToString() // FIXME
                };
                builds.Add(bi);
            }

            Console.WriteLine("Pushing upstream");
            var req = new PushFinishedBuildsRequest
            {
                DataSourceId = 1, // FIXME
                BuildInfo = builds
            };
            dataService.PushFinishedBuilds(req);

            Console.WriteLine("TFS: Polling complete");
        }
    }
}
