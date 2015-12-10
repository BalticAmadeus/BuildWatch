using BalticAmadeus.BuildServer.Domain.Model;
using NHibernate;

namespace BalticAmadeus.BuildServer.Domain
{
	public static class DomainEvents
	{
		public static IDomainEventDispatcher Dispatcher { private get; set; }

		public static void Raise<T>(T domainEvent, ISession session) where T : IDomainEvent
		{
			Dispatcher.Raise(domainEvent, session);
		}
	}
}
