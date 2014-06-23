using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BuildWatch.DataSource.Common;

namespace BuildWatch.DataSource.Service
{
    public partial class DataSourceService : ServiceBase
    {
        private DataSourceManager _dsman;

        public DataSourceService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _dsman = new DataSourceManager();
            _dsman.Initialize(new Type[] { typeof(BuildWatch.DataSource.TFS.TFSDataSource) } );
            _dsman.Start();
        }

        protected override void OnStop()
        {
            _dsman.Stop();
        }
    }
}
