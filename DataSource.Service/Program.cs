using System;
using System.ServiceProcess;

namespace BuildWatch.DataSource.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
	        var service = new DataSourceService();

	        if (Environment.UserInteractive)
	        {
		        service.StartDataSourceManager();

				Console.WriteLine("DataSourceManager started. Press Enter to stop.");
				Console.ReadLine();

				service.StopDataSourceManager();
	        }
	        else
	        {
		        ServiceBase.Run(new [] { service});
	        }
        }
    }
}
