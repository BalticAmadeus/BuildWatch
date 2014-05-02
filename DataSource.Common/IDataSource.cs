using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildWatch.DataSource.Common.DataService;

namespace BuildWatch.DataSource.Common
{
    public interface IDataSource
    {
        void Initialize(DataSourceConfig config);
        void Poll(IDataService dataService);
    }
}
