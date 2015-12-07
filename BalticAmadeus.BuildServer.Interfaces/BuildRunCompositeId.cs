namespace BalticAmadeus.BuildServer.Interfaces
{
	public class BuildRunCompositeId
	{
		public BuildRunCompositeId(int buildId, int buildRunId)
		{
			BuildRunId = buildRunId;
			BuildId = buildId;
		}

		protected BuildRunCompositeId()
		{
			//NHibernate
		}

		public virtual int BuildRunId { get; protected set; }
		public virtual int BuildId { get; protected set; }

		public override bool Equals(object obj)
		{
			var other = obj as BuildRunCompositeId;

			if (other == null)
				return false;

			return BuildRunId == other.BuildRunId
				   && BuildId == other.BuildId;
		}

		public override int GetHashCode()
		{
			int code = GetType().GetHashCode();
			code += 439 ^ BuildId.GetHashCode();
			code += 433 ^ BuildRunId.GetHashCode();

			return code;
		}
	}
}
