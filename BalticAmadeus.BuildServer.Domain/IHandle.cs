using NHibernate;

namespace BalticAmadeus.BuildServer.Domain
{
	public interface IHandle<in T> where T : IDomainEvent
	{
		void Handle(T domainEvent, ISession existingSession);
	}
}