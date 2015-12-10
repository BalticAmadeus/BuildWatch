using NHibernate;

namespace BalticAmadeus.BuildServer.Domain
{
	public interface IDomainEventDispatcher
	{
		void Raise<T>(T domainEvent, ISession session) where T : IDomainEvent;
	}
}