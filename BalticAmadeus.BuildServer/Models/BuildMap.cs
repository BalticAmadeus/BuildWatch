using BalticAmadeus.BuildServer.Interfaces;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Models
{
	public class BuildMap : ClassMap<Build>
	{
		public BuildMap()
		{
			Table("Build");
			CompositeId(x => x.Id)
				.KeyProperty(x => x.BuildDefinitionId, "BuildDefinitionId")
				.KeyProperty(x => x.BuildId, "Id");

			Map(x => x.Instance).Column("Instance").Not.Nullable();
			Map(x => x.TimeStamp).Column("TimeStamp").Not.Nullable();
			Map(x => x.Status).Column("Status").Not.Nullable().CustomType<BuildStatus>();
			Map(x => x.Username).Column("Username").Not.Nullable();
			Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();
		}
	}
}