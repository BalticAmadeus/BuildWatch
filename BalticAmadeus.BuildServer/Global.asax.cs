using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using BalticAmadeus.BuildServer.Controllers;
using BalticAmadeus.BuildServer.Controllers.Builds;
using BalticAmadeus.BuildServer.Controllers.Settings;
using BalticAmadeus.BuildServer.Domain;
using BalticAmadeus.BuildServer.Infrastructure;
using NHibernate;

namespace BalticAmadeus.BuildServer
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			var configuration = GlobalConfiguration.Configuration;

			var builder = new ContainerBuilder();
			builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			//builder.RegisterType<BuildsReadController>().AsSelf();
			//builder.RegisterType<BuildsWriteController>().AsSelf();
			//builder.RegisterType<FinishedBuildsController>().AsSelf();
			//builder.RegisterType<QueuedBuildsController>().AsSelf();
			//builder.RegisterType<AppSettingsReadController>().AsSelf();
			//builder.RegisterType<AppSettingsWriteController>().AsSelf();
			//builder.RegisterType<SettingsReadController>().AsSelf();

			//builder.RegisterType<HomeController>().AsSelf();
			
			builder.Register(c => SessionFactory.CreateSessionFactory());
			builder.Register(c => c.Resolve<ISessionFactory>().OpenStatelessSession());
			builder.Register(c => c.Resolve<ISessionFactory>().OpenSession());

			var container = builder.Build();
			configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			DomainEvents.Dispatcher = new AutofacDomainEventDispatcher(container);
			AutofacDomainEventDispatcher.RegisterDomainEventHandlers(builder);
		}
	}
}
