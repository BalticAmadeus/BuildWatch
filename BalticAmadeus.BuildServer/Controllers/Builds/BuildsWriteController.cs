using System.Threading.Tasks;
using System.Web.Http;
using BalticAmadeus.BuildServer.Domain.Model.Builds;
using BalticAmadeus.BuildServer.Infrastructure;
using BalticAmadeus.BuildServer.Interfaces.Builds;
using NHibernate;

namespace BalticAmadeus.BuildServer.Controllers.Builds
{
	public class BuildsWriteController : WriteControllerBase
	{
		public BuildsWriteController(ISession session) : base(session)
		{
		}

		[Route("api/builds/addBuildRun")]
		[HttpPost]
		[BuildWatchExceptionFilter]
		public async Task<IHttpActionResult> AddBuildRun([FromBody] AddBuildRunCommand command)
		{
			await Task.Delay(1);

			using (var tx = Session.BeginTransaction())
			{
				BuildsService.AddBuildRun(Session, 
					command.BuildId, string.Concat(command.BuildId, command.BuildRunId), command.Title,
					command.Status, command.TimeStamp, command.FinishTimeStamp, command.Username);

				tx.Commit();
			}
			
			return Ok();
		}

		[Route("api/builds/renameBuild")]
		[HttpPost]
		[BuildWatchExceptionFilter]
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
		[BuildWatchExceptionFilter]
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