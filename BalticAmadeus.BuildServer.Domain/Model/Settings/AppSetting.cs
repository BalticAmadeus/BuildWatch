using System;

namespace BalticAmadeus.BuildServer.Domain.Model.Settings
{
	public class AppSetting
	{
		public virtual AppSettingCompositeId Id { get; protected set; }
		public virtual string Type { get; protected set; }
		public virtual string Value { get; protected set; }

		public virtual bool IsDeleted { get; protected set; }

		public AppSetting(AppSettingCompositeId id, string type, string value)
		{
			Id = id;
			Type = type;
			Value = value;

			IsDeleted = false;
		}

		public AppSetting()
		{
			//NHibernate	
		}

		public virtual void Delete()
		{
			IsDeleted = true;
		}

		public override bool Equals(object obj)
		{
			var target = obj as AppSetting;
			if (target == null)
				return false;

			return Id.Equals(target.Id);
		}

		public override int GetHashCode()
		{
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ Id.GetHashCode();

			return hash;
		}
	}

	public class AppSettingCompositeId
	{
		public virtual string SettingKey { get; protected set; }
		public virtual string AppKey { get; protected set; }

		public AppSettingCompositeId(string settingKey, string appKey)
		{
			if (string.IsNullOrWhiteSpace(settingKey))
				throw new ArgumentNullException(nameof(settingKey));
			if (string.IsNullOrWhiteSpace(appKey))
				throw new ArgumentNullException(nameof(appKey));

			SettingKey = settingKey;
			AppKey = appKey;
		}

		public AppSettingCompositeId()
		{
			//NHibernate
		}
		
		public override bool Equals(object obj)
		{
			var target = obj as AppSettingCompositeId;
			if (target == null)
				return false;

			return SettingKey.Equals(target.SettingKey) 
				&& AppKey.Equals(target.AppKey);
		}

		public override int GetHashCode()
		{
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ SettingKey.GetHashCode();
			hash = (hash * 397) ^ AppKey.GetHashCode();

			return hash;
		}
	}
}
