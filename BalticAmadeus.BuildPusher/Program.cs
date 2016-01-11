using System;
using System.Diagnostics;
using System.ServiceProcess;
using Autofac;
using Autofac.Features.ResolveAnything;
using BalticAmadeus.BuildPusher.DataSource.TeamCity;
using BalticAmadeus.BuildPusher.Infrastructure;
using BalticAmadeus.BuildPusher.Infrastructure.Http;
using BalticAmadeus.BuildPusher.Infrastructure.Logging;
using BalticAmadeus.BuildPusher.Infrastructure.Settings;

namespace BalticAmadeus.BuildPusher
{
	public static class Program
	{
		public static void Main()
		{
			var container = CreateAndConfigureContainer();

			var service = container.Resolve<BuildPusherService>();

			if (Environment.UserInteractive)
			{
				service.StartDataSourceManager();

		        Console.WriteLine("DataSourceManager started. Press Enter to stop.");
				Console.ReadLine();
				
				if (!Debugger.IsAttached)
					service.StopDataSourceManager();
			}
			else
			{
				ServiceBase.Run(new ServiceBase[] { service });
			}
		}

		private static IContainer CreateAndConfigureContainer()
		{
			var builder = new ContainerBuilder();

			builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

			builder.RegisterType<AppSettingsService>().As<IAppSettingsService>();
			builder.RegisterType<LocalSettingsService>().As<ILocalSettingsService>();
			builder.RegisterType<LoggingService>().As<ILoggingService>();

			builder.Register(x =>
				new ExceptionSafeHttpClientWrapper(
					new HttpClientWrapper(),
					x.Resolve<ILoggingService>()))
				.As<IHttpClientWrapper>();
			
			builder.RegisterType<BuildPusherService>().AsSelf();

			builder.RegisterType<DataSourceManager>().AsSelf();
			builder.RegisterType<TeamCityDataSource>().AsSelf();
			
			return builder.Build();
		}
	}
}
