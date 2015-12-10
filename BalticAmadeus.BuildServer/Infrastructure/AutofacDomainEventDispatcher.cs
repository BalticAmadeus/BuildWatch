using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using BalticAmadeus.BuildServer.Domain;
using BalticAmadeus.BuildServer.Domain.Model;
using Newtonsoft.Json;
using NHibernate;

namespace BalticAmadeus.BuildServer.Infrastructure
{
	public class AutofacDomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IContainer _container;
		private readonly JsonSerializer _serializer;

		public AutofacDomainEventDispatcher(IContainer container)
		{
			_container = container;
			_serializer = JsonSerializer.CreateDefault();
		}

		public void Raise<T>(T domainEvent, ISession session) where T : IDomainEvent
		{
			string eventData =_serializer.Serialize(domainEvent);

			var @event = new Event(domainEvent.GetType().FullName, eventData);
			session.Save(@event);

			using (var lifetime = _container.BeginLifetimeScope())
			{
				foreach (IHandle<T> specificEventHandler in ResolveMany(typeof (IHandle<T>), lifetime))
					specificEventHandler.Handle(domainEvent, session);
			}
		}

		private static IEnumerable<object> ResolveMany(Type type, ILifetimeScope lifetime)
		{
			var enumerableServiceType = typeof (IEnumerable<>).MakeGenericType(type);
			var instance = lifetime.Resolve(enumerableServiceType);
			return (IEnumerable<object>) instance;
		}

		public static void RegisterDomainEventHandlers(ContainerBuilder builder)
		{
			var eventHandlers = typeof (DomainEvents).Assembly.GetTypes()
				.Where(x => x.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof (IHandle<>)));

			foreach (var eventHandler in eventHandlers)
			{
				var handlerInterfaces =
					eventHandler.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IHandle<>));

				foreach (var handlerInterface in handlerInterfaces)
				{
					if (handlerInterface == typeof (IDomainEvent))
						builder.RegisterType(eventHandler).As<IHandle<IDomainEvent>>();
					else
						builder.RegisterType(eventHandler).As(handlerInterface);
				}
			}
		}
	}
}