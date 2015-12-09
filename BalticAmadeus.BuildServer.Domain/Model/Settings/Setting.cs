using System;

namespace BalticAmadeus.BuildServer.Domain.Model.Settings
{
	public class Setting
	{
		public virtual SettingCompositeId Id { get; protected set; }
		public virtual string Type { get; protected set; }
		public virtual string Value { get; protected set; }

		public virtual bool IsDeleted { get; protected set; }

		public Setting(SettingCompositeId id, string type, string value)
		{
			if (id == null)
				throw new ArgumentNullException(nameof(id));

			Id = id;
			Type = type;
			Value = value;

			IsDeleted = false;
		}

		public Setting()
		{
			//NHibernate	
		}

		public virtual void Delete()
		{
			IsDeleted = true;
		}

		public override bool Equals(object obj)
		{
			var target = obj as Setting;
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
	
	public class SettingCompositeId
	{
		public virtual string SettingKey { get; protected set; }
		public virtual string Category { get; protected set; }

		public SettingCompositeId(string settingKey, string category)
		{
			if (string.IsNullOrWhiteSpace(settingKey))
				throw new ArgumentNullException(nameof(settingKey));
			if (string.IsNullOrWhiteSpace(category))
				throw new ArgumentNullException(nameof(category));

			SettingKey = settingKey;
			Category = category;
		}

		public SettingCompositeId()
		{
			//NHibernate
		}

		public override bool Equals(object obj)
		{
			var target = obj as SettingCompositeId;
			if (target == null)
				return false;

			return SettingKey.Equals(target.SettingKey)
				&& Category.Equals(target.Category);
		}

		public override int GetHashCode()
		{
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ SettingKey.GetHashCode();
			hash = (hash * 397) ^ Category.GetHashCode();

			return hash;
		}
	}


}
