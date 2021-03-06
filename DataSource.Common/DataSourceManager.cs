﻿using System;
using BuildWatch.DataSource.Common.DataService;
using System.Threading;

namespace BuildWatch.DataSource.Common
{
    public class DataSourceManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DataSourceManager));

        private IDataService _dataService;
        private CancellationTokenSource _quitSource;
        private CancellationToken _quitToken;
        private Thread _managerThread;
        private IDataSource[] _dataSources;

        public void Initialize(Type[] dataSourceTypes)
        {
            log.Info("Initializing data source manager...");
            _dataService = new DataServiceClient();
            _quitSource = new CancellationTokenSource();
            _quitToken = _quitSource.Token;
            _managerThread = new Thread(ThreadRun);
            _dataSources = new IDataSource[dataSourceTypes.Length];

	        for (int index = 0; index < dataSourceTypes.Length; index++)
				_dataSources[index] = (IDataSource)Activator.CreateInstance(dataSourceTypes[index]);
        }

        public void Start()
        {
            log.Info("Starting data source manager...");
            int ver = _dataService.GetProtocolVersion();
            log.Info("DataService version: " + ver);
            _managerThread.Start();
        }

        public void Stop()
        {
            log.Info("Stopping data source manager...");
            _quitSource.Cancel();
            if (_managerThread.Join(30000))
                return;
            _managerThread.Abort();
        }

        private void ThreadRun()
        {
            log.Info("DataSourceManager thread starting");

            var req = new GetDataSourceConfigRequest {
                Name = "Default"
            };
            var resp = _dataService.GetDataSourceConfig(req);

            var dsConfig = new DataSourceConfig();
            foreach (ConfigEntry e in resp.ConfigEntries)
            {
                dsConfig[e.Key] = e.Value;
            }

	        foreach (var dataSource in _dataSources)
				dataSource.Initialize(dsConfig);

            int pollingInterval = Int32.Parse(dsConfig.Get("PollingInterval", "20"));

            log.Info("DataSourceManager thread operational");

            while (true)
            {
                for (int i = 0; i < pollingInterval; i++)
                {
                    Thread.Sleep(1000);

                    if (_quitToken.IsCancellationRequested)
                    {
                        log.Info("DataSourceManager thread quitting gracefully");
                        return;
                    }
                }

	            foreach (var dataSource in _dataSources)
	            {
					try
					{
						log.Debug("Polling _dataSource");
						dataSource.Poll(_dataService, _quitToken);
					}
					catch (Exception ex)
					{
						log.Error("Polling error", ex);
					}
	            }
            }
        }
    }
}
