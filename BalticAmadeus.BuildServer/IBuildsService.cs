using System;
using System.ServiceModel;

namespace BalticAmadeus.BuildServer
{
	[ServiceContract]
	public interface IBuildsService
	{
		[OperationContract]
		void CreateBuild(CreateBuildData data);

		[OperationContract]
		void RenameBuild(RenameBuildData data);

		[OperationContract]
		void DeleteBuild(DeleteBuildData data);

		[OperationContract]
		void AddBuildRun(AddBuildRunData data);
	}

	public class CreateBuildData
	{
		public string Title { get; set; }
	}

	public class RenameBuildData
	{
		public int BuildId { get; set; }

		public string Title { get; set; }
	}

	public class DeleteBuildData
	{
		public int BuildId { get; set; }
	}

	public class AddBuildRunData
	{
		public int BuildId { get; set; }

		public string Instance { get; set; }
		public int Status { get; set; }
		public DateTime TimeStamp { get; set; }
		public string Username { get; set; }
	}
}
