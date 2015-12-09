using BalticAmadeus.BuildServer.Domain.Model.Builds;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Domain.Mappings.Builds
{
	public class BuildRunMap : ClassMap<BuildRun>
	{
		public BuildRunMap()
		{
			Table("BuildRun");

			CompositeId()
				.KeyProperty(x => x.BuildId, "BuildId")
				.KeyProperty(x => x.Id, "Id");

			Map(x => x.Instance).Column("Instance").Not.Nullable();
			Map(x => x.Status).Column("Status").Not.Nullable();
			Map(x => x.StartedTimeStamp).Column("StartedTimeStamp").Not.Nullable();
			Map(x => x.FinishedTimeStamp).Column("FinishedTimeStamp").Nullable();
			Map(x => x.Username).Column("Username").Not.Nullable();
			Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();
		}
	}
}
