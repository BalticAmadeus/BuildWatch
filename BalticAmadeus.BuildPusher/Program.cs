using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using Autofac;
using Autofac.Features.ResolveAnything;
using BalticAmadeus.BuildPusher.DataSource.TeamCity;
using BalticAmadeus.BuildPusher.Infrastructure;
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

			builder.RegisterType<BuildPusherService>().AsSelf();
			builder.RegisterType<AppSettingsService>().As<IAppSettingsService>();
			builder.RegisterType<LocalSettingsService>().As<ILocalSettingsService>();
			builder.RegisterType<DataSourceManager>().AsSelf();

			builder.RegisterType<TeamCityDataSource>().AsSelf();

			//var types = AppDomain.CurrentDomain.GetAssemblies()
			//	.SelectMany(assembly => assembly.GetTypes())
			//	.Where(type => type.IsAssignableFrom(typeof(IDataSource)));

			//foreach (var type in types)
			//	builder.RegisterType(type).AsSelf();

			return builder.Build();
		}
	}
}
