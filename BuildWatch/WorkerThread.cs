using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BuildWatch.Properties;
using Microsoft.TeamFoundation.Client;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Build.Client;

namespace BuildWatchWorker
{
    enum BuildColor {
        GREEN, RED, WHITE
    }

    class BuildInfo
    {
        public string Name { get; set; }
        public BuildColor Color { get; set; }
        public DateTime FinishTime { get; set; }
        public string User { get; set; }
    }

    class BuildTopSorter : IComparer<BuildInfo>
    {
        public int Compare(BuildInfo x, BuildInfo y)
        {
            if (x.Color != y.Color)
                return ColorWeight(x.Color) - ColorWeight(y.Color);
            else
                return y.FinishTime.CompareTo(x.FinishTime);
        }

        private int ColorWeight(BuildColor c)
        {
            switch (c)
            {
                case BuildColor.GREEN:
                    return 2;
                case BuildColor.RED:
                    return 1;
                default:
                    return 0;
            }
        }
    }

    interface IWorkerThread
    {
        void InitConnection(IWin32Window parent);
        void Start();
        List<string> RetrieveLogMessages();
        List<BuildInfo> RetrieveBuildTop();
    }

    class WorkerThread : IWorkerThread
    {
        private Thread thread;
        private Uri serverUri;
        private TfsConfigurationServer configServer;
        private string projectCollectionName;
        private string projectName;
        private TfsTeamProjectCollection projectCollection;
        private IBuildServer buildService;

        private object syncObject;
        private List<string> logMessages;
        private List<BuildInfo> buildTop;

        public WorkerThread()
        {
            thread = new Thread(ThreadMain);
            thread.IsBackground = true;
            serverUri = new Uri(Settings.Default.TfsServerUri);
            syncObject = new object();
        }

        public void InitConnection(IWin32Window parent)
        {
            configServer = TfsConfigurationServerFactory.GetConfigurationServer(serverUri, new UICredentialsProvider(parent));

            projectCollectionName = Settings.Default.TfsProjectCollection;
            projectName = Settings.Default.TfsTeamProject;

            var collectionNodes = configServer.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);

            var collectionNode = collectionNodes.FirstOrDefault(n => n.Resource.DisplayName == projectCollectionName);
            if (collectionNode == null)
                throw new KeyNotFoundException(string.Format("Project collection {0} was not found at {1}",
                    projectCollectionName, serverUri));

            var projectNodes = collectionNode.QueryChildren(
                new[] { CatalogResourceTypes.TeamProject },
                false, CatalogQueryOptions.None);

            var projectNode = projectNodes.FirstOrDefault(p => p.Resource.DisplayName == projectName);
            if (projectNode == null)
                throw new KeyNotFoundException(string.Format("Project {0} was not found in {1} at {2}",
                    projectName, projectCollectionName, serverUri));

            projectCollection = configServer.GetTeamProjectCollection(new Guid(collectionNode.Resource.Properties["InstanceId"]));
            buildService = projectCollection.GetService<IBuildServer>();
        }

        public void Start()
        {
            thread.Start();
        }

        private void ThreadMain()
        {
            bool firstTime = true;
            int sleepTime = Settings.Default.PollPaceSleep;
            if (sleepTime < 2000)
                sleepTime = 2000;
            else if (sleepTime > 300000)
                sleepTime = 300000;
            while (true)
            {
                // Pace maker
                if (!firstTime)
                {
                    Log("Sleeping " + sleepTime + "ms...");
                    Thread.Sleep(sleepTime);
                }
                try
                {
                    // Build preliminary build list fast
                    if (firstTime)
                    {
                        firstTime = false;
                        List<BuildInfo> fastList = new List<BuildInfo>();
                        foreach (IBuildDefinition bdef in buildService.QueryBuildDefinitions(projectName))
                        {
                            fastList.Add(new BuildInfo()
                            {
                                Name = bdef.Name,
                                Color = BuildColor.WHITE,
                                User = ""
                            });
                            lock (syncObject)
                            {
                                buildTop = fastList;
                            }
                        }
                    }
                    // Go through each build definition and retrieve last status
                    Log("Retrieving build information...");
                    IBuildDefinition[] allBuilds = buildService.QueryBuildDefinitions(projectName);
                    List<BuildInfo> top = new List<BuildInfo>();
                    foreach (IBuildDefinition bdef in allBuilds)
                    {
                        Log("..." + bdef.Name);
                        var bdSpec = buildService.CreateBuildDetailSpec(bdef);
                        bdSpec.MaxBuildsPerDefinition = 1;
                        bdSpec.QueryOrder = BuildQueryOrder.FinishTimeDescending;
                        bdSpec.Status = BuildStatus.Failed | BuildStatus.Succeeded | BuildStatus.PartiallySucceeded;
                        var bq = buildService.QueryBuilds(bdSpec);
                        Log("" + bq.Builds.Length + " builds, " + bq.Failures.Length + " failures");
                        if (bq.Builds.Length != 1)
                            continue;
                        IBuildDetail bd = bq.Builds[0];
                        var bi = new BuildInfo()
                        {
                            Name = bd.BuildDefinition.Name,
                            Color = (bd.Status == BuildStatus.Succeeded) ? BuildColor.GREEN : BuildColor.RED,
                            FinishTime = bd.FinishTime,
                            User = bd.RequestedFor
                        };
                        top.Add(bi);
                    }
                    Log("Sorting builds...");
                    top.Sort(new BuildTopSorter());
                    lock (syncObject)
                    {
                        buildTop = top;
                    }
                }
                catch (Exception ex)
                {
                    Log(string.Format("Exception: {0}: {1}", ex.GetType().FullName, ex.Message));
                }
            }
        }

