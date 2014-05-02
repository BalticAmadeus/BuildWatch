using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildWatch.DataSource.Common;
using BuildWatch.DataSource.Common.DataService;

namespace BuildWatch.DataSource.TFS
{
    public class TFSDataSource : IDataSource
    {
        public string TeamServerUri { get; private set; }
        public string TeamProjectCollection { get; private set; }
        public string TeamProjectName { get; private set; }
        public string TeamServerUser { get; private set; }
        public string TeamServerPassword { get; private set; }


        public void Initialize(DataSourceConfig config)
        {
            TeamServerUri = config["TeamServerUri"];
            TeamProjectCollection = config["TeamProjectCollection"];
            TeamProjectName = config["TeamProjectName"];
            TeamServerUser = config.Get("TeamServerUser", "");
            TeamServerPassword = config.Get("TeamServerPassword", "");
        }

        public void Poll(IDataService dataService)
        {
            throw new NotImplementedException();
        }
    }
}
