using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
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

			builder.Register(c => SessionFactory.CreateSessionFactory());
			builder.Register(c => c.Resolve<ISessionFactory>().OpenStatelessSession());
			builder.Register(c => c.Resolve<ISessionFactory>().OpenSession());

			var container = builder.Build();
			configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}
