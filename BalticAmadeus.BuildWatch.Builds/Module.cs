using System;
using System.IO;
using System.ServiceModel;
using Autofac;
using BalticAmadeus.BuildWatch.Builds.ClientService;
using Prism.Modularity;
using Prism.Regions;

namespace BalticAmadeus.BuildWatch.Builds
{
	public class Module : IModule
	{
		private readonly IContainer _container;

		public Module(IContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			var updater = new ContainerBuilder();

			updater.Register(c => new ClientServiceClient(
				new BasicHttpBinding(),
				new EndpointAddress(c.ResolveKeyed<string>("ClientServiceEndpoint")))).As<IClientService>();

			updater.Register(c => new PhotoService($"{Directory.GetCurrentDirectory()}/{"Photos"}")).AsSelf();
			updater.Register(c => new SoundService($"{Directory.GetCurrentDirectory()}/{"Sounds"}")).AsSelf();

			updater.RegisterType<BuildListView>().AsSelf().Named<object>("BuildListView");

			updater.Update(_container);

			var regionManager = _container.Resolve<IRegionManager>();
			regionManager.RequestNavigate("Root", new Uri("BuildListView", UriKind.Relative));
		}
	}
}
