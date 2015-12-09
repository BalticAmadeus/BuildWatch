namespace BalticAmadeus.BuildPusher.Infrastructure
{
	public interface IDataSource
	{
		void SynchronizeBuilds();

		bool IsEnabled { get; }
	}
}
