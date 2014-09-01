using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildWatch.DataSource.Common;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BuildWatch.DataSource.Standalone
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            log.Info("START");
            var dsman = new DataSourceManager();
            dsman.Initialize(new Type[] { typeof(BuildWatch.DataSource.TFS.TFSDataSource) } );
            dsman.Start();
            Console.WriteLine("DataSourceManager started. Press Enter to stop.");
            Console.ReadLine();
            dsman.Stop();
        }
    }
}
