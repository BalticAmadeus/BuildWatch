using System;
using System.ServiceProcess;
using BuildWatch.DataSource.Common;
using BuildWatch.DataSource.TFS;
using DataSource.TC;
using DataSource.TFS2015;

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
	        StartDataSourceManager();
        }

	    protected override void OnStop()
	    {
		    StopDataSourceManager();
	    }

	    public void StartDataSourceManager()
	    {
		    log.Info("Starting BuildWatch as a service");
		    _dsman = new DataSourceManager();
            _dsman.Initialize(new Type[] { typeof(TFSDataSource), typeof(TCDataSource), typeof(TfsDataSource) });
		    _dsman.Start();
	    }

	    public void StopDataSourceManager()
	    {
		    log.Info("Stopping BuildWatch service");
		    _dsman.Stop();
	    }
    }
}
