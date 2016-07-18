using System.Collections.Generic;

namespace BalticAmadeus.BuildServer.Domain.Model.Teams
{
	public class Team
	{
		public int Id { get; protected set; }
		public string Title { get; protected set; }

		public bool IsDeleted { get; protected set; }

		public IEnumerable<TeamMember> Members => _members;
		public IEnumerable<App> Apps => _apps;
		
		private readonly IList<TeamMember> _members = new List<TeamMember>();
		private readonly IList<App> _apps = new List<App>();
		
		public Team(string title)
		{
			Title = title;

			IsDeleted = false;
		}

		protected Team()
		{
			//NHibernate
		}
	}
}
