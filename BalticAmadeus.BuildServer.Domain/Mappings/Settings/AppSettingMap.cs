using BalticAmadeus.BuildServer.Domain.Model.Settings;
using FluentNHibernate.Mapping;

namespace BalticAmadeus.BuildServer.Domain.Mappings.Settings
{
	public class AppSettingMap : ClassMap<AppSetting>
	{
		public AppSettingMap()
		{
			Table("AppSetting");
			Not.LazyLoad();

			CompositeId(x => x.Id)
				.KeyProperty(x => x.SettingKey, "AppSettingKey")
				.KeyProperty(x => x.AppKey, "AppKey");

			Map(x => x.Type).Column("Type").Not.Nullable();
			Map(x => x.Value).Column("Value").Not.Nullable();
			Map(x => x.IsDeleted).Column("IsDeleted").Not.Nullable();
		}
	}
}
