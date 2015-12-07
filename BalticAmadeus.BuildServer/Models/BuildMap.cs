using BalticAmadeus.BuildServer.Interfaces;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Models
{
	public class BuildMap : ClassMap<Build>
	{
		public BuildMap()
		{
			Table("Build");

			Id(x => x.Id).GeneratedBy.Identity().Column("Id");

			Map(x => x.AliasTitle).Column("AliasTitle").Not.Nullable();
			Map(x => x.OriginalTitle).Column("OriginalTitle").Not.Nullable();
			Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();

			HasMany(x => x.BuildRuns).KeyColumn("BuildId").Where(x => x.IsDeleted == false).OrderBy("Id DESC").Cascade.SaveUpdate().AsBag();
		}
	}
}