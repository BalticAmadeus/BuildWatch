using System;
using BuildWatch.DataSource.Common;
using BuildWatch.DataSource.TFS;
using DataSource.TC;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]

namespace BuildWatch.DataSource.Standalone
{
	internal class Program
	{
		private static readonly ILog log = LogManager.GetLogger(typeof (Program));

		private static void Main(string[] args)
		{
			log.Info("START");
			var dsman = new DataSourceManager();
			dsman.Initialize(new[] {typeof (TFSDataSource), typeof(TCDataSource)});
			dsman.Start();
			Console.WriteLine("DataSourceManager started. Press Enter to stop.");
			Console.ReadLine();
			dsman.Stop();
		}
	}
}