using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BuildWatch.Properties;
using System.Windows.Forms;
using BuildWatch.ClientService;

namespace BuildWatchWorker
{
    enum BuildColor {
        GREEN, RED, WHITE, QUEUED
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
            else if (x.Color == BuildColor.QUEUED)
                return x.FinishTime.CompareTo(y.FinishTime);
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
        void RetrieveBuildTop(out List<BuildInfo> buildTop, out DateTime buildTimestamp);
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

        public void RetrieveBuildTop(out List<BuildInfo> buildTop, out DateTime buildTimestamp)
        {
            if (eventTime > DateTime.Now)
            {
                buildTop = null;
                buildTimestamp = DateTime.Now;
                return;
            }
            eventCounter++;
            eventTime = DateTime.Now.AddSeconds(20);
            if (eventCounter == -2)
            {
                eventTime = DateTime.Now.AddSeconds(10);
                buildTop = master.ToList();
                buildTimestamp = DateTime.Now;
                return;
            }
            else if (eventCounter == -1)
            {
                foreach (BuildInfo bi in master)
                {
                    bi.Color = BuildColor.GREEN;
                    bi.FinishTime = DateTime.Now.AddSeconds(rnd.NextDouble() * -3600 - 300);
                }
                master.Sort(new BuildTopSorter());
                buildTop = master.ToList();
                buildTimestamp = DateTime.Now;
                return;
            }
            int i, j;
            BuildInfo b;
            switch (eventCounter % 5) {
                case 0:
                    i = rnd.Next(master.Count);
                    Log("DEMO: Build " + master[i].Name);
                    master[i].FinishTime = DateTime.Now;
                    master.Sort(new BuildTopSorter());
                    buildTop = master.ToList();
                    if (eventCounter % 20 == 5)
                    {
                        Log("DEMO: Stale");
                        buildTimestamp = DateTime.Now.AddMinutes(-20);
                        return;
                    }
                    if (eventCounter % 20 == 0)
                    {
                        for (j = rnd.Next(master.Count); j >= 0; j--)
                        {
                            i = rnd.Next(master.Count);
                            Log("DEMO: Queue " + master[i].Name);
                            buildTop.Add(new BuildInfo
                            {
                                Name = master[i].Name,
                                Color = BuildColor.QUEUED,
                                FinishTime = DateTime.Now.AddMinutes(-j),
                                User = ""
                            });
                        }
                        buildTop.Sort(new BuildTopSorter());
                    }
                    buildTimestamp = DateTime.Now;
                    return;
                case 1:
                    i = rnd.Next(master.Count);
                    Log("DEMO: Fail " + master[i].Name);
                    master[i].FinishTime = DateTime.Now;
                    master[i].Color = BuildColor.RED;
                    master.Sort(new BuildTopSorter());
                    buildTop = master.ToList();
                    buildTimestamp = DateTime.Now;
                    return;
                case 2:
                    i = rnd.Next(master.Count);
                    Log("DEMO: Recover " + master[i].Name);
                    master[i].FinishTime = DateTime.Now;
                    master[i].Color = BuildColor.GREEN;
                    master.Sort(new BuildTopSorter());
                    buildTop = master.ToList();
                    buildTimestamp = DateTime.Now;
                    return;
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
                    buildTop = master.ToList();
                    buildTimestamp = DateTime.Now;
                    return;
                case 4:
                    b = master.Where(p => p.Color == BuildColor.RED).LastOrDefault();
                    if (b == null)
                    {
                        buildTop = null;
                        buildTimestamp = DateTime.Now;
                        return;
                    }
                    Log("DEMO: Recover " + b.Name);
                    b.FinishTime = DateTime.Now;
                    b.Color = BuildColor.GREEN;
                    master.Sort(new BuildTopSorter());
                    buildTop = master.ToList();
                    buildTimestamp = DateTime.Now;
                    return;
                default:
                    buildTop = null;
                    buildTimestamp = DateTime.Now;
                    return;
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

    class ServiceWorkerThread : IWorkerThread
    {
        private IClientService _clientService;
        private Thread _thread;

        private object _syncObject;
        private List<string> _logMessages;
        private List<BuildInfo> _buildTop;
        private DateTime _buildTimestamp;

        public ServiceWorkerThread()
        {
            _clientService = new ClientServiceClient();
            _thread = new Thread(ThreadMain);
            _thread.IsBackground = true;
            _syncObject = new object();
        }

        public void InitConnection(IWin32Window parent)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _thread.Start();
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
                firstTime = false;
                try
                {
                    // Go through each build definition and retrieve last status
                    Log("Retrieving build information...");
                    var req = new PollBuildStatusReq
                    {
                        ConfigurationId = 1,
                        UpdateCounter = 0
                    };
                    var resp = _clientService.PollBuildStatus(req);
                    List<BuildInfo> top = new List<BuildInfo>();
                    if (resp.FinishedBuilds == null)
                        resp.FinishedBuilds = new List<FinishedBuild>();
                    foreach (FinishedBuild fb in resp.FinishedBuilds)
                    {
                        var bi = new BuildInfo()
                        {
                            Name = fb.BuildName,
                            Color = (fb.Result == "OK") ? BuildColor.GREEN : BuildColor.RED,
                            FinishTime = fb.TimeStamp,
                            User = fb.UserName
                        };
                        top.Add(bi);
                    }
                    if (resp.QueuedBuilds == null)
                        resp.QueuedBuilds = new List<QueuedBuild>();
                    foreach (QueuedBuild qb in resp.QueuedBuilds)
                    {
                        var bi = new BuildInfo()
                        {
                            Name = qb.BuildName,
                            Color = BuildColor.QUEUED,
                            FinishTime = qb.QueueTime,
                            User = ""
                        };
                        top.Add(bi);
                    }
                    lock (_syncObject)
                    {
                        _buildTop = top;
                        _buildTimestamp = resp.FinishedBuildsDate;
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
            lock (_syncObject)
            {
                if (_logMessages == null)
                    _logMessages = new List<string>();
                _logMessages.Add(line);
            }
        }

        public List<string> RetrieveLogMessages()
        {
            lock (_syncObject)
            {
                var tmp = _logMessages;
                _logMessages = null;
                return tmp;
            }
        }

        public void RetrieveBuildTop(out List<BuildInfo> buildTop, out DateTime buildTimestamp)
        {
            lock (_syncObject)
            {
                buildTop = _buildTop;
                _buildTop = null;
                buildTimestamp = DateTime.Now;
            }
        }

    }
}
