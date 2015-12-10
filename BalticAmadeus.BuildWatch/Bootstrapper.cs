using System.Windows;
using Autofac;
using Autofac.Features.ResolveAnything;
using BalticAmadeus.BuildWatch.Infrastructure.Settings;
using BuildWatch.Infrastructure;
using Prism.Autofac;
using Prism.Modularity;

namespace BuildWatch
{
	public class Bootstrapper : AutofacBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<Shell>();
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();

			Application.Current.MainWindow = (Window)Shell;
			Application.Current.MainWindow.Show();
		}

		protected override void ConfigureContainerBuilder(ContainerBuilder builder)
		{
			base.ConfigureContainerBuilder(builder);

			builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

			builder.RegisterType<LocalSettingsService>().As<ILocalSettingsService>();
		}

		protected override void ConfigureModuleCatalog()
		{
			base.ConfigureModuleCatalog();

			var moduleCatalog = (ModuleCatalog)ModuleCatalog;
			moduleCatalog.AddModule(typeof(BalticAmadeus.BuildWatch.Builds.Module));
		}
	}
}
