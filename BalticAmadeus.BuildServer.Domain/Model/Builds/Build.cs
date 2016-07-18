using System;
using System.Collections.Generic;

namespace BalticAmadeus.BuildServer.Domain.Model.Builds
{
	public class Build
	{
		public Build(string externalId)
		{
			AliasTitle = externalId;

			IsDeleted = false;
		}

		protected Build()
		{
			//NH
		}

		public virtual int Id { get; protected set; }
		public virtual string ExternalId { get; protected set; }
		public virtual string AliasTitle { get; protected set; }
		//public virtual int? TeamId { get; protected set; }
		public virtual bool IsDeleted { get; protected set; }

		private readonly IList<BuildRun> _buildRuns = new List<BuildRun>();

		public virtual IEnumerable<BuildRun> BuildRuns => _buildRuns;

		public virtual void Rename(string title)
		{
			AliasTitle = title;
		}

		public virtual void AddBuild(BuildRun run)
		{
			if (run == null)
				throw new ArgumentNullException(nameof(run));

			_buildRuns.Add(run);
		}

		public virtual void Delete()
		{
			IsDeleted = true;

			foreach (var buildRun in BuildRuns)
				buildRun.Delete();
		}
		
		public override bool Equals(object obj)
		{
			var target = obj as Build;
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
}
