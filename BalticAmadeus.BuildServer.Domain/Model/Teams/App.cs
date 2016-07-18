namespace BalticAmadeus.BuildServer.Domain.Model.Teams
{
	public class App
	{
		public int Id { get; protected set; }
		public int TeamId { get; protected set; }
		public string UniqueName { get; protected set; }
		public bool IsDeleted { get; protected set; }

		public App(int teamId, string uniqueName)
		{
			TeamId = teamId;
			UniqueName = uniqueName;

			IsDeleted = false;
		}

		public App()
		{
			//NHibernate
		}
	}
}