namespace BalticAmadeus.BuildPusher.Infrastructure
{
	public interface IDataSource
	{
		void Initialize();
		void SynchronizeBuilds();

		bool IsEnabled { get; }
	}
}
