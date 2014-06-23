using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildWatch.DataSource.Common.DataService;
using System.Threading;

namespace BuildWatch.DataSource.Common
{
    public class DataSourceManager
    {
        private IDataService _dataService;
        private CancellationTokenSource _quitSource;
        private CancellationToken _quitToken;
        private Thread _managerThread;
        private IDataSource _dataSource;

        public void Initialize(Type[] dataSourceTypes)
        {
            _dataService = new DataServiceClient();
            _quitSource = new CancellationTokenSource();
            _quitToken = _quitSource.Token;
            _managerThread = new Thread(ThreadRun);
            _dataSource = (IDataSource) Activator.CreateInstance(dataSourceTypes[0]);
        }

        public void Start()
        {
            int ver = _dataService.GetProtocolVersion();
            Console.WriteLine("DataService version: " + ver);
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
            var req = new GetDataSourceConfigRequest {
                Name = "Default"
            };
            var resp = _dataService.GetDataSourceConfig(req);

            var dsConfig = new DataSourceConfig();
            foreach (ConfigEntry e in resp.ConfigEntries)
            {
                dsConfig[e.Key] = e.Value;
            }

            _dataSource.Initialize(dsConfig);

            int pollingInterval = Int32.Parse(dsConfig.Get("PollingInterval", "20"));

            while (true)
            {
                for (int i = 0; i < pollingInterval; i++)
                {
                    Thread.Sleep(1000);
                    if (_quitToken.IsCancellationRequested)
                        return;
                }
                try
                {
                    _dataSource.Poll(_dataService, _quitToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
