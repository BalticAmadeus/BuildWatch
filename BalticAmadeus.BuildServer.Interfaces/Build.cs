using System;
using System.Collections.Generic;

namespace BalticAmadeus.BuildServer.Interfaces
{
	public class Build
	{
		public Build(string title)
		{
			OriginalTitle = title;
			AliasTitle = title;

			IsDeleted = false;
		}

		protected Build()
		{
			//NH
		}

		public virtual int Id { get; protected set; }
		public virtual string AliasTitle { get; protected set; }
		public virtual string OriginalTitle { get; protected set; }
		public virtual bool IsDeleted { get; protected set; }

		public virtual IList<BuildRun> BuildRuns { get; set; }

		public virtual void Rename(string title)
		{
			AliasTitle = title;
		}

		public virtual void AddBuild(BuildRun run)
		{
			if (run == null)
				throw new ArgumentNullException(nameof(run));

			BuildRuns.Add(run);
		}

		public virtual void Delete()
		{
			IsDeleted = true;

			foreach (var buildRun in BuildRuns)
				buildRun.Delete();
		}
	}
}
