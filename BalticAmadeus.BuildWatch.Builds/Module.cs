using System;
using System.IO;
using Autofac;
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
			
			updater.Register(c => new PhotoService($"{Directory.GetCurrentDirectory()}/{"Photos"}")).AsSelf();
			updater.Register(c => new SoundService($"{Directory.GetCurrentDirectory()}/{"Sounds"}")).AsSelf();

			updater.RegisterType<BuildListView>().AsSelf().Named<object>("BuildListView");

			updater.Update(_container);

			var regionManager = _container.Resolve<IRegionManager>();
			regionManager.RequestNavigate("Root", new Uri("BuildListView", UriKind.Relative));
		}
	}
}
