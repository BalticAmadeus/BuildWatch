using BalticAmadeus.BuildServer.Interfaces;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Models
{
	public class BuildRunMap : ClassMap<BuildRun>
	{
		public BuildRunMap()
		{
			Table("BuildRun");
			
			Id(x => x.Id).Column("Id");

			Map(x => x.BuildId).Column("BuildId").Not.Nullable();
			Map(x => x.Instance).Column("Instance").Not.Nullable();
			Map(x => x.StartedTimeStamp).Column("StartedTimeStamp").Not.Nullable();
			Map(x => x.FinishedTimeStamp).Column("FinishedTimeStamp").Nullable();
			Map(x => x.Status).Column("Status").Not.Nullable().CustomType<BuildRunStatus>();
			Map(x => x.Username).Column("Username").Not.Nullable();
			Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();
		}
	}
}