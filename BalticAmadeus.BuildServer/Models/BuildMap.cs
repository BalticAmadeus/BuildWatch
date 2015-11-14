using BalticAmadeus.BuildServer.Interfaces;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Models
{
	public class BuildMap : ClassMap<Build>
	{
		public BuildMap()
		{
			Table("Build");

			Id(x => x.Id).Column("Id").GeneratedBy.Increment();

			Map(x => x.Instance).Column("Instance");
			Map(x => x.Name).Column("Name");
			Map(x => x.Status).Column("Status").CustomType<int>();
			Map(x => x.TimeStamp).Column("TimeStamp");
			Map(x => x.User).Column("Username");
		}
	}
}