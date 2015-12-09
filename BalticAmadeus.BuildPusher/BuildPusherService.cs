using System.ServiceProcess;
using BalticAmadeus.BuildPusher.Infrastructure;

namespace BalticAmadeus.BuildPusher
{
	public partial class BuildPusherService : ServiceBase
	{
		private readonly DataSourceManager _dataSourceManager;

		public BuildPusherService(DataSourceManager dataSourceManager)
		{
			_dataSourceManager = dataSourceManager;

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
			_dataSourceManager.Start();
		}

		public void StopDataSourceManager()
		{
			_dataSourceManager.Stop();
		}
	}
}
