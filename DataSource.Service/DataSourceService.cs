using System;
using System.ServiceProcess;
using BuildWatch.DataSource.Common;
using DataSource.TC;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BuildWatch.DataSource.Service
{
    public partial class DataSourceService : ServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DataSourceService));

        private DataSourceManager _dsman;

        public DataSourceService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log.Info("Starting BuildWatch as a service");
            _dsman = new DataSourceManager();
            _dsman.Initialize(new Type[] { typeof(TFS.TFSDataSource) } );
            _dsman.Initialize(new Type[] { typeof(TCDataSource) } );
            _dsman.Start();
        }

        protected override void OnStop()
        {
            log.Info("Stopping BuildWatch service");
            _dsman.Stop();
        }
    }
}
