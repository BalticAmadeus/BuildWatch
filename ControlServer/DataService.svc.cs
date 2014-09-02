using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BuildWatch.ControlServer.Properties;

namespace BuildWatch.ControlServer
{
    public class DataService : IDataService
    {
        class BuildTopSorter : IComparer<FinishedBuild>
        {
            public int Compare(FinishedBuild x, FinishedBuild y)
            {
                if (x.Result != y.Result)
                    return ColorWeight(x.Result) - ColorWeight(y.Result);
                else
                    return y.TimeStamp.CompareTo(x.TimeStamp);
            }

            private int ColorWeight(string c)
            {
                switch (c)
                {
                    case "OK":
                        return 2;
                    case "FAIL":
                        return 1;
                    default:
                        return 0;
                }
            }
        }

        public int GetProtocolVersion()
        {
            return 0;
        }

        public GetDataSourceConfigResponse GetDataSourceConfig(GetDataSourceConfigRequest req)
        {
            if (req.Name != "Default")
                throw new ApplicationException("DataSource with specified Name is not present");
            var resp = new GetDataSourceConfigResponse();
            resp.DataSourceId = 1;
            resp.ConfigEntries = new List<ConfigEntry>();
            string pairList = Settings.Default.DataSourceSettings;
            foreach (string pair in pairList.Split(';'))
            {
                int p = pair.IndexOf('=');
                if (p < 0)
                    continue;
                string key = pair.Substring(0, p);
                string value = pair.Substring(p + 1);
                resp.ConfigEntries.Add(new ConfigEntry()
                {
                    Key = key,
                    Value = value
                });
            }
            return resp;
        }

        public PushFinishedBuildsResponse PushFinishedBuilds(PushFinishedBuildsRequest req)
        {
            if (req.DataSourceId != 1)
                throw new ApplicationException("DataSource with specified DataSourceId is not present");

            var finishedBuilds = new List<FinishedBuild>();

            foreach (FinishedBuildInfo bi in req.BuildInfo)
            {
                var fb = new FinishedBuild
                {
                    BuildName = bi.BuildName,
                    BuildInstance = bi.BuildInstance,
                    TimeStamp = bi.TimeStamp,
                    Result = bi.Result,
                    UserName = bi.UserName
                };
                switch (bi.Result)
                {
                    case "OK":
                        fb.State = "Green/White";
                        break;
                    case "FAIL":
                        fb.State = "Red/Yellow";
                        break;
                    default:
                        fb.State = "White/Black";
                        break;
                }
                finishedBuilds.Add(fb);
            }

            finishedBuilds.Sort(new BuildTopSorter());

            AppContext.Current.SetBuilds(finishedBuilds);

            var resp = new PushFinishedBuildsResponse();

            return resp;
        }
    }
}
