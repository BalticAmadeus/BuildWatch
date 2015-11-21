using BalticAmadeus.BuildServer.Interfaces;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Models
{
	public class BuildDefinitionMap : ClassMap<BuildDefinition>
	{
		public BuildDefinitionMap()
		{
			Table("BuildDefinition");
			Id(x => x.Id).GeneratedBy.Identity().Column("Id");
			Map(x => x.AliasTitle).Column("AliasTitle").Not.Nullable();
			Map(x => x.OriginalTitle).Column("OriginalTitle").Not.Nullable();
			Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();
		}
	}
}