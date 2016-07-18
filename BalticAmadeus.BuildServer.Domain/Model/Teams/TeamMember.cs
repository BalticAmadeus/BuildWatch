namespace BalticAmadeus.BuildServer.Domain.Model.Teams
{
	public class TeamMember
	{
		public int Id { get; protected set; }
		public int TeamId { get; protected set; }
		public int PersonId { get; protected set; }

		public bool IsDeleted { get; protected set; }

		public TeamMember(int teamId, int personId)
		{
			TeamId = teamId;
			PersonId = personId;

			IsDeleted = false;
		}

		public TeamMember()
		{
			//NHibernate
		}
	}
}