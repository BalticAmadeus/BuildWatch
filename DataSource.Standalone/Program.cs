using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildWatch.DataSource.Common;

namespace BuildWatch.DataSource.Standalone
{
    class Program
    {
        static void Main(string[] args)
        {
            var dsman = new DataSourceManager();
            dsman.Initialize(new Type[] { typeof(BuildWatch.DataSource.TFS.TFSDataSource) } );
            dsman.Start();
            Console.WriteLine("DataSourceManager started. Press Enter to stop.");
            Console.ReadLine();
            dsman.Stop();
        }
    }
}
