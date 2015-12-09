using BalticAmadeus.BuildServer.Domain.Model.Builds;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Domain.Mappings.Builds
{
	public class BuildMap : ClassMap<Build>
	{
		public BuildMap()
		{
			Table("Build");

			Id(x => x.Id).Column("Id").GeneratedBy.Assigned();
			Map(x => x.AliasTitle).Column("AliasTitle").Not.Nullable();
			Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();

			HasMany(x => x.BuildRuns).KeyColumns.Add("BuildId")
				.Where(x => x.IsDeleted == false)
				.OrderBy("StartedTimeStamp DESC")
				.Cascade.SaveUpdate().AsBag().Not.LazyLoad();
		}
	}
}