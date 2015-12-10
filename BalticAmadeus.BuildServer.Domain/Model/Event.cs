using System;

namespace BalticAmadeus.BuildServer.Domain.Model
{
	public class Event
	{
		public virtual Guid Id { get; protected set; }
		public virtual DateTime TimeStamp { get; protected set; }
		public virtual string FullTypeName { get; protected set; }
		public virtual string Content { get; protected set; }

		public Event(string fullTypeName, string content)
		{
			FullTypeName = fullTypeName;
			Content = content;

			Id = Guid.NewGuid();
			TimeStamp = DateTime.Now;
		}

		public Event()
		{
			//NHibernate
		}
	}
}
