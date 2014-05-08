using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildWatch.DataSource.Common.DataService;

namespace BuildWatch.DataSource.Common
{
    public class DataSourceManager
    {
        private IDataService _dataService;

        public void Initialize()
        {
            _dataService = new DataServiceClient();
        }

        public void Start()
        {
            int ver = _dataService.GetProtocolVersion();
            Console.WriteLine("DataService version: " + ver);
        }

        public void Stop()
        {
        }
    }
}
