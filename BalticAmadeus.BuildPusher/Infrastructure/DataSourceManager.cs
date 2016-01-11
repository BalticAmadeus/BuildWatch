using System.Linq;
using System.Threading;
using BalticAmadeus.BuildPusher.DataSource.TeamCity;
using BalticAmadeus.BuildPusher.DataSource.Tfs;
using BalticAmadeus.BuildPusher.Infrastructure.Settings;
using BalticAmadeus.BuildServer.Interfaces;

namespace BalticAmadeus.BuildPusher.Infrastructure
{
	public class DataSourceManager
	{
		private readonly IAppSettingsService _appSettingsService;
		
		private readonly CancellationTokenSource _quitSource;
		private CancellationToken _quitToken;

		private readonly Thread _managerThread;
		private readonly IDataSource[] _dataSources;

		public DataSourceManager(
			IAppSettingsService appSettingsService, 
			TeamCityDataSource teamCityDataSource,
			TfsDataSource tfsDataSource)
		{
			_appSettingsService = appSettingsService;
			
			_quitSource = new CancellationTokenSource();
			_quitToken = _quitSource.Token;

			_managerThread = new Thread(ThreadRun);

			_dataSources = new IDataSource[]
			{
				//teamCityDataSource,
				tfsDataSource
			};
		}

		public void Start()
		{
			_appSettingsService.ReloadSettings();
			
			_managerThread.Start();
		}

		public void Stop()
		{
			_quitSource.Cancel();

			if (_managerThread.Join(30000))
				return;

			_managerThread.Abort();
		}

		private void ThreadRun()
		{
			foreach (var dataSource in _dataSources)
				dataSource.Initialize();

			int pollingIntervalInMiliseconds = _appSettingsService.GetInt(SharedConstants.DataSource.RefreshTimeoutInMilisecondsKey);

			while (!_quitToken.IsCancellationRequested)
			{
				foreach (var dataSource in _dataSources.Where(x => x.IsEnabled))
				{
					dataSource.SynchronizeBuilds();
				}

				for (int i = 0; i < pollingIntervalInMiliseconds/1000; i++)
				{
					if (_quitToken.IsCancellationRequested)
						return;

					Thread.Sleep(1000);
				}
			}
		}
	}
}
