using BalticAmadeus.BuildServer.Domain.Model.Settings;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Domain.Mappings.Settings
{
	public class SettingMap : ClassMap<Setting>
	{
		public SettingMap()
		{
			Table("Setting");
			Not.LazyLoad();

			CompositeId(x => x.Id)
				.KeyProperty(x => x.SettingKey, "Key")
				.KeyProperty(x => x.Category, "Category");

			Map(x => x.Type).Column("Type").Not.Nullable();
			Map(x => x.Value).Column("Value").Not.Nullable();
			Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();
		}
	}
}
