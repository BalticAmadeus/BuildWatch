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
            // TODO This is kitchen sink
            return new PushFinishedBuildsResponse();
        }
    }
}
