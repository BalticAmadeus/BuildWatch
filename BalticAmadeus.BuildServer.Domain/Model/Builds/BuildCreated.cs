namespace BalticAmadeus.BuildServer.Domain.Model.Builds
{
	public class BuildCreated : IDomainEvent
	{
		public string Id { get; set; }

		public BuildCreated(string id)
		{
			Id = id;
		}

		public BuildCreated()
		{
			//JsonSerializer
		}
	}
}