        private void Log(string message)
        {
            string line = string.Format("[{0}] {1}\n", DateTime.Now, message);
            lock (syncObject)
            {
                if (logMessages == null)
                    logMessages = new List<string>();
                logMessages.Add(line);
            }
        }

        public List<string> RetrieveLogMessages()
        {
            lock (syncObject)
            {
                var tmp = logMessages;
                logMessages = null;
                return tmp;
            }
        }

        public List<BuildInfo> RetrieveBuildTop()
        {
            lock (syncObject)
            {
                var tmp = buildTop;
                buildTop = null;
                return tmp;
            }
        }
    }

    class DemoWorkerThread : IWorkerThread
    {
        private DateTime eventTime;
        private int eventCounter;
        private List<BuildInfo> master;
        private Random rnd;
        private List<string> logMessages;

        public void InitConnection(IWin32Window parent)
        {
            // nothing
        }

        public void Start()
        {
            Log("!!! STARTED IN DEMO MODE !!!");
            master = new List<BuildInfo>();
            master.Add(new BuildInfo
            {
                Name = "Main Engine",
                User = "peter",
                Color = BuildColor.WHITE
            });
            master.Add(new BuildInfo
            {
                Name = "UI Tools",
                User = "peter",
                Color = BuildColor.WHITE
            });
            master.Add(new BuildInfo
            {
                Name = "Web Interface",
                User = "john",
                Color = BuildColor.WHITE
            });
            master.Add(new BuildInfo
            {
                Name = "3D Support Libs",
                User = "cris",
                Color = BuildColor.WHITE
            });
            master.Add(new BuildInfo
            {
                Name = "Installer",
                User = "gordon",
                Color = BuildColor.WHITE
            });
            eventTime = DateTime.Now.AddSeconds(-1);
            eventCounter = -3;
            rnd = new Random();
        }

        public List<string> RetrieveLogMessages()
        {
            var tmp = logMessages;
            logMessages = null;
            return tmp;
        }

        public List<BuildInfo> RetrieveBuildTop()
        {
            if (eventTime > DateTime.Now)
                return null;
            eventCounter++;
            eventTime = DateTime.Now.AddSeconds(20);
            if (eventCounter == -2)
            {
                eventTime = DateTime.Now.AddSeconds(10);
                return master.ToList();
            }
            else if (eventCounter == -1)
            {
                foreach (BuildInfo bi in master)
                {
                    bi.Color = BuildColor.GREEN;
                    bi.FinishTime = DateTime.Now.AddSeconds(rnd.NextDouble() * -3600 - 300);
                }
                master.Sort(new BuildTopSorter());
                return master.ToList();
            }
            int i;
            BuildInfo b;
            switch (eventCounter % 5) {
                case 0:
                    i = rnd.Next(master.Count);
                    Log("DEMO: Build " + master[i].Name);
                    master[i].FinishTime = DateTime.Now;
                    master.Sort(new BuildTopSorter());
                    return master.ToList();
                case 1:
                    i = rnd.Next(master.Count);
                    Log("DEMO: Fail " + master[i].Name);
                    master[i].FinishTime = DateTime.Now;
                    master[i].Color = BuildColor.RED;
                    master.Sort(new BuildTopSorter());
                    return master.ToList();
                case 2:
                    i = rnd.Next(master.Count);
                    Log("DEMO: Recover " + master[i].Name);
                    master[i].FinishTime = DateTime.Now;
                    master[i].Color = BuildColor.GREEN;
                    master.Sort(new BuildTopSorter());
                    return master.ToList();
                case 3:
                    i = rnd.Next(master.Count);
                    b = master[i];
                    if (master.Count(p => p.Color == BuildColor.RED) >= 2)
                    {
                        Log("DEMO: Recover " + b.Name);
                        b.Color = BuildColor.GREEN;
                    }
                    else
                    {
                        Log("DEMO: Change state " + b.Name);
                        b.Color = (b.Color == BuildColor.RED) ? BuildColor.GREEN : BuildColor.RED;
                    }
                    b.FinishTime = DateTime.Now;
                    master.Sort(new BuildTopSorter());
                    return master.ToList();
                case 4:
                    b = master.Where(p => p.Color == BuildColor.RED).LastOrDefault();
                    if (b == null)
                        return null;
                    Log("DEMO: Recover " + b.Name);
                    b.FinishTime = DateTime.Now;
                    b.Color = BuildColor.GREEN;
                    master.Sort(new BuildTopSorter());
                    return master.ToList();
                default:
                    return null;
            }
        }

        private void Log(string message)
        {
            string line = string.Format("[{0}] {1}\n", DateTime.Now, message);
            if (logMessages == null)
                logMessages = new List<string>();
            logMessages.Add(line);
        }
    }
}
