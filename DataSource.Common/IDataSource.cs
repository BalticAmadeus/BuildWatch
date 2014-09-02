using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildWatch.DataSource.Common.DataService;
using System.Threading;

namespace BuildWatch.DataSource.Common
{
    public interface IDataSource
    {
        void Initialize(DataSourceConfig config);
        void Poll(IDataService dataService, CancellationToken quitToken);
    }
}
