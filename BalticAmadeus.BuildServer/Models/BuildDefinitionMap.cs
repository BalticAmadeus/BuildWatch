using BalticAmadeus.BuildServer.Interfaces;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Models
{
	public class BuildDefinitionMap : ClassMap<BuildDefinition>
	{
		public BuildDefinitionMap()
		{
			Table("Build");

			Id(x => x.Id).Column("Id").GeneratedBy.Increment();

			Map(x => x.AliasTitle).Column("AliasTitle");
			Map(x => x.OriginalTitle).Column("OriginalTitle");
		}
	}
}