using BalticAmadeus.BuildServer.Domain.Model;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Domain.Mappings
{
	public class EventMap : ClassMap<Event>
	{
		public EventMap()
		{
			Table("Event");

			Id(x => x.Id).Column("Id").GeneratedBy.Assigned();
			Map(x => x.TimeStamp).Column("TimeStamp");
			Map(x => x.FullTypeName).Column("FullTypeName");
			Map(x => x.Content).Column("Content").CustomType<StringClobUserType>();
		}
	}
}
