using BalticAmadeus.BuildServer.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;

namespace BalticAmadeus.BuildServer
{
	public class SessionFactory
	{
		public static ISessionFactory CreateSessionFactory()
		{
			var cfg = new Configuration().Configure();
			
			return Fluently.Configure(cfg)
				.Mappings(m => m.FluentMappings
					.Conventions.Setup(x => x.Add(AutoImport.Never()))
					.AddFromAssemblyOf<BuildMap>())
				.BuildSessionFactory();
		}
	}
}