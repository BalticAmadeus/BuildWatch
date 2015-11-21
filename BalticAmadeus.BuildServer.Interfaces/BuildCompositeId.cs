namespace BalticAmadeus.BuildServer.Interfaces
{
	public class BuildCompositeId
	{
		public BuildCompositeId(int buildDefinitionId, int buildId)
		{
			BuildId = buildId;
			BuildDefinitionId = buildDefinitionId;
		}

		protected BuildCompositeId()
		{
			//NHibernate
		}

		public virtual int BuildId { get; protected set; }
		public virtual int BuildDefinitionId { get; protected set; }

		public override bool Equals(object obj)
		{
			var other = obj as BuildCompositeId;

			if (other == null)
				return false;

			return BuildId == other.BuildId
				   && BuildDefinitionId == other.BuildDefinitionId;
		}

		public override int GetHashCode()
		{
			int code = GetType().GetHashCode();
			code += 433 ^ BuildId.GetHashCode();
			code += 439 ^ BuildDefinitionId.GetHashCode();

			return code;
		}
	}
}
