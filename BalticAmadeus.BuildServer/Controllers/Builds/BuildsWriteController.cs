using System.Threading.Tasks;
using System.Web.Http;
using BalticAmadeus.BuildServer.Domain.Model.Builds;
using BalticAmadeus.BuildServer.Interfaces.Builds;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers.Builds
{
	public class BuildsWriteController : WriteControllerBase
	{
		private readonly BuildsService _buildsService;

		public BuildsWriteController(ISession session, BuildsService buildsService) : base(session)
		{
			_buildsService = buildsService;
		}

		[Route("api/builds/addBuildRun")]
		[HttpPost]
		public async Task<IHttpActionResult> AddBuildRun([FromBody] AddBuildRunCommand command)
		{
			await Task.Delay(1);

			using (var tx = Session.BeginTransaction())
			{
				_buildsService.AddBuildRun(Session, 
					command.BuildId, command.BuildRunId, command.Title,
					command.Status, command.TimeStamp, command.FinishTimeStamp, command.Username);

				tx.Commit();
			}
			
			return Ok();
		}

		[Route("api/builds/renameBuild")]
		[HttpPost]
		public async Task<IHttpActionResult> RenameBuild([FromBody] RenameBuildCommand command)
		{
			await Task.Delay(1);

			using (var tx = Session.BeginTransaction())
			{
				var build = Session.Load<Build>(command.BuildId);
				build.Rename(command.Title);

				tx.Commit();
			}

			return Ok();
		}

		[Route("api/builds/deleteBuild")]
		[HttpPost]
		public async Task<IHttpActionResult> DeleteBuild([FromBody] DeleteBuildCommand command)
		{
			await Task.Delay(1);

			using (var tx = Session.BeginTransaction())
			{
				var build = Session.Load<Build>(command.BuildId);
				build.Delete();

				tx.Commit();
			}

			return Ok();
		}
	}
}